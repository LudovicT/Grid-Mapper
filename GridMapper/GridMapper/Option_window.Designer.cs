namespace GridMapper
{
    partial class Option_window
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Option_window ) );
			this.option_cancel = new System.Windows.Forms.Button();
			this.option_accept = new System.Windows.Forms.Button();
			this.option_ping = new System.Windows.Forms.CheckBox();
			this.option_ARPing = new System.Windows.Forms.CheckBox();
			this.option_DNS = new System.Windows.Forms.CheckBox();
			this.option_port = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.option_ARP = new System.Windows.Forms.CheckBox();
			this.pingGroupBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
			this.RandomGroupBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.PortGroupBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
			this.option_RandomTCPPort = new System.Windows.Forms.CheckBox();
			this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.option_TCPPort = new System.Windows.Forms.NumericUpDown();
			this.option_RandomUDPPort = new System.Windows.Forms.CheckBox();
			this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.option_UDPPort = new System.Windows.Forms.NumericUpDown();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.option_nbPacketToSend = new System.Windows.Forms.NumericUpDown();
			this.flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.option_waitTime = new System.Windows.Forms.NumericUpDown();
			this.pingGroupBox.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel5.SuspendLayout();
			this.RandomGroupBox.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.PortGroupBox.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.flowLayoutPanel6.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.flowLayoutPanel7.SuspendLayout();
			this.flowLayoutPanel8.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_TCPPort ) ).BeginInit();
			this.flowLayoutPanel9.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_UDPPort ) ).BeginInit();
			this.groupBox4.SuspendLayout();
			this.flowLayoutPanel10.SuspendLayout();
			this.flowLayoutPanel12.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_nbPacketToSend ) ).BeginInit();
			this.flowLayoutPanel11.SuspendLayout();
			this.flowLayoutPanel13.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_waitTime ) ).BeginInit();
			this.SuspendLayout();
			// 
			// option_cancel
			// 
			this.option_cancel.Location = new System.Drawing.Point( 293, 324 );
			this.option_cancel.Margin = new System.Windows.Forms.Padding( 2 );
			this.option_cancel.Name = "option_cancel";
			this.option_cancel.Size = new System.Drawing.Size( 56, 19 );
			this.option_cancel.TabIndex = 0;
			this.option_cancel.Text = "Cancel";
			this.option_cancel.UseVisualStyleBackColor = true;
			this.option_cancel.Click += new System.EventHandler( this.option_cancel_Click );
			// 
			// option_accept
			// 
			this.option_accept.Location = new System.Drawing.Point( 221, 324 );
			this.option_accept.Margin = new System.Windows.Forms.Padding( 2 );
			this.option_accept.Name = "option_accept";
			this.option_accept.Size = new System.Drawing.Size( 56, 19 );
			this.option_accept.TabIndex = 1;
			this.option_accept.Text = "Save";
			this.option_accept.UseVisualStyleBackColor = true;
			this.option_accept.Click += new System.EventHandler( this.option_accept_Click );
			// 
			// option_ping
			// 
			this.option_ping.AutoSize = true;
			this.option_ping.ForeColor = System.Drawing.SystemColors.ControlText;
			this.option_ping.Location = new System.Drawing.Point( 3, 26 );
			this.option_ping.Name = "option_ping";
			this.option_ping.Size = new System.Drawing.Size( 76, 17 );
			this.option_ping.TabIndex = 2;
			this.option_ping.Text = "ICMP Ping";
			this.option_ping.UseVisualStyleBackColor = true;
			// 
			// option_ARPing
			// 
			this.option_ARPing.AutoSize = true;
			this.option_ARPing.Location = new System.Drawing.Point( 3, 3 );
			this.option_ARPing.Name = "option_ARPing";
			this.option_ARPing.Size = new System.Drawing.Size( 71, 17 );
			this.option_ARPing.TabIndex = 3;
			this.option_ARPing.Text = "ARP ping";
			this.option_ARPing.UseVisualStyleBackColor = true;
			// 
			// option_DNS
			// 
			this.option_DNS.AutoSize = true;
			this.option_DNS.Location = new System.Drawing.Point( 3, 3 );
			this.option_DNS.Name = "option_DNS";
			this.option_DNS.Size = new System.Drawing.Size( 113, 17 );
			this.option_DNS.TabIndex = 4;
			this.option_DNS.Text = "Resolve Hosname";
			this.option_DNS.UseVisualStyleBackColor = true;
			// 
			// option_port
			// 
			this.option_port.AutoSize = true;
			this.option_port.Location = new System.Drawing.Point( 3, 6 );
			this.option_port.Margin = new System.Windows.Forms.Padding( 3, 6, 3, 3 );
			this.option_port.Name = "option_port";
			this.option_port.Size = new System.Drawing.Size( 50, 17 );
			this.option_port.TabIndex = 5;
			this.option_port.Text = "Ports";
			this.option_port.UseVisualStyleBackColor = true;
			this.option_port.CheckedChanged += new System.EventHandler( this.option_port_CheckedChanged );
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point( 76, 3 );
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size( 92, 20 );
			this.textBox1.TabIndex = 6;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point( 87, 3 );
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size( 58, 20 );
			this.textBox2.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 3, 7 );
			this.label1.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 67, 13 );
			this.label1.TabIndex = 8;
			this.label1.Text = "Timeout (ms)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 3, 7 );
			this.label2.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 78, 13 );
			this.label2.TabIndex = 9;
			this.label2.Text = "Maximum Task";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point( 59, 3 );
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size( 115, 20 );
			this.textBox3.TabIndex = 10;
			// 
			// option_ARP
			// 
			this.option_ARP.AutoSize = true;
			this.option_ARP.Location = new System.Drawing.Point( 3, 26 );
			this.option_ARP.Name = "option_ARP";
			this.option_ARP.Size = new System.Drawing.Size( 110, 17 );
			this.option_ARP.TabIndex = 11;
			this.option_ARP.Text = "Get MAC Address";
			this.option_ARP.UseVisualStyleBackColor = true;
			// 
			// pingGroupBox
			// 
			this.pingGroupBox.AutoSize = true;
			this.pingGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pingGroupBox.Controls.Add( this.flowLayoutPanel2 );
			this.pingGroupBox.Location = new System.Drawing.Point( 12, 12 );
			this.pingGroupBox.Name = "pingGroupBox";
			this.pingGroupBox.Size = new System.Drawing.Size( 183, 97 );
			this.pingGroupBox.TabIndex = 12;
			this.pingGroupBox.TabStop = false;
			this.pingGroupBox.Text = "Ping";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel2.Controls.Add( this.option_ARPing );
			this.flowLayoutPanel2.Controls.Add( this.option_ping );
			this.flowLayoutPanel2.Controls.Add( this.flowLayoutPanel5 );
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size( 177, 78 );
			this.flowLayoutPanel2.TabIndex = 0;
			// 
			// flowLayoutPanel5
			// 
			this.flowLayoutPanel5.AutoSize = true;
			this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel5.Controls.Add( this.label1 );
			this.flowLayoutPanel5.Controls.Add( this.textBox1 );
			this.flowLayoutPanel5.Location = new System.Drawing.Point( 3, 49 );
			this.flowLayoutPanel5.Name = "flowLayoutPanel5";
			this.flowLayoutPanel5.Size = new System.Drawing.Size( 171, 26 );
			this.flowLayoutPanel5.TabIndex = 16;
			// 
			// RandomGroupBox
			// 
			this.RandomGroupBox.AutoSize = true;
			this.RandomGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.RandomGroupBox.Controls.Add( this.flowLayoutPanel3 );
			this.RandomGroupBox.Location = new System.Drawing.Point( 201, 12 );
			this.RandomGroupBox.Name = "RandomGroupBox";
			this.RandomGroupBox.Size = new System.Drawing.Size( 125, 65 );
			this.RandomGroupBox.TabIndex = 13;
			this.RandomGroupBox.TabStop = false;
			this.RandomGroupBox.Text = "Other";
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel3.Controls.Add( this.option_DNS );
			this.flowLayoutPanel3.Controls.Add( this.option_ARP );
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size( 119, 46 );
			this.flowLayoutPanel3.TabIndex = 0;
			// 
			// PortGroupBox
			// 
			this.PortGroupBox.AutoSize = true;
			this.PortGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PortGroupBox.Controls.Add( this.flowLayoutPanel1 );
			this.PortGroupBox.Location = new System.Drawing.Point( 12, 115 );
			this.PortGroupBox.Name = "PortGroupBox";
			this.PortGroupBox.Size = new System.Drawing.Size( 183, 45 );
			this.PortGroupBox.TabIndex = 14;
			this.PortGroupBox.TabStop = false;
			this.PortGroupBox.Text = "Port to test";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add( this.option_port );
			this.flowLayoutPanel1.Controls.Add( this.textBox3 );
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size( 177, 26 );
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add( this.flowLayoutPanel4 );
			this.groupBox1.Location = new System.Drawing.Point( 201, 115 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size( 154, 45 );
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tasks";
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.AutoSize = true;
			this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel4.Controls.Add( this.label2 );
			this.flowLayoutPanel4.Controls.Add( this.textBox2 );
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel4.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size( 148, 26 );
			this.flowLayoutPanel4.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSize = true;
			this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox2.Controls.Add( this.flowLayoutPanel6 );
			this.groupBox2.Location = new System.Drawing.Point( 12, 166 );
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size( 343, 154 );
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Network Specifics";
			// 
			// flowLayoutPanel6
			// 
			this.flowLayoutPanel6.AutoSize = true;
			this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel6.Controls.Add( this.groupBox3 );
			this.flowLayoutPanel6.Controls.Add( this.groupBox4 );
			this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel6.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel6.Name = "flowLayoutPanel6";
			this.flowLayoutPanel6.Size = new System.Drawing.Size( 337, 135 );
			this.flowLayoutPanel6.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.AutoSize = true;
			this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox3.Controls.Add( this.flowLayoutPanel7 );
			this.groupBox3.Location = new System.Drawing.Point( 3, 3 );
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size( 148, 129 );
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "TCP/UDP specifics";
			// 
			// flowLayoutPanel7
			// 
			this.flowLayoutPanel7.AutoSize = true;
			this.flowLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel7.Controls.Add( this.option_RandomTCPPort );
			this.flowLayoutPanel7.Controls.Add( this.flowLayoutPanel8 );
			this.flowLayoutPanel7.Controls.Add( this.option_RandomUDPPort );
			this.flowLayoutPanel7.Controls.Add( this.flowLayoutPanel9 );
			this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel7.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel7.Name = "flowLayoutPanel7";
			this.flowLayoutPanel7.Size = new System.Drawing.Size( 142, 110 );
			this.flowLayoutPanel7.TabIndex = 0;
			// 
			// option_RandomTCPPort
			// 
			this.option_RandomTCPPort.AutoSize = true;
			this.option_RandomTCPPort.Enabled = false;
			this.option_RandomTCPPort.Location = new System.Drawing.Point( 3, 3 );
			this.option_RandomTCPPort.Name = "option_RandomTCPPort";
			this.option_RandomTCPPort.Size = new System.Drawing.Size( 134, 17 );
			this.option_RandomTCPPort.TabIndex = 17;
			this.option_RandomTCPPort.Text = "Use Random TCP Port";
			this.option_RandomTCPPort.UseVisualStyleBackColor = true;
			this.option_RandomTCPPort.CheckedChanged += new System.EventHandler( this.option_RandomTCPPort_CheckedChanged );
			// 
			// flowLayoutPanel8
			// 
			this.flowLayoutPanel8.AutoSize = true;
			this.flowLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel8.Controls.Add( this.label3 );
			this.flowLayoutPanel8.Controls.Add( this.option_TCPPort );
			this.flowLayoutPanel8.Location = new System.Drawing.Point( 3, 26 );
			this.flowLayoutPanel8.Name = "flowLayoutPanel8";
			this.flowLayoutPanel8.Size = new System.Drawing.Size( 118, 26 );
			this.flowLayoutPanel8.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point( 3, 7 );
			this.label3.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size( 50, 13 );
			this.label3.TabIndex = 1;
			this.label3.Text = "TCP Port";
			// 
			// option_TCPPort
			// 
			this.option_TCPPort.Location = new System.Drawing.Point( 59, 3 );
			this.option_TCPPort.Maximum = new decimal( new int[] {
            65535,
            0,
            0,
            0} );
			this.option_TCPPort.Minimum = new decimal( new int[] {
            5000,
            0,
            0,
            0} );
			this.option_TCPPort.Name = "option_TCPPort";
			this.option_TCPPort.Size = new System.Drawing.Size( 56, 20 );
			this.option_TCPPort.TabIndex = 0;
			this.option_TCPPort.Value = new decimal( new int[] {
            62000,
            0,
            0,
            0} );
			// 
			// option_RandomUDPPort
			// 
			this.option_RandomUDPPort.AutoSize = true;
			this.option_RandomUDPPort.Enabled = false;
			this.option_RandomUDPPort.Location = new System.Drawing.Point( 3, 58 );
			this.option_RandomUDPPort.Name = "option_RandomUDPPort";
			this.option_RandomUDPPort.Size = new System.Drawing.Size( 136, 17 );
			this.option_RandomUDPPort.TabIndex = 18;
			this.option_RandomUDPPort.Text = "Use Random UDP Port";
			this.option_RandomUDPPort.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel9
			// 
			this.flowLayoutPanel9.AutoSize = true;
			this.flowLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel9.Controls.Add( this.label4 );
			this.flowLayoutPanel9.Controls.Add( this.option_UDPPort );
			this.flowLayoutPanel9.Location = new System.Drawing.Point( 3, 81 );
			this.flowLayoutPanel9.Name = "flowLayoutPanel9";
			this.flowLayoutPanel9.Size = new System.Drawing.Size( 120, 26 );
			this.flowLayoutPanel9.TabIndex = 18;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Location = new System.Drawing.Point( 3, 7 );
			this.label4.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size( 52, 13 );
			this.label4.TabIndex = 1;
			this.label4.Text = "UDP Port";
			// 
			// option_UDPPort
			// 
			this.option_UDPPort.Enabled = false;
			this.option_UDPPort.Location = new System.Drawing.Point( 61, 3 );
			this.option_UDPPort.Maximum = new decimal( new int[] {
            65535,
            0,
            0,
            0} );
			this.option_UDPPort.Minimum = new decimal( new int[] {
            5000,
            0,
            0,
            0} );
			this.option_UDPPort.Name = "option_UDPPort";
			this.option_UDPPort.Size = new System.Drawing.Size( 56, 20 );
			this.option_UDPPort.TabIndex = 0;
			this.option_UDPPort.Value = new decimal( new int[] {
            62001,
            0,
            0,
            0} );
			// 
			// groupBox4
			// 
			this.groupBox4.AutoSize = true;
			this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox4.Controls.Add( this.flowLayoutPanel10 );
			this.groupBox4.Location = new System.Drawing.Point( 157, 3 );
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size( 177, 103 );
			this.groupBox4.TabIndex = 17;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Connection specifics";
			// 
			// flowLayoutPanel10
			// 
			this.flowLayoutPanel10.AutoSize = true;
			this.flowLayoutPanel10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel10.Controls.Add( this.flowLayoutPanel12 );
			this.flowLayoutPanel10.Controls.Add( this.flowLayoutPanel11 );
			this.flowLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel10.Location = new System.Drawing.Point( 3, 16 );
			this.flowLayoutPanel10.Name = "flowLayoutPanel10";
			this.flowLayoutPanel10.Size = new System.Drawing.Size( 171, 84 );
			this.flowLayoutPanel10.TabIndex = 0;
			// 
			// flowLayoutPanel12
			// 
			this.flowLayoutPanel12.AutoSize = true;
			this.flowLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel12.Controls.Add( this.label6 );
			this.flowLayoutPanel12.Controls.Add( this.option_nbPacketToSend );
			this.flowLayoutPanel12.Location = new System.Drawing.Point( 3, 3 );
			this.flowLayoutPanel12.Name = "flowLayoutPanel12";
			this.flowLayoutPanel12.Size = new System.Drawing.Size( 165, 26 );
			this.flowLayoutPanel12.TabIndex = 21;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point( 3, 7 );
			this.label6.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size( 97, 13 );
			this.label6.TabIndex = 19;
			this.label6.Text = "Packets buffer size";
			// 
			// option_nbPacketToSend
			// 
			this.option_nbPacketToSend.Location = new System.Drawing.Point( 106, 3 );
			this.option_nbPacketToSend.Maximum = new decimal( new int[] {
            100000000,
            0,
            0,
            0} );
			this.option_nbPacketToSend.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            -2147483648} );
			this.option_nbPacketToSend.Name = "option_nbPacketToSend";
			this.option_nbPacketToSend.Size = new System.Drawing.Size( 56, 20 );
			this.option_nbPacketToSend.TabIndex = 18;
			// 
			// flowLayoutPanel11
			// 
			this.flowLayoutPanel11.AutoSize = true;
			this.flowLayoutPanel11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel11.Controls.Add( this.flowLayoutPanel13 );
			this.flowLayoutPanel11.Controls.Add( this.option_waitTime );
			this.flowLayoutPanel11.Location = new System.Drawing.Point( 3, 35 );
			this.flowLayoutPanel11.Name = "flowLayoutPanel11";
			this.flowLayoutPanel11.Size = new System.Drawing.Size( 152, 46 );
			this.flowLayoutPanel11.TabIndex = 20;
			// 
			// flowLayoutPanel13
			// 
			this.flowLayoutPanel13.AutoSize = true;
			this.flowLayoutPanel13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel13.Controls.Add( this.label5 );
			this.flowLayoutPanel13.Controls.Add( this.label7 );
			this.flowLayoutPanel13.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel13.Location = new System.Drawing.Point( 3, 3 );
			this.flowLayoutPanel13.Name = "flowLayoutPanel13";
			this.flowLayoutPanel13.Size = new System.Drawing.Size( 84, 40 );
			this.flowLayoutPanel13.TabIndex = 18;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point( 3, 7 );
			this.label5.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size( 78, 13 );
			this.label5.TabIndex = 19;
			this.label5.Text = "Delay between";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point( 3, 27 );
			this.label7.Margin = new System.Windows.Forms.Padding( 3, 7, 3, 0 );
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size( 61, 13 );
			this.label7.TabIndex = 0;
			this.label7.Text = "each buffer";
			// 
			// option_waitTime
			// 
			this.option_waitTime.Location = new System.Drawing.Point( 93, 13 );
			this.option_waitTime.Margin = new System.Windows.Forms.Padding( 3, 13, 3, 3 );
			this.option_waitTime.Maximum = new decimal( new int[] {
            1000000,
            0,
            0,
            0} );
			this.option_waitTime.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            -2147483648} );
			this.option_waitTime.Name = "option_waitTime";
			this.option_waitTime.Size = new System.Drawing.Size( 56, 20 );
			this.option_waitTime.TabIndex = 18;
			// 
			// Option_window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size( 359, 354 );
			this.ControlBox = false;
			this.Controls.Add( this.groupBox2 );
			this.Controls.Add( this.groupBox1 );
			this.Controls.Add( this.PortGroupBox );
			this.Controls.Add( this.RandomGroupBox );
			this.Controls.Add( this.pingGroupBox );
			this.Controls.Add( this.option_accept );
			this.Controls.Add( this.option_cancel );
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.DoubleBuffered = true;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
			this.Margin = new System.Windows.Forms.Padding( 2 );
			this.MinimumSize = new System.Drawing.Size( 375, 370 );
			this.Name = "Option_window";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.TopMost = true;
			this.pingGroupBox.ResumeLayout( false );
			this.pingGroupBox.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout( false );
			this.flowLayoutPanel2.PerformLayout();
			this.flowLayoutPanel5.ResumeLayout( false );
			this.flowLayoutPanel5.PerformLayout();
			this.RandomGroupBox.ResumeLayout( false );
			this.RandomGroupBox.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout( false );
			this.flowLayoutPanel3.PerformLayout();
			this.PortGroupBox.ResumeLayout( false );
			this.PortGroupBox.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout( false );
			this.flowLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout( false );
			this.groupBox1.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout( false );
			this.flowLayoutPanel4.PerformLayout();
			this.groupBox2.ResumeLayout( false );
			this.groupBox2.PerformLayout();
			this.flowLayoutPanel6.ResumeLayout( false );
			this.flowLayoutPanel6.PerformLayout();
			this.groupBox3.ResumeLayout( false );
			this.groupBox3.PerformLayout();
			this.flowLayoutPanel7.ResumeLayout( false );
			this.flowLayoutPanel7.PerformLayout();
			this.flowLayoutPanel8.ResumeLayout( false );
			this.flowLayoutPanel8.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_TCPPort ) ).EndInit();
			this.flowLayoutPanel9.ResumeLayout( false );
			this.flowLayoutPanel9.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_UDPPort ) ).EndInit();
			this.groupBox4.ResumeLayout( false );
			this.groupBox4.PerformLayout();
			this.flowLayoutPanel10.ResumeLayout( false );
			this.flowLayoutPanel10.PerformLayout();
			this.flowLayoutPanel12.ResumeLayout( false );
			this.flowLayoutPanel12.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_nbPacketToSend ) ).EndInit();
			this.flowLayoutPanel11.ResumeLayout( false );
			this.flowLayoutPanel11.PerformLayout();
			this.flowLayoutPanel13.ResumeLayout( false );
			this.flowLayoutPanel13.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.option_waitTime ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button option_cancel;
        private System.Windows.Forms.Button option_accept;
        private System.Windows.Forms.CheckBox option_ping;
        private System.Windows.Forms.CheckBox option_ARPing;
        private System.Windows.Forms.CheckBox option_DNS;
        private System.Windows.Forms.CheckBox option_port;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.CheckBox option_ARP;
		private System.Windows.Forms.GroupBox pingGroupBox;
		private System.Windows.Forms.GroupBox RandomGroupBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.GroupBox PortGroupBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
		private System.Windows.Forms.CheckBox option_RandomTCPPort;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown option_TCPPort;
		private System.Windows.Forms.CheckBox option_RandomUDPPort;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown option_UDPPort;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
		private System.Windows.Forms.NumericUpDown option_waitTime;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown option_nbPacketToSend;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel13;
		private System.Windows.Forms.Label label7;
    }
}