using System;
using System.Collections.Generic;
using UniRx;
using PacketFlow.Domain;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public class NetworkActor : Actor<NetworkEvent, NetworkCommand, NetworkError>
	{
		private readonly NetworkAggregate _workshopAggregate = new NetworkAggregate();

		public NetworkActor(IObservable<Unit> processQueueTicks) : base(processQueueTicks) { }

		protected override IHandleCommand<NetworkCommand, NetworkError> CommandHandler
			=> _workshopAggregate;

		protected override IEnumerable<NetworkEvent> UncommittedEvents
			=> _workshopAggregate.UncommittedEvents;

		protected override void MarkCommitted()
			=> _workshopAggregate.MarkCommitted();
	}
}
