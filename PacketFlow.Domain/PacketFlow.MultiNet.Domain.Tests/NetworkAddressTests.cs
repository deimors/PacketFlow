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

		[Theory]
		[InlineData(0, 0, 0, 0, "00.00.00.00")]
		[InlineData(0x1, 0x8, 0xC, 0xF, "01.08.0C.0F")]
		[InlineData(0xA3, 0xF, 0x00, 0x20, "A3.0F.00.20")]
		public void AddressToString_IsExpected(byte part1, byte part2, byte part3, byte part4, string expected)
		{
			new NetworkAddress(part1, part2, part3, part4).ToString().Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 0, 0, 0, "00.00.00.00")]
		[InlineData(0x1, 0x8, 0xC, 0xF, "01.08.0C.0F")]
		[InlineData(0xA3, 0xF, 0x00, 0x20, "A3.0F.00.20")]
		public void AddressFromString_IsExpected(byte part1, byte part2, byte part3, byte part4, string expected)
		{
			NetworkAddress.FromString(expected).ShouldBeEquivalentTo(new NetworkAddress(part1, part2, part3, part4));
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
