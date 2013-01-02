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
			option_ARPing.Checked = opt.Arping;
			option_ARP.Checked = opt.Arp;
			option_DNS.Checked = opt.Dns;
			option_ping.Checked = opt.Ping;
			option_port.Checked = opt.Port;
			textBox1.Text = opt.PingTimeout.ToString();
			textBox2.Text = opt.MaximumTasks.ToString();

			string ports = string.Empty;
			foreach ( ushort port in opt.PortToTest.Result )
			{
				if ( ports != String.Empty )
				{
					ports += ",";
				}
				ports += port.ToString();
			}
			textBox3.Text = ports ;

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
							if ( option_port.Checked == true )
							{
							    PortsParserResult ppr = PortsParser.Tryparse( textBox3.Text );
							    if ( ppr.Result != null )
							    {
									OptionUpdatedEventArgs args = new OptionUpdatedEventArgs( option_ARPing.Checked, option_ARP.Checked, option_DNS.Checked, option_port.Checked, timeout, tasks, ppr );
									OptionUpdated( this, args );
							        this.Close();
							    }
							    else
							    {
							        MessageBox.Show( "Incorect input for the ports to search", "Error in port format" );
							    }
							}
							else
							{
								OptionUpdatedEventArgs args = new OptionUpdatedEventArgs( option_ARPing.Checked, option_ARP.Checked, option_DNS.Checked, false, timeout, tasks, null );
								OptionUpdated( this, args );
								this.Dispose();
							}
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

		private void option_port_CheckedChanged( object sender, EventArgs e )
		{
			if ( option_port.Checked == false )
				textBox3.Hide();
			else if ( option_port.Checked == true )
				textBox3.Show();
		}

    }
		public class OptionUpdatedEventArgs : EventArgs
		{
			public OptionUpdatedEventArgs( bool arping, bool arp, bool dns, bool port, int timeout, int tasks, PortsParserResult ports )
			{
				Arping = arping; ;
				Arp = arp;
				Dns = dns;
				Port = port;
				Timeout = timeout;
				Tasks = tasks;
				Ports = ports;
			}

			public bool Arping { get; private set; }
			public bool Arp { get; private set; }
			public bool Dns { get; private set; }
			public bool Port { get; private set; }
			public int Timeout { get; private set; }
			public int Tasks { get; private set; }
			public PortsParserResult Ports { get; private set; }
		}
}
