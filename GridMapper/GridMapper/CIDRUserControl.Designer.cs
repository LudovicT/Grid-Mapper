namespace GridMapper
{
    partial class CIDRUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
			this.ipAddressInput = new IPAddressControlLib.IPAddressControl();
			this.slash = new System.Windows.Forms.Label();
			this.CIDRInput = new System.Windows.Forms.NumericUpDown();
			( (System.ComponentModel.ISupportInitialize)( this.CIDRInput ) ).BeginInit();
			this.SuspendLayout();
			// 
			// ipAddressInput
			// 
			this.ipAddressInput.AllowInternalTab = false;
			this.ipAddressInput.AutoHeight = true;
			this.ipAddressInput.BackColor = System.Drawing.SystemColors.Window;
			this.ipAddressInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ipAddressInput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ipAddressInput.Location = new System.Drawing.Point( 35, 3 );
			this.ipAddressInput.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ipAddressInput.Name = "ipAddressInput";
			this.ipAddressInput.ReadOnly = false;
			this.ipAddressInput.Size = new System.Drawing.Size( 87, 20 );
			this.ipAddressInput.TabIndex = 0;
			this.ipAddressInput.Text = "...";
			// 
			// slash
			// 
			this.slash.AutoSize = true;
			this.slash.Location = new System.Drawing.Point( 124, 6 );
			this.slash.Name = "slash";
			this.slash.Size = new System.Drawing.Size( 12, 13 );
			this.slash.TabIndex = 2;
			this.slash.Text = "/";
			// 
			// CIDRInput
			// 
			this.CIDRInput.Location = new System.Drawing.Point( 142, 3 );
			this.CIDRInput.Maximum = new decimal( new int[] {
            32,
            0,
            0,
            0} );
			this.CIDRInput.Name = "CIDRInput";
			this.CIDRInput.Size = new System.Drawing.Size( 36, 20 );
			this.CIDRInput.TabIndex = 3;
			this.CIDRInput.Value = new decimal( new int[] {
            24,
            0,
            0,
            0} );
			this.CIDRInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.numericUpDown_KeyPress );
			// 
			// CIDRUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add( this.CIDRInput );
			this.Controls.Add( this.slash );
			this.Controls.Add( this.ipAddressInput );
			this.Margin = new System.Windows.Forms.Padding( 2 );
			this.Name = "CIDRUserControl";
			this.Size = new System.Drawing.Size( 270, 25 );
			( (System.ComponentModel.ISupportInitialize)( this.CIDRInput ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label slash;
		public IPAddressControlLib.IPAddressControl ipAddressInput;
		public System.Windows.Forms.NumericUpDown CIDRInput;

    }
}
