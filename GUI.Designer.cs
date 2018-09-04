namespace NSHW
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.txtInForPacket = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new NSHW.AbortableBackgroundWorker();
            this.backgroundWorker2 = new NSHW.AbortableBackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedPacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedPacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnStop = new System.Windows.Forms.ToolStripButton();
            this.adapters_list = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grHeader = new System.Windows.Forms.GroupBox();
            this.grCenter = new System.Windows.Forms.GroupBox();
            this.grBottom = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grHeader.SuspendLayout();
            this.grCenter.SuspendLayout();
            this.grBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInForPacket
            // 
            this.txtInForPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInForPacket.Location = new System.Drawing.Point(3, 16);
            this.txtInForPacket.Multiline = true;
            this.txtInForPacket.Name = "txtInForPacket";
            this.txtInForPacket.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInForPacket.Size = new System.Drawing.Size(931, 126);
            this.txtInForPacket.TabIndex = 17;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader1});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(7, 20);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(923, 219);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 182;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Source";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 142;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Destination";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 142;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Protocol";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Length";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 105;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Info";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 198;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(937, 27);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveSelectedPacketToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // saveSelectedPacketToolStripMenuItem
            // 
            this.saveSelectedPacketToolStripMenuItem.Name = "saveSelectedPacketToolStripMenuItem";
            this.saveSelectedPacketToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveSelectedPacketToolStripMenuItem.Text = "Save Selected packet";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.deleteSelectedPacketToolStripMenuItem,
            this.findToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // deleteSelectedPacketToolStripMenuItem
            // 
            this.deleteSelectedPacketToolStripMenuItem.Name = "deleteSelectedPacketToolStripMenuItem";
            this.deleteSelectedPacketToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.deleteSelectedPacketToolStripMenuItem.Text = "Delete Selected Packet";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 23);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnOpen,
            this.toolStripSeparator1,
            this.tbtnCapture,
            this.toolStripSeparator2,
            this.tbtnPause,
            this.toolStripSeparator3,
            this.tbtnStop});
            this.toolStrip1.Location = new System.Drawing.Point(16, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(914, 35);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnOpen
            // 
            this.tbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnOpen.Image = global::NSHW.Properties.Resources.Folder;
            this.tbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnOpen.Name = "tbtnOpen";
            this.tbtnOpen.Size = new System.Drawing.Size(26, 32);
            this.tbtnOpen.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // tbtnCapture
            // 
            this.tbtnCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCapture.Image = global::NSHW.Properties.Resources.wifi_signal_tower;
            this.tbtnCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCapture.Name = "tbtnCapture";
            this.tbtnCapture.Size = new System.Drawing.Size(26, 32);
            this.tbtnCapture.Text = "Capture";
            this.tbtnCapture.Click += new System.EventHandler(this.tbtnCapture_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // tbtnPause
            // 
            this.tbtnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnPause.Image = global::NSHW.Properties.Resources.PauseNormal;
            this.tbtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPause.Name = "tbtnPause";
            this.tbtnPause.Size = new System.Drawing.Size(26, 32);
            this.tbtnPause.Text = "Pause";
            this.tbtnPause.Click += new System.EventHandler(this.tbtnPause_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // tbtnStop
            // 
            this.tbtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStop.Image = global::NSHW.Properties.Resources.Stop1NormalRed;
            this.tbtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStop.Name = "tbtnStop";
            this.tbtnStop.Size = new System.Drawing.Size(26, 32);
            this.tbtnStop.Text = "Stop";
            this.tbtnStop.Click += new System.EventHandler(this.tbtnStop_Click);
            // 
            // adapters_list
            // 
            this.adapters_list.FormattingEnabled = true;
            this.adapters_list.Location = new System.Drawing.Point(646, 61);
            this.adapters_list.Name = "adapters_list";
            this.adapters_list.Size = new System.Drawing.Size(291, 21);
            this.adapters_list.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(538, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Select device";
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(58, 66);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(329, 20);
            this.tbFilter.TabIndex = 36;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Filter";
            // 
            // grHeader
            // 
            this.grHeader.Controls.Add(this.toolStrip1);
            this.grHeader.Controls.Add(this.label7);
            this.grHeader.Controls.Add(this.tbFilter);
            this.grHeader.Controls.Add(this.adapters_list);
            this.grHeader.Controls.Add(this.label1);
            this.grHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.grHeader.Location = new System.Drawing.Point(20, 87);
            this.grHeader.Name = "grHeader";
            this.grHeader.Size = new System.Drawing.Size(937, 100);
            this.grHeader.TabIndex = 42;
            this.grHeader.TabStop = false;
            // 
            // grCenter
            // 
            this.grCenter.Controls.Add(this.listView1);
            this.grCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCenter.Location = new System.Drawing.Point(20, 187);
            this.grCenter.Name = "grCenter";
            this.grCenter.Size = new System.Drawing.Size(937, 391);
            this.grCenter.TabIndex = 43;
            this.grCenter.TabStop = false;
            this.grCenter.Text = "Danh sách các gói tin bắt được";
            // 
            // grBottom
            // 
            this.grBottom.Controls.Add(this.txtInForPacket);
            this.grBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grBottom.Location = new System.Drawing.Point(20, 433);
            this.grBottom.Name = "grBottom";
            this.grBottom.Size = new System.Drawing.Size(937, 145);
            this.grBottom.TabIndex = 44;
            this.grBottom.TabStop = false;
            this.grBottom.Text = "Thông tin gói tin";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 598);
            this.Controls.Add(this.grBottom);
            this.Controls.Add(this.grCenter);
            this.Controls.Add(this.grHeader);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.Text = "Chương trình bắt gói tin ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grHeader.ResumeLayout(false);
            this.grHeader.PerformLayout();
            this.grCenter.ResumeLayout(false);
            this.grBottom.ResumeLayout(false);
            this.grBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtInForPacket;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Timer timer1;
        private AbortableBackgroundWorker backgroundWorker1;
        private AbortableBackgroundWorker backgroundWorker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedPacketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedPacketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ComboBox adapters_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grHeader;
        private System.Windows.Forms.GroupBox grCenter;
        private System.Windows.Forms.GroupBox grBottom;
        private System.Windows.Forms.ToolStripButton tbtnOpen;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripButton tbtnCapture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbtnPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbtnStop;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

