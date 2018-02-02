namespace PacketFlow.MultiNet.Domain
{
	public class Network
	{
		public Network(NetworkIdentifier id)
		{
			Id = id;
		}

		public NetworkIdentifier Id { get; }
	}
}
