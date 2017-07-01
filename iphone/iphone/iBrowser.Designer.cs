namespace iphone
{
    partial class iBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(iBrowser));
            this.listView1 = new System.Windows.Forms.ListView();
            this.fileName1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateMod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightClickFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToPc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rename = new System.Windows.Forms.ToolStripMenuItem();
            this.delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.createFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.newEmptyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.clrTmp = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeImList = new System.Windows.Forms.ImageList(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.spacer1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.batteryStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.spacer2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.diskSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.spacer3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewItems = new System.Windows.Forms.ToolStripStatusLabel();
            this.spacer4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.spacer5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.navigationBarHelperLabel = new System.Windows.Forms.Label();
            this.searchBarHelperLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cancelSearch = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSidebar = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.historyDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.hiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEject = new System.Windows.Forms.ToolStripButton();
            this.btnThumbView = new System.Windows.Forms.ToolStripButton();
            this.btnListView = new System.Windows.Forms.ToolStripButton();
            this.btnDeviceInfo = new System.Windows.Forms.Button();
            this.deviceListSideBar = new System.Windows.Forms.ListView();
            this.btnTerminal = new System.Windows.Forms.Button();
            this.rightClickFiles.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName1,
            this.size,
            this.type,
            this.dateMod});
            this.listView1.ContextMenuStrip = this.rightClickFiles;
            this.listView1.Enabled = false;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.FullRowSelect = true;
            this.listView1.LabelEdit = true;
            this.listView1.LargeImageList = this.LargeImList;
            this.listView1.Location = new System.Drawing.Point(0, 125);
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(1032, 332);
            this.listView1.SmallImageList = this.imageList;
            this.listView1.TabIndex = 20;
            this.listView1.TabStop = false;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseLeave += new System.EventHandler(this.listView1_MouseLeave);
            this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // fileName1
            // 
            this.fileName1.Text = "Name";
            this.fileName1.Width = 164;
            // 
            // size
            // 
            this.size.Text = "Size";
            this.size.Width = 100;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 150;
            // 
            // dateMod
            // 
            this.dateMod.Text = "Date Modified";
            this.dateMod.Width = 160;
            // 
            // rightClickFiles
            // 
            this.rightClickFiles.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rightClickFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToPc,
            this.toolStripSeparator1,
            this.rename,
            this.delete,
            this.menuRefresh,
            this.toolStripSeparator12,
            this.createFolder,
            this.toolStripSeparator3,
            this.copyPath,
            this.toolStripSeparator4,
            this.paste,
            this.toolStripSeparator6,
            this.toolStripMenuItem1,
            this.toolStripSeparator11,
            this.properties,
            this.toolStripSeparator5,
            this.clrTmp});
            this.rightClickFiles.Name = "rightClickMenu";
            this.rightClickFiles.ShowImageMargin = false;
            this.rightClickFiles.Size = new System.Drawing.Size(132, 266);
            this.rightClickFiles.Opening += new System.ComponentModel.CancelEventHandler(this.rightClickMenu_Opening);
            // 
            // saveToPc
            // 
            this.saveToPc.Name = "saveToPc";
            this.saveToPc.Size = new System.Drawing.Size(131, 22);
            this.saveToPc.Text = "Save to PC";
            this.saveToPc.Click += new System.EventHandler(this.SaveToPc);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // rename
            // 
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(131, 22);
            this.rename.Text = "Rename";
            this.rename.Click += new System.EventHandler(this.RenameItem);
            // 
            // delete
            // 
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(131, 22);
            this.delete.Text = "Delete";
            this.delete.Click += new System.EventHandler(this.DeleteItem);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(131, 22);
            this.menuRefresh.Text = "Refresh";
            this.menuRefresh.Click += new System.EventHandler(this.Refresh);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(128, 6);
            // 
            // createFolder
            // 
            this.createFolder.Name = "createFolder";
            this.createFolder.Size = new System.Drawing.Size(131, 22);
            this.createFolder.Text = "Create folder";
            this.createFolder.Click += new System.EventHandler(this.CreateFolder);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
            // 
            // copyPath
            // 
            this.copyPath.Name = "copyPath";
            this.copyPath.Size = new System.Drawing.Size(131, 22);
            this.copyPath.Text = "Copy path";
            this.copyPath.Click += new System.EventHandler(this.CopyPath);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(128, 6);
            // 
            // paste
            // 
            this.paste.Enabled = false;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(131, 22);
            this.paste.Text = "Paste";
            this.paste.Click += new System.EventHandler(this.PasteFile);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(128, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolder,
            this.newEmptyFile});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.toolStripMenuItem1.Text = "New";
            // 
            // newFolder
            // 
            this.newFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newFolder.Name = "newFolder";
            this.newFolder.Size = new System.Drawing.Size(127, 22);
            this.newFolder.Text = "Folder";
            this.newFolder.Click += new System.EventHandler(this.CreateFolder);
            // 
            // newEmptyFile
            // 
            this.newEmptyFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newEmptyFile.Name = "newEmptyFile";
            this.newEmptyFile.Size = new System.Drawing.Size(127, 22);
            this.newEmptyFile.Text = "Empty file";
            this.newEmptyFile.Click += new System.EventHandler(this.CreateEmptyFile);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(128, 6);
            // 
            // properties
            // 
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(131, 22);
            this.properties.Text = "Properties";
            this.properties.Click += new System.EventHandler(this.ItemProperties);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(128, 6);
            // 
            // clrTmp
            // 
            this.clrTmp.Name = "clrTmp";
            this.clrTmp.Size = new System.Drawing.Size(131, 22);
            this.clrTmp.Text = "Clear temp files";
            this.clrTmp.Click += new System.EventHandler(this.ClearTemp);
            // 
            // LargeImList
            // 
            this.LargeImList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.LargeImList.ImageSize = new System.Drawing.Size(64, 64);
            this.LargeImList.TransparentColor = System.Drawing.Color.Maroon;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "foldericonsmall.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connStatus,
            this.spacer1,
            this.batteryStatus,
            this.spacer2,
            this.diskSpace,
            this.spacer3,
            this.listViewItems,
            this.spacer4,
            this.status,
            this.spacer5,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1032, 22);
            this.statusStrip1.TabIndex = 49;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connStatus
            // 
            this.connStatus.Name = "connStatus";
            this.connStatus.Size = new System.Drawing.Size(79, 17);
            this.connStatus.Text = "Disconnected";
            // 
            // spacer1
            // 
            this.spacer1.Name = "spacer1";
            this.spacer1.Size = new System.Drawing.Size(25, 17);
            this.spacer1.Text = "      ";
            // 
            // batteryStatus
            // 
            this.batteryStatus.Name = "batteryStatus";
            this.batteryStatus.Size = new System.Drawing.Size(77, 17);
            this.batteryStatus.Text = "Not charging";
            // 
            // spacer2
            // 
            this.spacer2.Name = "spacer2";
            this.spacer2.Size = new System.Drawing.Size(25, 17);
            this.spacer2.Text = "      ";
            // 
            // diskSpace
            // 
            this.diskSpace.Name = "diskSpace";
            this.diskSpace.Size = new System.Drawing.Size(65, 17);
            this.diskSpace.Text = "Free space:";
            // 
            // spacer3
            // 
            this.spacer3.Name = "spacer3";
            this.spacer3.Size = new System.Drawing.Size(25, 17);
            this.spacer3.Text = "      ";
            // 
            // listViewItems
            // 
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(45, 17);
            this.listViewItems.Text = "0 items";
            // 
            // spacer4
            // 
            this.spacer4.Name = "spacer4";
            this.spacer4.Size = new System.Drawing.Size(25, 17);
            this.spacer4.Text = "      ";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(39, 17);
            this.status.Text = "Ready";
            // 
            // spacer5
            // 
            this.spacer5.Name = "spacer5";
            this.spacer5.Size = new System.Drawing.Size(25, 17);
            this.spacer5.Text = "      ";
            this.spacer5.ToolTipText = "      ";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(125, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Visible = false;
            // 
            // pathBox
            // 
            this.pathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBox.Enabled = false;
            this.pathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathBox.Location = new System.Drawing.Point(6, 3);
            this.pathBox.MinimumSize = new System.Drawing.Size(100, 4);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(505, 31);
            this.pathBox.TabIndex = 62;
            this.pathBox.TabStop = false;
            this.pathBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathBox_KeyDown);
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Enabled = false;
            this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.searchBox.Location = new System.Drawing.Point(3, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(471, 31);
            this.searchBox.TabIndex = 84;
            this.searchBox.TabStop = false;
            this.searchBox.Text = "Search..";
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // navigationBarHelperLabel
            // 
            this.navigationBarHelperLabel.AutoSize = true;
            this.navigationBarHelperLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.navigationBarHelperLabel.Location = new System.Drawing.Point(3, 37);
            this.navigationBarHelperLabel.Name = "navigationBarHelperLabel";
            this.navigationBarHelperLabel.Size = new System.Drawing.Size(185, 13);
            this.navigationBarHelperLabel.TabIndex = 81;
            this.navigationBarHelperLabel.Text = "TAB to auto complete, ENTER to go";
            // 
            // searchBarHelperLabel
            // 
            this.searchBarHelperLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBarHelperLabel.AutoSize = true;
            this.searchBarHelperLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchBarHelperLabel.Location = new System.Drawing.Point(3, 37);
            this.searchBarHelperLabel.Name = "searchBarHelperLabel";
            this.searchBarHelperLabel.Size = new System.Drawing.Size(89, 13);
            this.searchBarHelperLabel.TabIndex = 87;
            this.searchBarHelperLabel.Text = "ENTER to search";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.navigationBarHelperLabel);
            this.splitContainer1.Panel1.Controls.Add(this.pathBox);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.cancelSearch);
            this.splitContainer1.Panel2.Controls.Add(this.searchBarHelperLabel);
            this.splitContainer1.Panel2.Controls.Add(this.searchBox);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(1032, 64);
            this.splitContainer1.SplitterDistance = 514;
            this.splitContainer1.TabIndex = 101;
            this.splitContainer1.TabStop = false;
            // 
            // cancelSearch
            // 
            this.cancelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelSearch.BackColor = System.Drawing.Color.White;
            this.cancelSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cancelSearch.Enabled = false;
            this.cancelSearch.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.cancelSearch.FlatAppearance.BorderSize = 0;
            this.cancelSearch.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cancelSearch.Location = new System.Drawing.Point(480, 3);
            this.cancelSearch.Name = "cancelSearch";
            this.cancelSearch.Size = new System.Drawing.Size(31, 31);
            this.cancelSearch.TabIndex = 88;
            this.cancelSearch.Text = "X";
            this.cancelSearch.UseVisualStyleBackColor = true;
            this.cancelSearch.Click += new System.EventHandler(this.CancelSearch);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSidebar,
            this.toolStripLabel3,
            this.btnBack,
            this.btnForward,
            this.historyDropDown,
            this.btnEject,
            this.btnThumbView,
            this.btnListView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1032, 52);
            this.toolStrip1.TabIndex = 111;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSidebar
            // 
            this.btnSidebar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSidebar.Image = global::iphone.Properties.Resources.sidebar;
            this.btnSidebar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSidebar.Name = "btnSidebar";
            this.btnSidebar.Size = new System.Drawing.Size(36, 49);
            this.btnSidebar.Text = "toolStripButton1";
            this.btnSidebar.Click += new System.EventHandler(this.Sidebar);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(22, 49);
            this.toolStripLabel3.Text = "     ";
            // 
            // btnBack
            // 
            this.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBack.Enabled = false;
            this.btnBack.Image = global::iphone.Properties.Resources.back;
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(36, 49);
            this.btnBack.Text = "toolStripButton1";
            this.btnBack.ToolTipText = "Back";
            this.btnBack.Click += new System.EventHandler(this.Back);
            // 
            // btnForward
            // 
            this.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnForward.Enabled = false;
            this.btnForward.Image = global::iphone.Properties.Resources.forward;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(36, 49);
            this.btnForward.Text = "toolStripButton2";
            this.btnForward.ToolTipText = "Forward";
            this.btnForward.Click += new System.EventHandler(this.Forward);
            // 
            // historyDropDown
            // 
            this.historyDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.historyDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiToolStripMenuItem});
            this.historyDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.historyDropDown.Name = "historyDropDown";
            this.historyDropDown.Size = new System.Drawing.Size(13, 49);
            this.historyDropDown.Text = "toolStripDropDownButton1";
            this.historyDropDown.ToolTipText = "History";
            this.historyDropDown.DropDownOpening += new System.EventHandler(this.historyDropDownOpening);
            // 
            // hiToolStripMenuItem
            // 
            this.hiToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.hiToolStripMenuItem.Checked = true;
            this.hiToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hiToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hiToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hiToolStripMenuItem.Name = "hiToolStripMenuItem";
            this.hiToolStripMenuItem.Size = new System.Drawing.Size(84, 22);
            this.hiToolStripMenuItem.Text = "hi";
            // 
            // btnEject
            // 
            this.btnEject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEject.Enabled = false;
            this.btnEject.Image = global::iphone.Properties.Resources.eject;
            this.btnEject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEject.Name = "btnEject";
            this.btnEject.Size = new System.Drawing.Size(28, 49);
            this.btnEject.Text = "Eject";
            this.btnEject.Click += new System.EventHandler(this.Eject);
            // 
            // btnThumbView
            // 
            this.btnThumbView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnThumbView.Image = global::iphone.Properties.Resources.thumb;
            this.btnThumbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThumbView.Name = "btnThumbView";
            this.btnThumbView.Size = new System.Drawing.Size(52, 49);
            this.btnThumbView.Text = "   ";
            this.btnThumbView.ToolTipText = "Thumbnail view";
            this.btnThumbView.Click += new System.EventHandler(this.btnThumbView_Click);
            // 
            // btnListView
            // 
            this.btnListView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnListView.Enabled = false;
            this.btnListView.Image = global::iphone.Properties.Resources.detail;
            this.btnListView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnListView.Name = "btnListView";
            this.btnListView.Size = new System.Drawing.Size(52, 49);
            this.btnListView.Text = "   ";
            this.btnListView.ToolTipText = "Detail view";
            this.btnListView.Click += new System.EventHandler(this.btnListView_Click);
            // 
            // btnDeviceInfo
            // 
            this.btnDeviceInfo.Location = new System.Drawing.Point(202, 13);
            this.btnDeviceInfo.Name = "btnDeviceInfo";
            this.btnDeviceInfo.Size = new System.Drawing.Size(75, 26);
            this.btnDeviceInfo.TabIndex = 114;
            this.btnDeviceInfo.Text = "Device info";
            this.btnDeviceInfo.UseVisualStyleBackColor = true;
            this.btnDeviceInfo.Click += new System.EventHandler(this.DeviceInfo);
            // 
            // deviceListSideBar
            // 
            this.deviceListSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.deviceListSideBar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceListSideBar.Location = new System.Drawing.Point(0, 51);
            this.deviceListSideBar.Name = "deviceListSideBar";
            this.deviceListSideBar.Size = new System.Drawing.Size(115, 432);
            this.deviceListSideBar.TabIndex = 115;
            this.deviceListSideBar.TileSize = new System.Drawing.Size(100, 50);
            this.deviceListSideBar.UseCompatibleStateImageBehavior = false;
            this.deviceListSideBar.View = System.Windows.Forms.View.Tile;
            this.deviceListSideBar.Visible = false;
            this.deviceListSideBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DeviceSelected);
            // 
            // btnTerminal
            // 
            this.btnTerminal.Location = new System.Drawing.Point(284, 13);
            this.btnTerminal.Name = "btnTerminal";
            this.btnTerminal.Size = new System.Drawing.Size(75, 26);
            this.btnTerminal.TabIndex = 116;
            this.btnTerminal.Text = "Terminal";
            this.btnTerminal.UseVisualStyleBackColor = true;
            this.btnTerminal.Click += new System.EventHandler(this.btnTerminal_Click);
            // 
            // iBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1032, 482);
            this.Controls.Add(this.btnTerminal);
            this.Controls.Add(this.deviceListSideBar);
            this.Controls.Add(this.btnDeviceInfo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(789, 348);
            this.Name = "iBrowser";
            this.Text = "iBrowser";
            this.rightClickFiles.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ContextMenuStrip rightClickFiles;
        private System.Windows.Forms.ToolStripMenuItem saveToPc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem rename;
        private System.Windows.Forms.ToolStripMenuItem delete;
        private System.Windows.Forms.ToolStripMenuItem clrTmp;
        private System.Windows.Forms.ColumnHeader fileName1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ColumnHeader dateMod;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.ToolStripMenuItem createFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel diskSpace;
        private System.Windows.Forms.ToolStripStatusLabel connStatus;
        private System.Windows.Forms.ToolStripStatusLabel listViewItems;
        private System.Windows.Forms.ToolStripMenuItem copyPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label navigationBarHelperLabel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label searchBarHelperLabel;
        private System.Windows.Forms.ToolStripMenuItem properties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel spacer1;
        private System.Windows.Forms.ToolStripStatusLabel spacer2;
        private System.Windows.Forms.ToolStripStatusLabel spacer3;
        private System.Windows.Forms.ToolStripStatusLabel batteryStatus;
        private System.Windows.Forms.ToolStripStatusLabel spacer4;
        private System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ImageList LargeImList;
        private System.Windows.Forms.ToolStripMenuItem paste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel spacer5;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripButton btnEject;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newFolder;
        private System.Windows.Forms.ToolStripMenuItem newEmptyFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btnThumbView;
        private System.Windows.Forms.ToolStripButton btnListView;
        private System.Windows.Forms.ToolStripDropDownButton historyDropDown;
        private System.Windows.Forms.ToolStripMenuItem hiToolStripMenuItem;
        private System.Windows.Forms.Button cancelSearch;
        private System.Windows.Forms.Button btnDeviceInfo;
        private System.Windows.Forms.ToolStripButton btnSidebar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ListView deviceListSideBar;
        private System.Windows.Forms.Button btnTerminal;
    }
}

