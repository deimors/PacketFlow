using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PacketFlow.MultiNet.Domain
{
	public class NetworkAddress : IEquatable<NetworkAddress>
	{
		public NetworkAddress(byte part1, byte part2, byte part3, byte part4)
		{
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
		}

		public byte Part1 { get; }
		public byte Part2 { get; }
		public byte Part3 { get; }
		public byte Part4 { get; }

		public override bool Equals(object obj) 
			=> Equals(obj as NetworkAddress);

		public bool Equals(NetworkAddress other) 
			=> other != null 
				&& Part1 == other.Part1 
				&& Part2 == other.Part2 
				&& Part3 == other.Part3 
				&& Part4 == other.Part4;

		public override int GetHashCode()
		{
			var hashCode = 1823247110;
			hashCode = hashCode * -1521134295 + Part1.GetHashCode();
			hashCode = hashCode * -1521134295 + Part2.GetHashCode();
			hashCode = hashCode * -1521134295 + Part3.GetHashCode();
			hashCode = hashCode * -1521134295 + Part4.GetHashCode();
			return hashCode;
		}

		public override string ToString()
			=> $"{Part1:X2}.{Part2:X2}.{Part3:X2}.{Part4:X2}";

		public static NetworkAddress FromString(string address)
		{
			var match = Regex.Match(address, @"^([A-F0-9]{2}).([A-F0-9]{2}).([A-F0-9]{2}).([A-F0-9]{2})$");

			if (!match.Success) throw new Exception();

			var bytes = match.Groups
				.Cast<Group>()
				.Skip(1)
				.Select(group => byte.Parse(group.Value, System.Globalization.NumberStyles.HexNumber))
				.ToArray();

			return new NetworkAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
		}
	}
}
