using PacketFlow.Domain;

namespace PacketFlow.UseCases
{
	public interface IDisplayLink
	{
		void DisplayLink(NodePosition sourcePosition, NodePosition sinkPosition);
	}
}