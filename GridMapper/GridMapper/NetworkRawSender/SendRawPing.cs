using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ConsoleApplication5;
using System.Collections;

namespace GridMapper.NetworkRawSender
{
	class SendRawPing
	{
		int dataSize;
		int ttlValue;
		System.Diagnostics.Process proc;
		Socket pingSocket;
		byte[] receiveBuffer;
		byte[] pingPayload;
		IcmpHeader icmpHeader; //a la mano mon reuff !
		byte[] pingPacket;
		EndPoint castResponseEndPoint;
		ArrayList protocolHeaderList;

		public static event EventHandler<SendPingEventArgs> SendPing;

		public SendRawPing()
		{

			dataSize = 256;
			ttlValue = 64;
			protocolHeaderList = new ArrayList();

			pingSocket = new Socket( AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp );

			pingSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttlValue );

			IPEndPoint localEndPoint = new IPEndPoint( IPAddress.Any, 0 );
			pingSocket.Bind( localEndPoint );

			receiveBuffer = new byte[200 + 28 + 8 + dataSize];
			pingPayload = new byte[dataSize];
			for( int i = 0 ; i < pingPayload.Length ; i++ )
			{
				pingPayload[i] = (byte)'e';
			}

			icmpHeader = new IcmpHeader();

			icmpHeader.Id = 0;
			icmpHeader.Sequence = 1;
			icmpHeader.Type = IcmpHeader.EchoRequestType;
			icmpHeader.Code = IcmpHeader.EchoRequestCode;

			protocolHeaderList.Add( icmpHeader );

			pingPacket = icmpHeader.BuildPacket( protocolHeaderList, pingPayload );

			
		}

		public void Send(IPAddress ipAddress)
		{
			castResponseEndPoint = new IPEndPoint( ipAddress, 0 );

			pingSocket.BeginReceiveFrom( receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ref castResponseEndPoint, rveCallback, this );
			pingSocket.BeginSendTo( pingPacket, 0, pingPacket.Length, SocketFlags.None, castResponseEndPoint, sendCallback, this );
			//Thread.Sleep( 5 );
		}

		private void sendCallback( IAsyncResult ar )
		{
			pingSocket.EndSendTo( ar );
		}


		static readonly IPAddress ownIP = Dns.GetHostEntry( Dns.GetHostName() ).AddressList[0];
		static volatile int i = 0; 

		public static void rveCallback( IAsyncResult ar )
		{
			try
			{
				SendRawPing rawSock = (SendRawPing)ar.AsyncState;

				int bytesReceived = rawSock.pingSocket.EndReceiveFrom( ar, ref rawSock.castResponseEndPoint );

				//Ipv4Header v4Header;
				//IcmpHeader icmpv4Header;
				//byte[] pktIcmp;
				//int offset = 0;

				//// Remember, raw IPv4 sockets will return the IPv4 header along with all
				////    subsequent protocol headers
				//v4Header = Ipv4Header.Create( rawSock.receiveBuffer, ref offset );
				//pktIcmp = new byte[200 - offset];
				//Array.Copy( rawSock.receiveBuffer, offset, pktIcmp, 0, pktIcmp.Length );
				//icmpv4Header = IcmpHeader.Create( pktIcmp, ref offset );

				if (i == 0 && ownIP == ( (IPEndPoint)rawSock.castResponseEndPoint ).Address )
				{
					i++;
					SendPing( null, new SendPingEventArgs( ( (IPEndPoint)rawSock.castResponseEndPoint ).Address ) );
					return;
				}
				if ( ownIP != ( (IPEndPoint)rawSock.castResponseEndPoint ).Address )
				{
					SendPing( null, new SendPingEventArgs( ( (IPEndPoint)rawSock.castResponseEndPoint ).Address ) );
				}
			}
			catch( Exception e )
			{
				//Console.WriteLine( e.Message );
				return;
			}
		}
	}
	public class SendPingEventArgs : EventArgs
	{
		public SendPingEventArgs( IPAddress ipAddress )
		{
			IpAddress = ipAddress;
		}

		public IPAddress IpAddress { get; private set; }
	}
}
