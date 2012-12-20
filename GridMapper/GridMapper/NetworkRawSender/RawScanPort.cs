using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace GridMapper.NetworkRawSender
{
	public class RawScanPort
	{
		Socket _portScanSocket;
		IPEndPoint _localEndPoint;
		IPAddress _ipAddress;
		ushort _port;

		public event EventHandler<ScanCompletedEventArgs> ScanCompleted;

		public RawScanPort()
		{
			try
			{
				_portScanSocket = new Socket( AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Tcp );
				_portScanSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, 16 );
				_portScanSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.NoDelay, true );
				_portScanSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.Linger, 10 );

				//_portScanSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.DontRoute, true );

				//_localEndPoint = new IPEndPoint( IPAddress.Parse("192.168.1.21"), 65000 );
				//_portScanSocket.Bind( _localEndPoint );
			}
			catch( Exception e )
			{
				Console.WriteLine( e.Message );
			}
		}

		public void ScanWithConnect(IPAddress ipAddress, int port)
		{
			try
			{
				_ipAddress = ipAddress;
				_port = (ushort)port;
				EndPoint endPoint = new IPEndPoint(ipAddress, port);
				_portScanSocket.BeginConnect( endPoint, new AsyncCallback( receiveConnect ), _portScanSocket );
			}
			catch( Exception e )
			{
				Console.WriteLine( e.Message );
			}
		}

		public static void receiveConnect( IAsyncResult ar )
		{
			try
			{
				RawScanPort ScanSocket = (RawScanPort)ar.AsyncState;
				ScanSocket._portScanSocket.EndConnect( ar );
				ScanSocket.ScanCompleted( ScanSocket, new ScanCompletedEventArgs( ScanSocket._ipAddress, ScanSocket._port ) );
				Console.WriteLine( "c'est true" );
			}
			catch( Exception e )
			{
				Console.WriteLine( e.Message );
			}
		}
	}

	public class ScanCompletedEventArgs : EventArgs
	{
		public ScanCompletedEventArgs(IPAddress ipAddress, ushort port)
		{
			IpAddress = ipAddress;
			Port = port;
		}

		public IPAddress IpAddress { get; private set; }
		public ushort Port { get; private set; }
	}
}
