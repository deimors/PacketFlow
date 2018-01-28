namespace Assets.Code.Processing.TransportCommands
{
	public enum TransportCommandPayloadType
	{
		AddGatewayNode = 0,
		AddRouterNode,
		AddConsumerNode,
		LinkNodes,
		AddPacket,
		IncrementPacketTypeDirection,
		ProcessNodeQueue,
		CompleteTransmission
	}		
}
