namespace GridMapper
{
	partial class GridWindow
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.LoadScan = new System.Windows.Forms.ToolStripButton();
            this.SaveScan = new System.Windows.Forms.ToolStripButton();
            this.ProgressScan = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.normalScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MacAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ports = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.diagramDisplayControl1 = new GridMapper.DiagramDisplayControl();
            this.button1 = new System.Windows.Forms.Button();
            this.ScanButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadScan,
            this.SaveScan});
            this.toolStrip1.Location = new System.Drawing.Point(14, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(49, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // LoadScan
            // 
            this.LoadScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadScan.Image = ((System.Drawing.Image)(resources.GetObject("LoadScan.Image")));
            this.LoadScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadScan.Name = "LoadScan";
            this.LoadScan.Size = new System.Drawing.Size(23, 22);
            this.LoadScan.Text = "LoadScan";
            this.LoadScan.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // SaveScan
            // 
            this.SaveScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveScan.Image = ((System.Drawing.Image)(resources.GetObject("SaveScan.Image")));
            this.SaveScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveScan.Name = "SaveScan";
            this.SaveScan.Size = new System.Drawing.Size(23, 22);
            this.SaveScan.Text = "SaveScan";
            this.SaveScan.Click += new System.EventHandler(this.SaveScan_Click);
            // 
            // ProgressScan
            // 
            this.ProgressScan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressScan.Location = new System.Drawing.Point(0, 513);
            this.ProgressScan.Margin = new System.Windows.Forms.Padding(2);
            this.ProgressScan.Name = "ProgressScan";
            this.ProgressScan.Size = new System.Drawing.Size(603, 20);
            this.ProgressScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressScan.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.OptionToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalScanToolStripMenuItem,
            this.fastScanToolStripMenuItem,
            this.portScanToolStripMenuItem});
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem1.Text = "Start";
            // 
            // normalScanToolStripMenuItem
            // 
            this.normalScanToolStripMenuItem.Name = "normalScanToolStripMenuItem";
            this.normalScanToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.normalScanToolStripMenuItem.Text = "Normal scan";
            this.normalScanToolStripMenuItem.Click += new System.EventHandler(this.normalScanToolStripMenuItem_Click);
            // 
            // fastScanToolStripMenuItem
            // 
            this.fastScanToolStripMenuItem.Name = "fastScanToolStripMenuItem";
            this.fastScanToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.fastScanToolStripMenuItem.Text = "Fast scan";
            this.fastScanToolStripMenuItem.Click += new System.EventHandler(this.fastScanToolStripMenuItem_Click_1);
            // 
            // portScanToolStripMenuItem
            // 
            this.portScanToolStripMenuItem.Name = "portScanToolStripMenuItem";
            this.portScanToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.portScanToolStripMenuItem.Text = "Port scan";
            this.portScanToolStripMenuItem.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.startToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(603, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(179, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 25);
            this.panel1.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "IP Range",
            "CIDR",
            "Manual"});
            this.comboBox1.Location = new System.Drawing.Point(66, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(107, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 468);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(595, 442);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Grid View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IPAddress,
            this.MacAddress,
            this.HostName,
            this.Ports});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(591, 438);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 2;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // IPAddress
            // 
            this.IPAddress.HeaderText = "IP Address";
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.ReadOnly = true;
            // 
            // MacAddress
            // 
            this.MacAddress.HeaderText = "Mac Address";
            this.MacAddress.Name = "MacAddress";
            this.MacAddress.ReadOnly = true;
            // 
            // HostName
            // 
            this.HostName.HeaderText = "Host Name";
            this.HostName.Name = "HostName";
            this.HostName.ReadOnly = true;
            // 
            // Ports
            // 
            this.Ports.HeaderText = "Ports";
            this.Ports.Name = "Ports";
            this.Ports.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.diagramDisplayControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(595, 442);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphic View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // diagramDisplayControl1
            // 
            this.diagramDisplayControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramDisplayControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diagramDisplayControl1.Location = new System.Drawing.Point(0, 0);
            this.diagramDisplayControl1.Name = "diagramDisplayControl1";
            this.diagramDisplayControl1.Size = new System.Drawing.Size(595, 442);
            this.diagramDisplayControl1.TabIndex = 0;
            this.diagramDisplayControl1.Load += new System.EventHandler(this.diagramDisplayControl1_Load);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(484, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ScanButton
            // 
            this.ScanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ScanButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanButton.ForeColor = System.Drawing.SystemColors.Window;
            this.ScanButton.Image = ((System.Drawing.Image)(resources.GetObject("ScanButton.Image")));
            this.ScanButton.Location = new System.Drawing.Point(452, 18);
            this.ScanButton.Margin = new System.Windows.Forms.Padding(0);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(32, 32);
            this.ScanButton.TabIndex = 9;
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // GridWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(603, 533);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.ProgressScan);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GridWindow";
            this.Text = "Grid Mapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GridWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton LoadScan;
        private System.Windows.Forms.ProgressBar ProgressScan;
        private System.Windows.Forms.ToolStripButton SaveScan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ScanButton;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fastScanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem portScanToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ToolStripMenuItem normalScanToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Id;
		private System.Windows.Forms.DataGridViewTextBoxColumn IPAddress;
		private System.Windows.Forms.DataGridViewTextBoxColumn MacAddress;
		private System.Windows.Forms.DataGridViewTextBoxColumn HostName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Ports;
		private System.Windows.Forms.TabPage tabPage2;
		private DiagramDisplayControl diagramDisplayControl1;
        private System.Windows.Forms.Button button1;
	}
}

