namespace GridMapper
{
	partial class IPRangeUserControl
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
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ToIP = new IPAddressControlLib.IPAddressControl();
			this.FromIP = new IPAddressControlLib.IPAddressControl();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point( 159, 6 );
			this.label2.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 16, 13 );
			this.label2.TabIndex = 11;
			this.label2.Text = "to";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point( 5, 6 );
			this.label1.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 62, 13 );
			this.label1.TabIndex = 10;
			this.label1.Text = "Range from";
			// 
			// ToIP
			// 
			this.ToIP.AllowInternalTab = false;
			this.ToIP.AutoHeight = true;
			this.ToIP.BackColor = System.Drawing.SystemColors.Window;
			this.ToIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ToIP.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ToIP.Location = new System.Drawing.Point( 179, 3 );
			this.ToIP.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.ToIP.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ToIP.Name = "ToIP";
			this.ToIP.ReadOnly = false;
			this.ToIP.Size = new System.Drawing.Size( 87, 20 );
			this.ToIP.TabIndex = 9;
			this.ToIP.Text = "0.0.0.0";
			// 
			// FromIP
			// 
			this.FromIP.AllowInternalTab = false;
			this.FromIP.AutoHeight = true;
			this.FromIP.BackColor = System.Drawing.SystemColors.Window;
			this.FromIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.FromIP.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.FromIP.Location = new System.Drawing.Point( 68, 3 );
			this.FromIP.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.FromIP.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.FromIP.Name = "FromIP";
			this.FromIP.ReadOnly = false;
			this.FromIP.Size = new System.Drawing.Size( 87, 20 );
			this.FromIP.TabIndex = 8;
			this.FromIP.Text = "0.0.0.0";
			// 
			// IPRangeUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.ToIP );
			this.Controls.Add( this.FromIP );
			this.Name = "IPRangeUserControl";
			this.Size = new System.Drawing.Size( 270, 25 );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private IPAddressControlLib.IPAddressControl ToIP;
		private IPAddressControlLib.IPAddressControl FromIP;
	}
}
