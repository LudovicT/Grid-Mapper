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
			this.ToIPInput = new IPAddressControlLib.IPAddressControl();
			this.FromIPInput = new IPAddressControlLib.IPAddressControl();
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
			// ToIPInput
			// 
			this.ToIPInput.AllowInternalTab = false;
			this.ToIPInput.AutoHeight = true;
			this.ToIPInput.BackColor = System.Drawing.SystemColors.Window;
			this.ToIPInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ToIPInput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ToIPInput.Location = new System.Drawing.Point( 179, 3 );
			this.ToIPInput.Margin = new System.Windows.Forms.Padding( 2 );
			this.ToIPInput.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ToIPInput.Name = "ToIPInput";
			this.ToIPInput.ReadOnly = false;
			this.ToIPInput.Size = new System.Drawing.Size( 87, 20 );
			this.ToIPInput.TabIndex = 9;
			this.ToIPInput.Text = "0.0.0.0";
			// 
			// FromIPInput
			// 
			this.FromIPInput.AllowInternalTab = false;
			this.FromIPInput.AutoHeight = true;
			this.FromIPInput.BackColor = System.Drawing.SystemColors.Window;
			this.FromIPInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.FromIPInput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.FromIPInput.Location = new System.Drawing.Point( 68, 3 );
			this.FromIPInput.Margin = new System.Windows.Forms.Padding( 2 );
			this.FromIPInput.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.FromIPInput.Name = "FromIPInput";
			this.FromIPInput.ReadOnly = false;
			this.FromIPInput.Size = new System.Drawing.Size( 87, 20 );
			this.FromIPInput.TabIndex = 8;
			this.FromIPInput.Text = "0.0.0.0";
			// 
			// IPRangeUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.ToIPInput );
			this.Controls.Add( this.FromIPInput );
			this.Name = "IPRangeUserControl";
			this.Size = new System.Drawing.Size( 270, 25 );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		public IPAddressControlLib.IPAddressControl ToIPInput;
		public IPAddressControlLib.IPAddressControl FromIPInput;
	}
}
