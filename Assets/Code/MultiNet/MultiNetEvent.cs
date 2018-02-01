using OneOf;

namespace MultiNet.Photon
{
	public abstract class MultiNetEvent : OneOfBase<MultiNetEvent.Connected>
	{
		public class Connected : MultiNetEvent { }
	}
}