namespace PacketFlow.MultiNet.Domain
{
	public class Network
	{
		public Network(NetworkAddress address)
		{
			Address = address;
		}

		public NetworkAddress Address { get; }
	}
}
