using PacketFlow.Domain;

namespace PacketFlow.UseCases
{
	public interface IDisplayArrowDirection
	{
		void Display(PacketType packetType, PortDirection direction);
	}
}
