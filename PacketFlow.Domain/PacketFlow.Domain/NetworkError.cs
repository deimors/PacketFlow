namespace PacketFlow.Domain
{
	public enum NetworkError
	{
		UnknownNode,
		PacketAlreadyAdded,
		QueueFull,
		PortFull,
		NodeNotRouter,
		PortDisconnected,
		NoGatewayOutput
	}
}
