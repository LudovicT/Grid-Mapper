using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper.NetworkUtilities
{
	public interface IPacketReceiverClient
	{
		void Update( Packet packet );
	}
}
