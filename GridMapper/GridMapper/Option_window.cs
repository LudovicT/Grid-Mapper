using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridMapper
{
    public partial class Option_window : Form
    {
        public Option_window()
        {
            InitializeComponent();
        }

        private void option_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void option_accept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
