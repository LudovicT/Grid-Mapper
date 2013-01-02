using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace GridMapper.NetworkRawSender
{
	public class SendArping
	{
		public void Arping( IPAddress ipToSearchFor )
		{
			string ownMAC;
			string ownIP;
			getLocalMacAndIP( out ownIP, out ownMAC );
			char[] aDelimiter = { '.' };
			string[] aIPReso = ipToSearchFor.ToString().Split( aDelimiter, 4 );
			string[] aIPAddr = ownIP.Split( aDelimiter, 4 );

			byte[] oPacket = new byte[] { /*0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
						Convert.ToByte("0x" + ownMAC.Substring(0,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(2,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(4,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(6,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(8,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(10,2), 16),
						0x08, 0x06,*/ 0x00, 0x01,
						0x08, 0x00, 0x06, 0x04, 0x00, 0x01,
						Convert.ToByte("0x" + ownMAC.Substring(0,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(2,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(4,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(6,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(8,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(10,2), 16),
						Convert.ToByte(aIPAddr[0], 10),
						Convert.ToByte(aIPAddr[1], 10),
						Convert.ToByte(aIPAddr[2], 10),
						Convert.ToByte(aIPAddr[3], 10),
						0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
						Convert.ToByte(aIPReso[0], 10),
						Convert.ToByte(aIPReso[1], 10),
						Convert.ToByte(aIPReso[2], 10),
						Convert.ToByte(aIPReso[3], 10)};
			Socket arpSocket;
			arpSocket = new Socket( System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw );
			//arpSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true );
			arpSocket.EnableBroadcast = true;
			EndPoint localEndPoint = new IPEndPoint( IPAddress.Any, 0 );
			EndPoint remoteEndPoint = new IPEndPoint( ipToSearchFor, 0 );
			arpSocket.Bind( localEndPoint );
			arpSocket.BeginSendTo( oPacket,0,oPacket.Length,SocketFlags.None, remoteEndPoint, null, this);
			byte[] buffer = new byte[100];
			arpSocket.BeginReceiveMessageFrom( buffer, 0, 100, SocketFlags.None, ref remoteEndPoint, null, this);
		}

		public static void getLocalMacAndIP( out string ip, out string mac )
		{
			ip = string.Empty;
			mac = string.Empty;
			//get the interface
			NetworkInterface TrueNic = null;
			foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
			{
				if ( nic.OperationalStatus == OperationalStatus.Up )
				{
					TrueNic = nic;
					break;
				}
			}

			//get the interface ipv4
			foreach ( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					ip = ips.ToString();
					break;
				}
			}
			//get the interface mac
			mac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() ).ToString();
		}

	}
}
