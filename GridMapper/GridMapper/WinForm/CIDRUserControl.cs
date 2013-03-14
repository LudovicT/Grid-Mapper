using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridMapper
{
    public partial class CIDRUserControl : UserControl
    {
        public CIDRUserControl()
        {
            InitializeComponent();
        }

		private void numericUpDown_KeyPress( object sender, KeyPressEventArgs e )
		{
			if ( !char.IsControl( e.KeyChar )
				&& !char.IsDigit( e.KeyChar )
				&& e.KeyChar != '.' )
			{
				e.Handled = true;
			}
		}
    }
}
