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
		public delegate void OptionUpdatedHandler( object sender, OptionUpdatedEventArgs e );
		public event OptionUpdatedHandler OptionUpdated;

        public Option_window(Option opt)
        {
            InitializeComponent();
			option_ARP.Checked = opt.Arp;
			option_DNS.Checked = opt.Dns;
			option_ping.Checked = opt.Ping;
			textBox1.Text = opt.PingTimeout.ToString();
			textBox2.Text = opt.MaximumTasks.ToString();

        }

        private void option_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void option_accept_Click(object sender, EventArgs e)
		{
			int timeout = 0;
			int tasks = 0;
			if ( int.TryParse( textBox1.Text, out timeout ) )
			{
				if ( timeout > 0 )
				{
					if ( int.TryParse( textBox2.Text, out tasks ) )
					{
						if ( tasks >= 5 && tasks <= 2000 )
						{
							//if ( option_port.Checked == true )
							//{
							//    PortsParserResult ppr = PortsParser.MainPortsParser( textBox3.Text );
							//    if ( ppr.Result != null )
							//    {
							//        OptionUpdatedEventArgs args = new OptionUpdatedEventArgs( option_ARP.Checked, option_DNS.Checked, option_port.Checked, timeout, tasks, ppr.Result );
							//        this.Close();
							//    }
							//    else
							//    {
							//        MessageBox.Show( "Incorect input for the ports to search", "Error in port format" );
							//    }
							//}
							//else
							//{
								OptionUpdatedEventArgs args = new OptionUpdatedEventArgs( option_ARP.Checked, option_DNS.Checked, false, timeout, tasks, null );
								OptionUpdated( this, args );
								this.Dispose();
							//}
						}
						else
						{

							MessageBox.Show( "The number of running tasks must be between 5 and 2000, 200 is recommended", "Error in tasks format" );
						}
					}
					else
					{
						MessageBox.Show( "Tasks must be a number", "Error in tasks format" );
					}
				}
				else
				{
					MessageBox.Show( "Timeout must be positive", "Error in timeout format" );
				}
			}
			else
			{
				MessageBox.Show( "Timeout must be a number", "Error in timeout format" );
			}
        }

    }
		public class OptionUpdatedEventArgs : EventArgs
		{
			public OptionUpdatedEventArgs( bool arp, bool dns, bool port, int timeout, int tasks, List<ushort> ports)
			{
				Arp = arp;
				Dns = dns;
				Port = port;
				Timeout = timeout;
				Tasks = tasks;
				Ports = ports;
			}

			public bool Arp { get; private set; }
			public bool Dns { get; private set; }
			public bool Port { get; private set; }
			public int Timeout { get; private set; }
			public int Tasks { get; private set; }
			public List<ushort> Ports { get; private set; }
		}
}
