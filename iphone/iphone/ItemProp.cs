using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iphone
{
    public partial class ItemProp : Form
    {
        private iPhone phone;
        private long FSIZE = 0;
        private int numFiles = 0, numDir = 0;

        public ItemProp(iPhone p, string fullPath, ListView.SelectedListViewItemCollection list)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            phone = p;
            setControls();

            if (list.Count <= 1)
            {
                split.Height = 2;
                split.Width = 250;
                split.AutoSize = false;
                split.BorderStyle = BorderStyle.Fixed3D;
                split.Location = new Point(12, 160);

                split2.Height = 2;
                split2.Width = 250;
                split2.AutoSize = false;
                split2.BorderStyle = BorderStyle.Fixed3D;
                split2.Location = new Point(12, 40);

                string full, filename = "";
                if (list.Count == 0)
                {
                    full = fullPath;
                    this.Text = fullPath;
                    name.Text = "Name: " + fullPath;
                }
                else
                {
                    filename = Path.GetFileName(list[0].Text);
                    full = fullPath + "/" + list[0].Text;
                    this.Text = filename + " Properties";
                    name.Text = "Name: " + filename;
                }
                contents.Text = "";
                long filesize = (long)phone.FileSize(full);
                if (phone.IsDirectory(full))
                {
                    type.Text = "Type: Folder";
                    contents.Text = "Contains: " + numFiles + " files, " + numDir + " folders";
                    this.Height = 320;
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += bwDoWork;
                    bw.WorkerReportsProgress = true;
                    bw.ProgressChanged += bwProgress;
                    if (!bw.IsBusy) bw.RunWorkerAsync(full);
                }
                else
                {
                    type.Text = "Type: " + ShellEx.GetFileTypeDescription(filename);
                    dateMod.Location = new Point(12, 170);
                    dateBirth.Location = new Point(12, 210);
                    this.Height = 275;
                }
                size.Text = "Size: " + (filesize / 1024d / 1024d).ToString("0.00") + " MB (" + toCSV(filesize.ToString()) + " bytes)";
                try
                {
                    dateMod.Text = "Modified: " + phone.GetModifiedTime(full).ToString();
                    dateBirth.Text = "Created: " + phone.GetCreationTime(full).ToString();
                }
                catch (KeyNotFoundException)
                {

                }
                location.Text = "Location: " + fullPath;
            }
            else if (list.Count > 1)
            {
                split.Text = "";
                split2.Text = "";
                foreach (ListViewItem l in list)
                {
                    string filename = Path.GetFileName(l.Text);
                    string full = fullPath + "/" + l.Text;
                    name.Text = "";
                    dateMod.Text = "";
                    dateBirth.Text = "";
                    type.Location = new Point(12, 10);
                    size.Location = new Point(12, 50);
                    contents.Location = new Point(12, 90);
                    location.Location = new Point(12, 130);
                    type.Text = "Type: Multiple types";
                    long filesize = (long)phone.FileSize(full);
                    if (phone.IsDirectory(full))
                    {
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += bwDoWork;
                        bw.WorkerReportsProgress = true;
                        bw.ProgressChanged += bwProgress;
                        if (!bw.IsBusy) bw.RunWorkerAsync(full);
                    }
                    location.Text = "Location: " + fullPath;
                }
                this.Height = 200;
            }
        }
        private void setControls()
        {
            name.Location = new Point(12, 10);
            type.Location = new Point(12, 50);
            size.Location = new Point(12, 90);
            location.Location = new Point(12, 130);
            contents.Location = new Point(12, 170);
            dateMod.Location = new Point(12, 210);
            dateBirth.Location = new Point(12, 250);
        }
        private string toCSV(string size)
        {
            if (size.Length <= 3) return size;
            else
            {
                string s = "";
                char[] nums = size.ToCharArray();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (i != 0 && (nums.Length - i) % 3 == 0)
                        s += "," + nums[i].ToString();
                    else s += nums[i].ToString();
                }
                return s;
            }
        }
        private void bwDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string path = (string)e.Argument;
            foreach (string file in phone.GetFiles(path))
            {
                FSIZE += (long)phone.FileSize(path + "/" + file);
                numFiles++;
                bw.ReportProgress((int)FSIZE);
            }
            foreach (string dir in phone.GetDirectories(path))
            {
                numDir++;
                getDirSize(bw, path + "/" + dir);
            }
        }
        private void bwProgress(object sender, ProgressChangedEventArgs e)
        {
            int filesize = e.ProgressPercentage;
            size.Text = "Size: " + (filesize / 1024d / 1024d).ToString("0.00") + " MB (" + toCSV(filesize.ToString()) + " bytes)";
            contents.Text = "Contains: " + numFiles + " files, " + numDir + " folders";
        }
        private void getDirSize(BackgroundWorker bw, string path)
        {
            foreach (string file in phone.GetFiles(path))
            {
                FSIZE += (long)phone.FileSize(path + "/" + file);
                numFiles++;
                bw.ReportProgress((int)FSIZE);
            }
            foreach (string dir in phone.GetDirectories(path))
            {
                numDir++;
                getDirSize(bw, path + "/" + dir);
            }
        }
    }
}
