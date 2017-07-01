using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iphone
{
    public partial class iBrowser : Form
    {
        //Globals
        private iPhone phone;
        private string phoneSrc, lstfileName = "", pcPath = "", copyWorkerPcDest,
            temp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\iBrowser\",
            fullpath = "/";
        private bool isSearching = false, openFile = false, canRemovePath = false;
        public static bool stopCopy = false;
        private int sortColumn = -1, pathCount = 0, fileCount = 0;
        private static int error;
        private IntPtr currDev;
        private List<string> recentPaths;
        private ArrayList devs;
        private BackgroundWorker getItems, search;
        private ListViewItem i;
        private Dictionary<string, string> pathsToCopyList;
        private const int THUMB_SIZE = 64;

        //Constructor
        public iBrowser()
        {
            InitializeComponent();
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);

            devs = new ArrayList();
            recentPaths = new List<string>();
            recentPaths.Add("/");
            phone = new iPhone(phoneConnect, phoneDisconnect);
            pathsToCopyList = new Dictionary<string, string>();

            ProcessTabKey(false);
            newFolder.Image = ShellEx.GetFolderIcon(ShellEx.IconSizeEnum.SmallIcon16);
            newEmptyFile.Image = ShellEx.GetFileIcon(".a random string that does not have an icon", ShellEx.IconSizeEnum.SmallIcon16);
            searchBox.GotFocus += searchBoxFocussed;
            searchBox.LostFocus += searchBoxLostFocus;
            listView1.EnableDoubleBuffer();
            listView1.SetIconSpacing((short)(THUMB_SIZE * 1.75), (short)(THUMB_SIZE * 1.75));
            foreach (Control c in this.Controls)
            {
                c.TabStop = false;
                c.TabIndex = 0;
            }
        }
        public static int ErrorChange
        {
            get { return error; }
            set
            {
                error = value;
                if (error == 1)
                    MessageBox.Show("Please unlock your device and choose 'Trust', then select OK");
                else if (error == 2)
                    MessageBox.Show("Cannot load thumbnails. Is your device locked?", "Error");
                else if (error == 3)
                    MessageBox.Show("Have you synced your device?\nIs iTunes installed?\nIs the MobileDevice service running?");
                else if (error == 4)
                    MessageBox.Show("Cannot save files/folder. Is your device locked?", "Error");
                else if (error == 5)
                    MessageBox.Show("Cannot copy files. Is your device locked?", "Error");
                else if (error == 6)
                    MessageBox.Show("Cannot access filesystem. Unlock your device then select OK/Trust", "Error");
            }
        }

        private enum CopyType
        {
            PhoneToPc,
            PcToPhone,
        }

        //Methods
        private string readableFileSize(ulong size)
        {
            string num = size.ToString();
            return (size / 1024d / 1024d).ToString("0.00") + " MB";
        }
        private void loadItems(string path)
        {
            fullpath = path;
            pathBox.Text = path;

            listView1.Items.Clear();
            LargeImList.Images.Clear();
            LargeImList.Images.Add(ShellEx.GetFolderIcon(ShellEx.IconSizeEnum.LargeIcon48));

            imageList.Images.Clear();
            imageList.Images.Add(ShellEx.GetFolderIcon(ShellEx.IconSizeEnum.SmallIcon16));
            foreach (string dir in phone.GetDirectories(path))
            {
                ListViewItem dirList = new ListViewItem(dir, 0);
                dirList.SubItems.Add(""); //size
                if (phone.IsLink(path + "/" + dir))
                {
                    string target = phone.LinkTarget;
                    if (!target.StartsWith("/"))
                        target = "/" + target;
                    dirList.SubItems.Add("SymLink -> " + target);
                }
                else
                    dirList.SubItems.Add("Folder");
                dirList.SubItems.Add(phone.GetModifiedTime(path + "/" + dir).ToString());
                Console.WriteLine("Adding directory");
                listView1.Items.Add(dirList);
            }

            int index = 1;

            foreach (string file in phone.GetFiles(path))
            {
                imageList.Images.Add(ShellEx.GetFileIcon(file, ShellEx.IconSizeEnum.SmallIcon16));
                LargeImList.Images.Add(ShellEx.GetFileIcon(file, ShellEx.IconSizeEnum.LargeIcon48));

                ListViewItem fileItem = new ListViewItem(file, index);
                fileItem.SubItems.Add(readableFileSize(phone.FileSize(path + "/" + file)));
                fileItem.SubItems.Add(ShellEx.GetFileTypeDescription(file));
                fileItem.SubItems.Add(phone.GetModifiedTime(path + "/" + file).ToString());
                Console.WriteLine("Adding file");
                listView1.Items.Add(fileItem);

                index++;
            }
            showStatusInfo();
        }
        private void SetListviewColumnMinMaxWidths()
        {
            for (int i = 0; i < 4; i++)
            {
                if (listView1.Columns[i].Width < 100)
                    listView1.Columns[i].Width = 150;
                if (listView1.Columns[i].Width > 300)
                    listView1.Columns[i].Width = 300;
            }
        }

        private void StartCopyWorkerFromPhone()
        {
            progressBar.Visible = true;
            status.Text = "Copying";
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += CopyFromPhoneWorker;
            bw.ProgressChanged += CopyFromPhoneProgress;
            bw.RunWorkerCompleted += CopyFromPhoneCompleted;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }
        private void StartCopyWorkerToPhone(string src, string dest)
        {
            progressBar.Visible = true;
            string[] args = { src, dest };
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += CopyToPhoneWorker;
            bw.ProgressChanged += CopyToPhoneProgress;
            bw.RunWorkerCompleted += CopyToPhoneCompleted;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync(args);
        }
        private void itemDblClick()
        {
            string newFullPath = fullpath + "/" + lstfileName;
            if (isSearching) newFullPath = lstfileName;
            if (phone.IsDirectory(newFullPath))
            {
                if (canRemovePath)
                {
                    /*
                    This removes previous paths when we go back and traverse through a different route.
                    */
                    int index = recentPaths.IndexOf(fullpath) + 1;
                    int c = recentPaths.Count;
                    //count changes after item removal so we get constant here
                    for (int i = index; i < c; i++)
                        recentPaths.RemoveAt(index);
                    canRemovePath = false;
                }

                if (fullpath == "/")
                    fullpath += lstfileName;
                else
                    fullpath += "/" + lstfileName;
                recentPaths.Add(fullpath);
                pathCount++;
                btnBack.Enabled = true;
                btnForward.Enabled = false;
                loadItems(fullpath);
            }
            else if (phone.IsFile(newFullPath))
            {
                status.Text = "Opening " + lstfileName;
                phoneSrc = newFullPath;
                pcPath = temp + Path.GetRandomFileName() + Path.GetExtension(lstfileName);
                copyWorkerPcDest = pcPath;
                openFile = true;
                pathsToCopyList.Add(phoneSrc, pcPath);
                StartCopyWorkerFromPhone();
            }
        }
        private void findFiles(string path, string searchTerm)
        {
            foreach (string file in phone.GetFiles(path))
            {
                if (search.CancellationPending)
                    break;
                else
                {
                    if (file.ToLower().Contains(searchTerm))
                    {
                        Item i = new Item(notifyEventHandler);
                        addFilesToItem(i, path + "/" + file, true);
                    }
                }
            }
            foreach (string dir in phone.GetDirectories(path))
            {
                if (search.CancellationPending)
                    break;
                else
                    findFiles(path + "/" + dir, searchTerm);
            }
        }
        private void addFilesToItem(Item i, string fileFullPath, bool isSearching)
        {
            if ((getItems != null && !getItems.CancellationPending) || (search != null && !search.CancellationPending))
            {
                string fileName;
                if (isSearching)
                    fileName = fileFullPath;
                else
                    fileName = Path.GetFileName(fileFullPath);

                FileOrFolder ff = new FileOrFolder(
                                      ShellEx.GetFileIcon(fileName, ShellEx.IconSizeEnum.LargeIcon48),
                                      fileName,
                                      readableFileSize(phone.FileSize(fileFullPath)),
                                      ShellEx.GetFileTypeDescription(fileName),
                                      phone.GetModifiedTime(fileFullPath)
                                  );
                i.Add(ff);
            }
        }

        private void showStatusInfo()
        {
            Disk d = phone.GetDiskStats();
            diskSpace.Text = "Free space: " + d.TotalUserAvailable + " / " + d.TotalUserCapacity;

            if (listView1.SelectedItems.Count > 0)
            {
                ulong s = 0;
                int filecount = 0;
                string path;
                foreach (ListViewItem l in listView1.SelectedItems)
                {
                    path = isSearching ? l.Text : fullpath + "/" + l.Text;
                    if (phone.IsFile(path))
                    {
                        filecount++;
                        s += phone.FileSize(path);
                    }
                }
                string s2 = listView1.SelectedItems.Count + " selected / " + listView1.Items.Count;
                string count = (listView1.Items.Count == 1) ? " item" : " items";
                listViewItems.Text = s2 + count;
                if (filecount > 0) //no point showing zero for folders
                    listViewItems.Text += " (" + readableFileSize(s) + ")";
            }
            else
            {
                string count = (listView1.Items.Count == 1) ? " item" : " items";
                listViewItems.Text = listView1.Items.Count + count;
            }
        }

        private bool isImage(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".tiff":
                    return true;
                default:
                    return false;
            }
        }
        private void clearSearchBox()
        {
            searchBox.Font = new Font("Microsoft Sans Serif", 16.0f, FontStyle.Italic);
            searchBox.ForeColor = SystemColors.WindowFrame;
            searchBox.Text = "Search..";
        }

        private void EnableControls()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
            pathBox.Enabled = true;
            searchBox.Enabled = true;
            btnEject.Enabled = true;
        }
        private void DisableControls()
        {
            listView1.Enabled = false;
            pathBox.Enabled = false;
            searchBox.Enabled = false;
            btnBack.Enabled = false;
            btnForward.Enabled = false;
            btnEject.Enabled = false;
            cancelSearch.Enabled = false;
            recentPaths.Clear();
        }

        private void GetFileCount(string path)
        {
            if (phone.IsFile(path))
                fileCount++;
            else
            {
                foreach (string f in phone.GetFiles(path)) fileCount++;
                foreach (string d in phone.GetDirectories(path)) GetFileCount(path + "/" + d);
            }
        }
        private void cancelWorkers()
        {
            if (search != null && search.IsBusy && search.WorkerSupportsCancellation)
            {
                search.CancelAsync();
                isSearching = false;
                cancelSearch.Enabled = false;
                searchBox.Clear();
            }
            if (getItems != null && getItems.IsBusy && getItems.WorkerSupportsCancellation)
                getItems.CancelAsync();
        }

        private void CopyData(string source, string dest, CopyType ct)
        {
            if (ct == CopyType.PhoneToPc)
            {
                // Copying from phone to PC
                Console.WriteLine("Copying from {0} to {1}. Type is {2}", source, dest, ct);
                if (phone.IsDirectory(source))
                {
                    Directory.CreateDirectory(dest);
                    foreach (string file in phone.GetFiles(source))
                        CopyData(source + "/" + file, dest + "/" + file, ct);
                    foreach (string dir in phone.GetDirectories(source))
                    {
                        Directory.CreateDirectory(dest + "/" + dir);
                        CopyData(source + "/" + dir, dest + "/" + dir, ct);
                    }
                }
                else if (phone.IsFile(source))
                {
                    CopyFile(source, dest, ct);
                }
            }
            else
            {
                // Copying from PC to phone
                Console.WriteLine("Copying from {0} to {1}. Type is {2}", source, dest, ct);

                if (Directory.Exists(source))
                {
                    phone.CreateDirectory(dest);
                    foreach (string file in Directory.GetFiles(source))
                        CopyData(source + "/" + file, dest, CopyType.PcToPhone);
                    foreach (string dir in Directory.GetDirectories(source))
                    {
                        phone.CreateDirectory(dest + "/" + dir);
                        CopyData(source + "/" + dir, dest + "/" + dir, CopyType.PcToPhone);
                    }
                }
                else if (File.Exists(source))
                {
                    string fileName = Path.GetFileName(source);
                    if (phone.Exists(dest + "/" + fileName))
                    {
                        if (MessageBox.Show(string.Format("The file '{0}' already exists. Are you sure you want to overwrite it?", fileName),
                                            "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            CopyFile(source, dest + "/" + fileName, CopyType.PcToPhone);
                    }
                    else CopyFile(source, dest + "/" + fileName, CopyType.PcToPhone);
                }
            }
        }
        private void CopyFile(string src, string dest, CopyType ct)
        {
            byte[] buffer;
            ulong fileSize = phone.FileSize(src);
            if (fileSize < (32 * 1024))
                buffer = new byte[fileSize];
            else
                buffer = new byte[32 * 1024]; //copy in 32kB chunks
            int bytesRead = 0;
            long totalBytesRead = 0;

            using (Stream inputReadStream = (ct == CopyType.PhoneToPc) ? (Stream)iPhoneFile.OpenRead(phone, src) : File.OpenRead(src))
            {
                if (inputReadStream != null)
                {
                    using (Stream outputWriteStream = (ct == CopyType.PhoneToPc) ? File.OpenWrite(dest) : (Stream)iPhoneFile.OpenWrite(phone, dest))
                    {
                        while ((bytesRead = inputReadStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            outputWriteStream.Write(buffer, 0, buffer.Length);
                            totalBytesRead += bytesRead;
                        }
                    }
                }
            }
        }

        private Stream getStreamForFile(Stream stream, string fileName)
        {
            phoneSrc = isSearching ? fileName : fullpath + "/" + fileName;
            long fileLen = (long)phone.FileSize(phoneSrc);

            byte[] buffer;
            if (fileLen < (32 * 1024))
                buffer = new byte[fileLen];
            else
                buffer = new byte[32 * 1024]; //copy in 32kB chunks

            int bytesRead = 0;
            long totalBytesRead = 0;
            using (iPhoneFile input = iPhoneFile.OpenRead(phone, phoneSrc))
            {
                if (input != null)
                {
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        totalBytesRead += bytesRead;
                    }
                    return stream;
                }
                return (Stream)null;
            }
        }

        private void CreateDeviceButton(Device d)
        {
            ListViewItem lv = new ListViewItem(d.Name + "\niOS " + d.Version);
            lv.Name = d.Handle.ToString();
            deviceListSideBar.Items.Add(lv);
            deviceListSideBar.Visible = true;
        }

        //Right click menu
        private void SaveToPc(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog();
            fsd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (fsd.ShowDialog())
            {
                pathsToCopyList.Clear();
                foreach (ListViewItem l in listView1.SelectedItems)
                {
                    pathsToCopyList.Add(fullpath + "/" + l.Text, fsd.SelectedPath + "/" + l.Text);
                }
                StartCopyWorkerFromPhone();
            }
        }
        private void RenameItem(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListViewItem lv = listView1.SelectedItems[0];
                lv.BeginEdit();
            }
        }
        private void DeleteItem(object sender, EventArgs e)
        {
            string msg;
            if (listView1.SelectedItems.Count == 1)
                msg = "'" + listView1.SelectedItems[0].Text + "'";
            else
                msg = "these " + listView1.SelectedItems.Count + " items";
            if (MessageBox.Show("Are you sure you want to delete " + msg + "?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                status.Text = "Deleting";
                listView1.BeginUpdate();
                foreach (ListViewItem l in listView1.SelectedItems)
                {
                    phone.Delete(fullpath + "/" + l.Text);
                    listView1.Items.Remove(l);
                }
                listView1.EndUpdate();
                status.Text = "Ready";

                showStatusInfo();
            }
        }
        private void CreateFolder(object sender, EventArgs e)
        {
            phone.CreateDirectory(fullpath + "/New Folder");
            ListViewItem lv = new ListViewItem("New Folder", 0);
            lv.SubItems.Add("");
            lv.SubItems.Add("Folder");
            lv.SubItems.Add(DateTime.Now.ToString());
            listView1.Items.Add(lv);
            lv.Selected = true;
            lv.BeginEdit();
        }
        private void CreateEmptyFile(object sender, EventArgs e)
        {
            using (iPhoneFile ifile = iPhoneFile.OpenWrite(phone, fullpath + "/test"))
            {
                ifile.WriteByte(0);
            }
            loadItems(fullpath);
        }
        private void CopyPath(object sender, EventArgs e)
        {
            Clipboard.SetText((isSearching) ? lstfileName : fullpath + "/" + lstfileName);
        }
        private void PasteFile(object sender, EventArgs e)
        {
            status.Text = "Copying";
            StringCollection sc = Clipboard.GetFileDropList();
            foreach (string s in sc)
            {
                CopyData(s, fullpath, CopyType.PcToPhone);
            }
            Clipboard.Clear();
            loadItems(fullpath);
            status.Text = "Ready";
        }
        private void ItemProperties(object sender, EventArgs e)
        {
            new ItemProp(phone, fullpath, listView1.SelectedItems).ShowDialog();
        }
        private void ClearTemp(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(temp);
            foreach (FileInfo file in d.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in d.GetDirectories())
                dir.Delete(true);
            MessageBox.Show("Done!");
        }
        private void Refresh(object sender, EventArgs e)
        {
            cancelWorkers();
            loadItems(fullpath);
        }


        //Buttons 
        private void Sidebar(object sender, EventArgs e)
        {
            if (deviceListSideBar.Visible)
                deviceListSideBar.Visible = false;
            else
                deviceListSideBar.Visible = true;
        }
        private void DeviceSelected(object sender, MouseEventArgs e)
        {
            ListViewItem lv = deviceListSideBar.GetItemAt(e.X, e.Y);
            Device d = (Device)devs[lv.Index];
            ConnectError c = phone.ConnectViaDevice(d.Handle);
            if (c == ConnectError.None)
            {
                deviceListSideBar.Visible = false;
                currDev = d.Handle;
                loadItems("/");
                connStatus.Text = "Connected";

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += PollBatteryWorker;
                bw.ProgressChanged += BatteryChargeProgress;
                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync();

                EnableControls();
                recentPaths.Clear();
                btnBack.Enabled = false;
                btnForward.Enabled = false;
                recentPaths.Add("/");
                status.Text = "Ready";
            }
            else MessageBox.Show(string.Format("Error: {0}", c.ToString()));
        }
        private void Back(object sender, EventArgs e)
        {
            //cancel async loading of long lists, as they continue adding when in different levels
            cancelWorkers();
            canRemovePath = true;
            if (pathCount > 0)
            {
                pathCount--;
                if (pathCount == 0) btnBack.Enabled = false;
            }
            string newPath = recentPaths[pathCount];
            loadItems(newPath);
            btnForward.Enabled = true;
        }
        private void Forward(object sender, EventArgs e)
        {
            if (pathCount < recentPaths.Count - 1)
            {
                pathCount++;
                if (pathCount == recentPaths.Count - 1) btnForward.Enabled = false;
                btnBack.Enabled = true;
            }
            //else btnForward.Enabled = false;
            loadItems(recentPaths[pathCount]);
        }
        private void Eject(object sender, EventArgs e)
        {
            phone.Eject();

            int i = 0;
            //remove disconnected device from array
            foreach (Device d in devs)
            {
                if (d.Handle == currDev)
                {
                    devs.Remove(d);
                    break;
                }
                i++;
            }

            listView1.Items.Clear();
            connStatus.Text = "Disconnected";
            batteryStatus.Text = "Not charging";
            diskSpace.Text = "Free space:";
            listViewItems.Text = "0 items";
            pathBox.Clear();

            DisableControls();
            currDev = IntPtr.Zero;
        }
        private void DeviceInfo(object sender, EventArgs e)
        {
            string s = phone.GetDetailedInfo();
            if (s != (string)null)
                MessageBox.Show(s, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CancelSearch(object sender, EventArgs e)
        {
            searchBox.Clear();
            cancelSearch.Enabled = false;
            clearSearchBox();
            cancelWorkers();
            loadItems(fullpath);
        }
        private void btnListView_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            btnListView.Enabled = false;
            btnThumbView.Enabled = true;
        }
        private void btnThumbView_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            btnListView.Enabled = true;
            btnThumbView.Enabled = false;
        }


        //Listview
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && listView1.SelectedItems.Count > 0)
                delete.PerformClick();
            else if (e.KeyCode == Keys.F2 && listView1.SelectedItems.Count == 1)
                rename.PerformClick();
            else if (e.KeyCode == Keys.Enter && listView1.SelectedItems.Count == 1)
                itemDblClick();
            else if (e.KeyCode == Keys.F5)
                menuRefresh.PerformClick();
            else if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem i in listView1.Items)
                    i.Selected = true;
            }
            else if (e.KeyCode == Keys.N && e.Control && e.Shift)
                createFolder.PerformClick();
        }
        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                showStatusInfo();
        }
        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string old = listView1.Items[e.Item].Text;
            if (e.Label != String.Empty)
            {
                phone.Rename(fullpath + "/" + old, fullpath + "/" + e.Label);
                listView1.Items[e.Item].Text = e.Label; //just update viewable text, don't reload path
            }
            else
            {
                MessageBox.Show("File name cannot be empty.");
                listView1.Items[e.Item].Text = old;
            }
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                listView1.Sorting = SortOrder.Descending;
            }
            else
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.None;
                else if (listView1.Sorting == SortOrder.Descending)
                    listView1.Sorting = SortOrder.Ascending;
                else
                    listView1.Sorting = SortOrder.Descending;
            }
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
            listView1.SetSortIcon(e.Column, listView1.Sorting);
            listView1.EnableDoubleBuffer();
        }
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                lstfileName = e.Item.Text;
                if (isSearching)
                    phoneSrc = lstfileName;
                else
                    phoneSrc = fullpath + "/" + lstfileName;
            }
        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string dest = fullpath;
            status.Text = "Copying";
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string item in s)
            {
                CopyData(item, dest, CopyType.PcToPhone);
            }
            status.Text = "Ready";
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            itemDblClick();
        }
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<VirtualFileDataObject.FileDescriptor> vList = new List<VirtualFileDataObject.FileDescriptor>();
            foreach (ListViewItem l in listView1.SelectedItems)
            {
                VirtualFileDataObject.FileDescriptor vfd = new VirtualFileDataObject.FileDescriptor
                {
                    Name = Path.GetFileName(isSearching ? l.Text : fullpath + "/" + l.Text),
                    StreamContents = stream =>
                    {
                        getStreamForFile(stream, l.Text);
                    }
                };
                vList.Add(vfd);
            }
            VirtualFileDataObject virtualData = new VirtualFileDataObject(null, null);
            virtualData.SetData(vList);
            VirtualFileDataObject.DoDragDrop(virtualData, System.Windows.DragDropEffects.Copy);
        }
        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {//Highlight items when mouse moving over them
            ListViewItem lvi = listView1.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                if (i == null)
                {
                    i = lvi;
                    lvi.BackColor = Color.LightBlue;
                }
                else
                {
                    i.BackColor = Color.White;
                    lvi.BackColor = Color.LightBlue;
                    i = lvi;
                }
            }
            else
            {
                if (i != null)
                    i.BackColor = Color.White;
            }
        }
        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            if (i != null)
                i.BackColor = Color.White;
        }
        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons != MouseButtons.Left)
                showStatusInfo();
        }

        
        //Events
        private void rightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            /*
             * 0 = save
             * 2 = rename
             * 3 = delete
             * 4 = refresh
             * 6 = create
             * 8 = copy path
             * 10 = paste
             * 12 = new
             * 14 = properties
             * 16 = clear temp
             * */
            if (Clipboard.ContainsFileDropList())
                rightClickFiles.Items[10].Enabled = true;
            if (listView1.SelectedItems.Count == 0)
            {
                rightClickFiles.Items[0].Enabled = false;//save
                rightClickFiles.Items[2].Enabled = false;//rename
                rightClickFiles.Items[3].Enabled = false;//delete
                rightClickFiles.Items[6].Enabled = true;//create folder
                rightClickFiles.Items[8].Enabled = false;//copy path 
                rightClickFiles.Items[12].Enabled = true;//new
            }
            else if (listView1.SelectedItems.Count == 1)
            {
                rightClickFiles.Items[0].Enabled = true;//save
                rightClickFiles.Items[2].Enabled = true;//rename
                rightClickFiles.Items[3].Enabled = true;//delete
                rightClickFiles.Items[6].Enabled = false;//create folder 
                rightClickFiles.Items[8].Enabled = true;//copy path
                rightClickFiles.Items[12].Enabled = false;//new
            }
            else //more than 1 selected
            {
                rightClickFiles.Items[0].Enabled = true;//save
                rightClickFiles.Items[2].Enabled = false;//rename
                rightClickFiles.Items[3].Enabled = true;//delete
                rightClickFiles.Items[6].Enabled = false;//create folder
                rightClickFiles.Items[8].Enabled = false;//copy path
                rightClickFiles.Items[12].Enabled = false;//new
            }
        }
        private void phoneDisconnect(object sender, ConnectEventArgs e)
        {
            IntPtr handle = e.Device;
            int i = 0;
            //remove disconnected device from array
            foreach (Device d in devs)
            {
                if (d.Handle == handle)
                {
                    devs.Remove(d);
                    break;
                }
                i++;
            }
            if (currDev == handle)//are we removing the currently connected device   
            {
                this.Invoke(new Action(() =>
                {
                    ListViewItem[] item = deviceListSideBar.Items.Find(handle.ToString(), false);
                    if (item.Length == 1)
                        deviceListSideBar.Items.Remove(item[0]);
                    listView1.Clear();
                    connStatus.Text = "Disconnected";
                    batteryStatus.Text = "Not charging";
                    diskSpace.Text = "Free space:";
                    listViewItems.Text = "0 items";
                    pathBox.Clear();

                    DisableControls();
                }));
                currDev = IntPtr.Zero;
            }
            else
            {
                this.Invoke(new Action(() =>
                {

                }));
            }
            if (devs.Count == 0) currDev = IntPtr.Zero;
        }
        private void phoneConnect(object sender, ConnectEventArgs e)
        {
            Console.WriteLine("Connected");
            Device d = new Device();
            d.Name = phone.DeviceName;
            d.Version = phone.DeviceVersion;
            d.Handle = e.Device;
            devs.Add(d);
            //keep connected to current device when device is connected
            if (currDev != IntPtr.Zero)
            {
                ConnectError c;
                if ((c = phone.ConnectViaDevice(currDev)) != ConnectError.None)
                    MessageBox.Show(string.Format("Couldn't connect. Error in {0}", c));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    CreateDeviceButton(d);
                }));
            }
        }
        private void pathBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newPath = phone.FullPath("/", pathBox.Text);
                if (phone.IsDirectory(newPath))
                {
                    cancelWorkers();
                    if (pathBox.Text == "/" || pathBox.Text == "")
                        btnBack.Enabled = false;
                    else
                    {
                        if (!pathBox.Text.StartsWith("/"))
                            pathBox.Text = "/" + pathBox.Text;
                        btnBack.Enabled = true;
                    }
                    loadItems(newPath);
                    pathBox.SelectionStart = pathBox.Text.Length;
                }
                else
                {
                    MessageBox.Show("Invalid path");
                    pathBox.Text = fullpath;
                }
            }
        }
        private void searchBoxFocussed(object sender, EventArgs e)
        {
            if (!isSearching)
            {
                searchBox.Clear();
                searchBox.Font = new Font("Microsoft Sans Serif", 16.0f, FontStyle.Regular);
                searchBox.ForeColor = SystemColors.WindowText;
            }
        }
        private void searchBoxLostFocus(object sender, EventArgs e)
        {
            if (!isSearching)
                clearSearchBox();
        }
        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cancelWorkers();
                search = new BackgroundWorker();
                search.DoWork += search_doWork;
                search.RunWorkerCompleted += searchCompleted;
                search.WorkerSupportsCancellation = true;
                status.Text = "Searching";
                listViewItems.Text = "0 items";
                listView1.Items.Clear();
                cancelSearch.Enabled = true;
                isSearching = true;
                if (!search.IsBusy)
                    search.RunWorkerAsync();
            }
        }
        protected override bool ProcessTabKey(bool forward)
        {
            if (pathBox.Focus() && pathBox.Text.StartsWith("/"))
            {
                string[] items = pathBox.Text.Split('/');
                string firstChars = items[items.Length - 1];//user-entered chars
                string tempFullPath = pathBox.Text.Remove(pathBox.Text.Length - firstChars.Length - 1);//full path to look for completions in
                string[] dirs = phone.GetDirectories(tempFullPath);//list of dirs in searchable path
                if (dirs.Length == 1)
                    pathBox.Text = tempFullPath + "/" + dirs[0] + "/";//if only one entry, complete to it
                else
                {
                    if (firstChars != "")//check there's something entered
                    {
                        char[] letters = firstChars.ToCharArray();//split entered string to chars
                        ArrayList possibles = new ArrayList();
                        foreach (string dir in dirs)//possible dirs in working dir
                        {
                            if (dir.Length >= letters.Length)//check search string is longer or equal to length of test dir, otherwise can't be a match
                            {
                                if (dir.StartsWith(firstChars))
                                {
                                    if (!possibles.Contains(dir))
                                        possibles.Add(dir);
                                }
                            }
                        }
                        string newText = pathBox.Text.Remove(pathBox.Text.Length - firstChars.Length - 1);

                        if (possibles.Count == 1)
                        {
                            pathBox.Text = newText + "/" + possibles[0] + "/";
                        }

                        //if more than one possible, loop through to find the common chars. 
                        //e.g my-long-text and my-long-other -> my-long-
                        else
                        {
                            int min = 500;//max length of directory name, increase as required
                            string sh = "";
                            foreach (string s in possibles)
                            {
                                if (s.Length < min)
                                {
                                    sh = s;
                                    min = s.Length;
                                }
                            }
                            char[] newletters = sh.ToCharArray();
                            char[] common = new char[newletters.Length];
                            bool allok = true;
                            for (int i = 0; i < min; i++)
                            {
                                if (!allok)
                                    break;
                                foreach (string s2 in possibles)
                                {
                                    if (s2 != sh)
                                    {
                                        char[] myStr = s2.ToCharArray();
                                        if (myStr[i] == newletters[i])
                                            common[i] = myStr[i];
                                        else
                                        {
                                            allok = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            string comm = new string(common);
                            comm = comm.Replace("\0", "");
                            if (comm == "")
                                pathBox.Text = newText + "/" + firstChars;
                            else
                                pathBox.Text = newText + "/" + comm;
                        }
                    }
                }
                pathBox.Select(pathBox.Text.Length, 0);//set cursor to end of text
            }
            return false;
        }
        private void notifyEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Notifying");
            bool details = (listView1.View == View.Details);
            int count;
            if (details)
                count = imageList.Images.Count;
            else
                count = LargeImList.Images.Count;
            foreach (FileOrFolder f in e.NewItems)
            {
                if (getItems != null && getItems.IsBusy && getItems.CancellationPending)
                    break;
                else if (search != null && search.IsBusy && search.CancellationPending)
                    break;
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        Bitmap bmp = null;
                        if (f.Image != null)
                            bmp = f.Image;
                        else bmp = new Bitmap(THUMB_SIZE, THUMB_SIZE);
                        if (details)
                            imageList.Images.Add(bmp);
                        else
                            LargeImList.Images.Add(bmp);

                        ListViewItem lst = new ListViewItem(f.Name, count);
                        lst.SubItems.Add(f.Size);
                        lst.SubItems.Add(f.Type);
                        lst.SubItems.Add(f.Date.ToString());
                        count++;

                        listView1.Items.Add(lst);
                        string num = (listView1.SelectedItems.Count == 1) ? " item" : " items";
                        listViewItems.Text = listView1.Items.Count + num;
                    }));
                }
            }
        }


        //Backgroundworkers
        private void search_doWork(object sender, DoWorkEventArgs e)
        {
            if (search.CancellationPending)
                e.Cancel = true;
            else
                findFiles(fullpath, searchBox.Text.ToLower());
        }
        private void searchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewItems.Text = listView1.Items.Count.ToString() + " item(s)";
            status.Text = "Ready";
        }

        private void getItems_DoWork(object sender, DoWorkEventArgs e)
        {
            if (getItems.CancellationPending)
                e.Cancel = true;
            else
            {
                Item i = new Item(notifyEventHandler);
                string path = (string)e.Argument;
                foreach (string file in phone.GetFiles(path))
                {
                    if (getItems.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                        addFilesToItem(i, path + "/" + file, false);
                }
            }
        }
        private void getItemsComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                showStatusInfo();
                if (listView1.View == View.Details)
                {
                    listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                status.Text = "Ready";
            }
        }

        private void btnTerminal_Click(object sender, EventArgs e)
        {

        }

        private void PollBatteryWorker(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            while (true)
            {
                int charge = phone.GetBatteryPercentage();
                if (charge > 0)
                    bw.ReportProgress(charge);
                System.Threading.Thread.Sleep(10000);
            }
        }
        private void BatteryChargeProgress(object sender, ProgressChangedEventArgs e)
        {
            batteryStatus.Text = e.ProgressPercentage + "%";
        }

        private void CopyFromPhoneWorker(object sender, DoWorkEventArgs e)
        {
            foreach (KeyValuePair<string, string> kv in pathsToCopyList)
            {
                CopyData(kv.Key, kv.Value, CopyType.PhoneToPc);
            }
        }
        private void CopyFromPhoneProgress(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        private void CopyFromPhoneCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (openFile)
            {
                Process.Start(copyWorkerPcDest);
                openFile = false;
            }
            copyWorkerPcDest = "";
            status.Text = "Ready";
            progressBar.Visible = false;
        }

        private void CopyToPhoneWorker(object sender, DoWorkEventArgs e)
        {
            string[] args = (string[])e.Argument;
            string sourceOnPc = args[0];
            string destOnPhone = args[1];

            if (File.Exists(sourceOnPc))
            {
                byte[] buffer;
                long fileSize = new FileInfo(sourceOnPc).Length;
                if (fileSize < (32 * 1024))
                    buffer = new byte[fileSize];
                else
                    buffer = new byte[32 * 1024]; //copy in 32kB chunks
                BackgroundWorker bw = sender as BackgroundWorker;
                int bytesRead = 0;
                long totalBytesRead = 0;
                try
                {
                    using (FileStream input = File.OpenRead(sourceOnPc))
                    {
                        using (iPhoneFile iFile = iPhoneFile.OpenWrite(phone, destOnPhone))
                        {
                            if (iFile != null)
                            {
                                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    iFile.Write(buffer, 0, buffer.Length);
                                    totalBytesRead += bytesRead;
                                    int prog = (int)(double)(((double)totalBytesRead / (double)fileSize) * 100);
                                    bw.ReportProgress(prog);
                                }
                            }
                            else
                            {
                                iBrowser.ErrorChange = 5;
                                // iBrowser.stopCopy = true;
                            }
                        }
                    }
                }
                catch (IOException ie)
                {
                    MessageBox.Show(ie.Message, "Error");
                }
            }
        }
        private void CopyToPhoneProgress(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        private void CopyToPhoneCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;
            status.Text = "Ready";
            loadItems(fullpath);
        }


        //History drop down
        private void historyDropDownOpening(object sender, EventArgs e)
        {
            historyDropDown.DropDownItems.Clear();
            historyDropDown.AutoSize = true;
            int i = 0;
            foreach (string s in recentPaths)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(s);
                tsmi.ImageScaling = ToolStripItemImageScaling.None;
                if (i == pathCount) tsmi.Checked = true;
                tsmi.Click += tsmi_Click;
                tsmi.BackColor = Color.White;
                tsmi.DisplayStyle = ToolStripItemDisplayStyle.Text;
                historyDropDown.DropDownItems.Add(tsmi);
                i++;
            }
        }
        private void tsmi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = sender as ToolStripMenuItem;
            pathCount = historyDropDown.DropDownItems.IndexOf(tm);
            Console.WriteLine("Index of selection is {0}", pathCount);

            btnBack.Enabled = (pathCount == 0) ? false : true;
            btnForward.Enabled = (pathCount == recentPaths.Count - 1) ? false : true;

            loadItems(tm.Text);
        }
    }
}
