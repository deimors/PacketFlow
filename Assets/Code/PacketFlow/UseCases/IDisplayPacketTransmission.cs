using PacketFlow.Domain;

namespace PacketFlow.UseCases
{
	public interface IDisplayPacketTransmission
	{
		void Display(PacketIdentifier packetId, float time);
	}
}