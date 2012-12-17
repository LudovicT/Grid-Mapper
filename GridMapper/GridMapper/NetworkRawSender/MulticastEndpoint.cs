using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Net;

namespace ConsoleApplication5
{
	class MulticastEndpoint
	{

		public ArrayList localInterfaceList, multicastJoinList;

		public Socket mcastSocket;

		public IPAddress bindAddress;

		public int bufferSize;

		public int localPort;

		public byte[] dataBuffer;



		/// <summary>

		/// Simple constructor for the

		/// </summary>

		public MulticastEndpoint()
		{

			localInterfaceList = new ArrayList();

			multicastJoinList = new ArrayList();

			bufferSize = 512;

			mcastSocket = null;

		}



		/// <summary>

		/// This method creates the socket, joins it to the given multicast groups, and initializes

		/// the send/receive buffer. Note that the local bind address should be the wildcard address

		/// because it is possible to join multicast groups on one or more local interface and if

		/// a socket is bound to an explicit local interface, it can lead to user confusion (although

		/// this does currently work on Windows OSes).

		/// </summary>

		/// <param name="port">Local port to bind socket to</param>

		/// <param name="bufferLength">Length of the send/recv buffer to create</param>

		public void Create( int port, int bufferLength )
		{

			localPort = port;



			Console.WriteLine( "Creating socket, joining multicast group and" );

			Console.WriteLine( "     initializing the send/receive buffer" );

			try
			{

				// If no bind address was specified, pick an appropriate one based on the multicast

				// group being joined.

				if( bindAddress == null )
				{

					IPAddress tmpAddr = (IPAddress)multicastJoinList[0];



					if( tmpAddr.AddressFamily == AddressFamily.InterNetwork )

						bindAddress = IPAddress.Any;

					else if( tmpAddr.AddressFamily == AddressFamily.InterNetworkV6 )

						bindAddress = IPAddress.IPv6Any;

				}



				// Create the UDP socket

				Console.WriteLine( "Creating the UDP socket..." );

				mcastSocket = new Socket(

					bindAddress.AddressFamily,

					SocketType.Dgram,

					0

					);



				Console.WriteLine( "{0} multicast socket created", bindAddress.AddressFamily.ToString() );



				// Bind the socket to the local endpoint

				Console.WriteLine( "Binding the socket to the local endpoint..." );

				IPEndPoint bindEndPoint = new IPEndPoint( bindAddress, port );

				mcastSocket.Bind( bindEndPoint );

				Console.WriteLine( "Multicast socket bound to: {0}", bindEndPoint.ToString() );



				// Join the multicast group

				Console.WriteLine( "Joining the multicast group..." );

				for( int i = 0 ; i < multicastJoinList.Count ; i++ )
				{

					for( int j = 0 ; j < localInterfaceList.Count ; j++ )
					{

						// Create the MulticastOption structure which is required to join the

						//    multicast group

						if( mcastSocket.AddressFamily == AddressFamily.InterNetwork )
						{

							MulticastOption mcastOption = new MulticastOption(

								(IPAddress)multicastJoinList[i],

								(IPAddress)localInterfaceList[j]

								);



							mcastSocket.SetSocketOption(

								SocketOptionLevel.IP,

								SocketOptionName.AddMembership,

								mcastOption

								);

						}

						else if( mcastSocket.AddressFamily == AddressFamily.InterNetworkV6 )
						{

							IPv6MulticastOption ipv6McastOption = new IPv6MulticastOption(

								(IPAddress)multicastJoinList[i],

								((IPAddress)localInterfaceList[j]).ScopeId

								);



							mcastSocket.SetSocketOption(

								SocketOptionLevel.IPv6,

								SocketOptionName.AddMembership,

								ipv6McastOption

								);

						}



						Console.WriteLine( "Joined multicast group {0} on interface {1}",

							multicastJoinList[i].ToString(),

							localInterfaceList[j].ToString()

							);

					}

				}



				// Allocate the send and receive buffer

				Console.WriteLine( "Allocating the send and receive buffer..." );

				dataBuffer = new byte[bufferLength];

			}

			catch( SocketException err )
			{

				Console.WriteLine( "Exception occurred when creating multicast socket: {0}", err.Message );

				throw;

			}

		}



		/// <summary>

		/// This method drops membership to any joined groups. To do so, you have to

		/// drop the group exactly as you joined it -- that is the local interface

		/// and multicast group must be the same as when it was joined. Also note

		/// that it is not required to drop joined groups before closing a socket.

		/// When a socket is closed all multicast joins are dropped for you -- this

		/// routine just illustrates how to drop a group if you need to in the middle

		/// of the lifetime of a socket.

		/// </summary>

		public void LeaveGroups()
		{

			try
			{

				Console.WriteLine( "Dropping membership to any joined groups..." );

				for( int i = 0 ; i < multicastJoinList.Count ; i++ )
				{

					for( int j = 0 ; j < localInterfaceList.Count ; j++ )
					{

						// Create the MulticastOption structure which is required to drop the

						//    multicast group (the same structure used to join the group is

						//    required to drop it).

						if( mcastSocket.AddressFamily == AddressFamily.InterNetwork )
						{

							MulticastOption mcastOption = new MulticastOption(

								(IPAddress)multicastJoinList[i],

								(IPAddress)localInterfaceList[j]

								);



							mcastSocket.SetSocketOption(

								SocketOptionLevel.IP,

								SocketOptionName.DropMembership,

								mcastOption

								);

						}

						else if( mcastSocket.AddressFamily == AddressFamily.InterNetworkV6 )
						{

							IPv6MulticastOption ipv6McastOption = new IPv6MulticastOption(

								(IPAddress)multicastJoinList[i],

								((IPAddress)localInterfaceList[j]).ScopeId

								);



							mcastSocket.SetSocketOption(

								SocketOptionLevel.IPv6,

								SocketOptionName.DropMembership,

								ipv6McastOption

								);

						}



						Console.WriteLine( "Dropping multicast group {0} on interface {1}",

							multicastJoinList[i].ToString(),

							localInterfaceList[j].ToString()

							);

					}

				}

			}

			catch
			{

				Console.WriteLine( "LeaveGroups: No multicast groups joined" );

			}

		}



		/// <summary>

		/// This method sets the outgoing interface when a socket sends data to a multicast

		/// group. Because multicast addresses are not routable, the network stack simply

		/// picks the first interface in the routing table with a multicast route. In order

		/// to change this behavior, the MulticastInterface option can be used to set the

		/// local interface on which all outgoing multicast traffic is to be sent (for this

		/// socket only). This is done by converting the 4 byte IPv4 address (or 16 byte

		/// IPv6 address) into a byte array.

		/// </summary>

		/// <param name="sendInterface"></param>

		public void SetSendInterface( IPAddress sendInterface )
		{

			// Set the outgoing multicast interface

			try
			{

				Console.WriteLine( "Setting the outgoing multicast interface..." );

				if( mcastSocket.AddressFamily == AddressFamily.InterNetwork )
				{

					mcastSocket.SetSocketOption(

						SocketOptionLevel.IP,

						SocketOptionName.MulticastInterface,

						sendInterface.GetAddressBytes()

						);

				}

				else
				{

					byte[] interfaceArray = BitConverter.GetBytes( (int)sendInterface.ScopeId );



					mcastSocket.SetSocketOption(

						SocketOptionLevel.IPv6,

						SocketOptionName.MulticastInterface,

						interfaceArray

						);

				}

				Console.WriteLine( "Setting multicast send interface to: " + sendInterface.ToString() );

			}

			catch( SocketException err )
			{

				Console.WriteLine( "SetSendInterface: Unable to set the multicast interface: {0}", err.Message );

				throw;

			}

		}



		/// <summary>

		/// This method takes a string and repeatedly copies it into the send buffer

		/// to the length of the send buffer.

		/// </summary>

		/// <param name="message">String to copy into send buffer</param>

		public void FormatBuffer( string message )
		{

			byte[] byteMessage = System.Text.Encoding.ASCII.GetBytes( message );

			int index = 0;



			// First convert the string to bytes and then copy into send buffer

			Console.WriteLine( "Formatting the send buffer..." );

			while( index < dataBuffer.Length )
			{

				for( int j = 0 ; j < byteMessage.Length ; j++ )
				{

					dataBuffer[index] = byteMessage[j];

					index++;



					// Make sure we don't go past the send buffer length

					if( index >= dataBuffer.Length )
					{

						break;

					}

				}

			}

		}

	}
}
