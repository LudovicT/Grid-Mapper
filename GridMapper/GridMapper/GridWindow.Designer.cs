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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aRPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dNSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.advancedOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fastScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FromIP = new IPAddressControlLib.IPAddressControl();
            this.ToIP = new IPAddressControlLib.IPAddressControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgressScan = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LoadScan = new System.Windows.Forms.ToolStripButton();
            this.SaveScan = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.startToolStripMenuItem1});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // startToolStripMenuItem
            // 
            resources.ApplyResources(this.startToolStripMenuItem, "startToolStripMenuItem");
            this.startToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            resources.ApplyResources(this.optionToolStripMenuItem, "optionToolStripMenuItem");
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem,
            this.aRPToolStripMenuItem,
            this.dNSToolStripMenuItem,
            this.portToolStripMenuItem,
            this.toolStripSeparator1,
            this.advancedOptionToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            // 
            // pingToolStripMenuItem
            // 
            resources.ApplyResources(this.pingToolStripMenuItem, "pingToolStripMenuItem");
            this.pingToolStripMenuItem.Checked = true;
            this.pingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            // 
            // aRPToolStripMenuItem
            // 
            resources.ApplyResources(this.aRPToolStripMenuItem, "aRPToolStripMenuItem");
            this.aRPToolStripMenuItem.Checked = true;
            this.aRPToolStripMenuItem.CheckOnClick = true;
            this.aRPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aRPToolStripMenuItem.Name = "aRPToolStripMenuItem";
            // 
            // dNSToolStripMenuItem
            // 
            resources.ApplyResources(this.dNSToolStripMenuItem, "dNSToolStripMenuItem");
            this.dNSToolStripMenuItem.Checked = true;
            this.dNSToolStripMenuItem.CheckOnClick = true;
            this.dNSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dNSToolStripMenuItem.Name = "dNSToolStripMenuItem";
            // 
            // portToolStripMenuItem
            // 
            resources.ApplyResources(this.portToolStripMenuItem, "portToolStripMenuItem");
            this.portToolStripMenuItem.Checked = true;
            this.portToolStripMenuItem.CheckOnClick = true;
            this.portToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // advancedOptionToolStripMenuItem
            // 
            resources.ApplyResources(this.advancedOptionToolStripMenuItem, "advancedOptionToolStripMenuItem");
            this.advancedOptionToolStripMenuItem.Name = "advancedOptionToolStripMenuItem";
            this.advancedOptionToolStripMenuItem.Click += new System.EventHandler(this.advancedOptionToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem1
            // 
            resources.ApplyResources(this.startToolStripMenuItem1, "startToolStripMenuItem1");
            this.startToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastScanToolStripMenuItem,
            this.portScanToolStripMenuItem,
            this.iPScanToolStripMenuItem});
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            // 
            // fastScanToolStripMenuItem
            // 
            resources.ApplyResources(this.fastScanToolStripMenuItem, "fastScanToolStripMenuItem");
            this.fastScanToolStripMenuItem.Name = "fastScanToolStripMenuItem";
            this.fastScanToolStripMenuItem.Click += new System.EventHandler(this.fastScanToolStripMenuItem_Click_1);
            // 
            // portScanToolStripMenuItem
            // 
            resources.ApplyResources(this.portScanToolStripMenuItem, "portScanToolStripMenuItem");
            this.portScanToolStripMenuItem.Name = "portScanToolStripMenuItem";
            // 
            // iPScanToolStripMenuItem
            // 
            resources.ApplyResources(this.iPScanToolStripMenuItem, "iPScanToolStripMenuItem");
            this.iPScanToolStripMenuItem.Name = "iPScanToolStripMenuItem";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadScan,
            this.SaveScan});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Name = "dataGridView1";
            // 
            // FromIP
            // 
            resources.ApplyResources(this.FromIP, "FromIP");
            this.FromIP.AllowInternalTab = false;
            this.FromIP.AutoHeight = true;
            this.FromIP.BackColor = System.Drawing.SystemColors.Window;
            this.FromIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FromIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FromIP.Name = "FromIP";
            this.FromIP.ReadOnly = false;
            this.FromIP.Click += new System.EventHandler(this.ipAddressControl1_Click);
            // 
            // ToIP
            // 
            resources.ApplyResources(this.ToIP, "ToIP");
            this.ToIP.AllowInternalTab = false;
            this.ToIP.AutoHeight = true;
            this.ToIP.BackColor = System.Drawing.SystemColors.Window;
            this.ToIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ToIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ToIP.Name = "ToIP";
            this.ToIP.ReadOnly = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ProgressScan
            // 
            resources.ApplyResources(this.ProgressScan, "ProgressScan");
            this.ProgressScan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProgressScan.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProgressScan.Name = "ProgressScan";
            this.ProgressScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // LoadScan
            // 
            resources.ApplyResources(this.LoadScan, "LoadScan");
            this.LoadScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadScan.Name = "LoadScan";
            this.LoadScan.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // SaveScan
            // 
            resources.ApplyResources(this.SaveScan, "SaveScan");
            this.SaveScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveScan.Name = "SaveScan";
            this.SaveScan.Click += new System.EventHandler(this.SaveScan_Click);
            // 
            // GridWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.ProgressScan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToIP);
            this.Controls.Add(this.FromIP);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GridWindow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton LoadScan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private IPAddressControlLib.IPAddressControl FromIP;
        private IPAddressControlLib.IPAddressControl ToIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar ProgressScan;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SaveScan;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fastScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portScanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iPScanToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem advancedOptionToolStripMenuItem;
	}
}

