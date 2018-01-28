using PacketFlow.Domain;

namespace PacketFlow.UseCases
{
	public interface IDisplayPacketTransmission
	{
		void Display(PacketType type, float time);
	}
}