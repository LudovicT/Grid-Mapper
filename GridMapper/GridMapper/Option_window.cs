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

		public Option_window( Option opt )
		{
			InitializeComponent();
			option_ARPing.Checked = opt.Arping;
			option_ARP.Checked = opt.Arp;
			option_DNS.Checked = opt.Dns;
			option_ping.Checked = opt.Ping;
			option_port.Checked = opt.Port;
			textBox1.Text = opt.PingTimeout.ToString();
			textBox2.Text = opt.MaximumTasks.ToString();
			option_nbPacketToSend.Value = opt.NbPacketToSend;
			option_waitTime.Value = opt.WaitTime;
			option_TCPPort.Value = opt.TCPPort;
			option_UDPPort.Value = opt.UDPPort;
			option_RandomTCPPort.Checked = opt.RandomTCPPort;
			option_RandomUDPPort.Checked = opt.RandomUDPPort;
			textBox3.Text = opt.PortToTestString;

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
							PortsParserResult ppr = PortsParser.Tryparse( textBox3.Text );
							if ( ppr.Result != null )
							{
								OptionUpdatedEventArgs args = new OptionUpdatedEventArgs(
									new Option()
									{
										Ping = option_ping.Checked,
										PingTimeout = timeout,
										Arping = option_ARPing.Checked,
										Arp = option_ARP.Checked,
										Dns = option_DNS.Checked,
										Port = option_port.Checked,
										PortToTestString = textBox3.Text,
										MaximumTasks = tasks,
										NbPacketToSend = (int)option_nbPacketToSend.Value,
										WaitTime = (int)option_waitTime.Value,
										TCPPort = (ushort)option_TCPPort.Value,
										UDPPort = (ushort)option_UDPPort.Value,
										RandomTCPPort = option_RandomTCPPort.Checked,
										RandomUDPPort = option_RandomUDPPort.Checked,
									} );
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
				textBox3.Enabled = false;
			else if ( option_port.Checked == true )
				textBox3.Enabled = true;
		}

		private void option_RandomTCPPort_CheckedChanged( object sender, EventArgs e )
		{
			if ( option_RandomTCPPort.Checked == false )
			{
				label3.Enabled = true;
				option_TCPPort.Enabled = true;
			}
			else if ( option_RandomTCPPort.Checked == true )
			{
				label3.Enabled = false;
				option_TCPPort.Enabled = false;
			}
		}


    }
		public class OptionUpdatedEventArgs : EventArgs
		{
			public OptionUpdatedEventArgs( Option option )
			{
				Option = option;
			}

			public Option Option { get; private set; }
		}
}
