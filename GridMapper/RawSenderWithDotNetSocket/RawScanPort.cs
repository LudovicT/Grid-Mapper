﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

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
				_portScanSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				_portScanSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, 16 );

				_portScanSocket.DontFragment = true;
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

		public void receiveConnect( IAsyncResult ar )
		{
			try
			{
				Socket ScanSocket = (Socket)ar.AsyncState;
				ScanSocket.EndConnect( ar );
				//ScanSocket.ScanCompleted( ScanSocket, new ScanCompletedEventArgs( ScanSocket._ipAddress, ScanSocket ) );
				Console.WriteLine( "c'est true " + _ipAddress + " " + _port );
				_portScanSocket.Close();
			}
			catch( Exception e )
			{
				_portScanSocket.Close();
				//Console.WriteLine( e.Message );
			}
		}
	}

	public class TimeOutSocket
	{
		public static void Connect( IPEndPoint remoteEndPoint, int timeoutMSec )
		{
			TcpClient tcpclient = new TcpClient();
			tcpclient.NoDelay = false;
			tcpclient.LingerState = new LingerOption(false, 0);

			IAsyncResult asyncResult = tcpclient.BeginConnect( remoteEndPoint.Address, remoteEndPoint.Port, null, null );
			Task.Factory.StartNew( () =>
				{
					if ( asyncResult.AsyncWaitHandle.WaitOne( timeoutMSec, false ) )
					{
						try
						{
							tcpclient.EndConnect( asyncResult );
							Console.WriteLine( "c'est true " + remoteEndPoint.Address + " " + remoteEndPoint.Port );
							tcpclient.Close();
							return true;
						}
						catch
						{
							tcpclient.Close();
							return false;
						}
					}
					else
					{
						tcpclient.Close();
						return false;
					}
				}
			);
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
