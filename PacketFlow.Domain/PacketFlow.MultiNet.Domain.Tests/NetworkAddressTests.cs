using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace PacketFlow.MultiNet.Domain.Tests
{
	public class NetworkAddressTests
	{
		[Fact]
		public void FromString_ReturnsCorrectAddress()
		{
			var expected = new NetworkAddress(255, 255, 255, 255);

			NetworkAddress.FromString("FF.FF.FF.FF").ShouldBeEquivalentTo(expected);
		}

		[Theory, AutoData]
		public void ToString_FromString(byte part1, byte part2, byte part3, byte part4)
		{
			var original = new NetworkAddress(part1, part2, part3, part4);

			var address = original.ToString();

			NetworkAddress.FromString(address).ShouldBeEquivalentTo(original);
		}

	}
}
