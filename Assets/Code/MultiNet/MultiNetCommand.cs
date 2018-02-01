using OneOf;

namespace MultiNet.Photon
{
	public abstract class MultiNetCommand : OneOfBase<MultiNetCommand.Connect>
	{
		public class Connect : MultiNetCommand { }
	}
}