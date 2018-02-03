using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Functional.Maybe;
using Xunit;

namespace PacketFlow.MultiNet.Domain.Tests
{
	public abstract class MultiNetTestFixture
	{
		private readonly MultiNetAggregate _aggregate;

		protected MultiNetTestFixture()
		{
			_aggregate = new MultiNetAggregate();
		}

		protected Maybe<MultiNetCommand.Error> Act_HandleCommand(MultiNetCommand command)
			=> _aggregate.HandleCommand(command);

		protected void Assert_Events(params MultiNetEvent[] expected)
			=> _aggregate.UncommittedEvents.Should().ContainInOrder(expected);
	}


	public class ConnectionTests : MultiNetTestFixture
	{
		[Fact]
		public void SetConnectionStateToConnecting_ConnectionStateUpdatedToConnecting()
		{
			Act_HandleCommand(new MultiNetCommand.SetConnectionState(new ConnectionState.Connecting()));

			Assert_Events(
				new MultiNetEvent.ConnectionStateUpdated(new ConnectionState.Connecting())
			);
		}
	}
}
