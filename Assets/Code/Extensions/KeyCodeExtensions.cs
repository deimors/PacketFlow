using UnityEngine;

namespace Assets.Code.Extensions
{
	internal static class KeyCodeExtensions
	{
		public static bool IsLetter(this KeyCode code) => ((int) code >= (int) KeyCode.A) && ((int) code <= (int)KeyCode.Z);
		public static bool IsNumber(this KeyCode code) => (((int) code >= (int) KeyCode.Alpha0) && ((int) code <= (int) KeyCode.Alpha9)) || (((int) code >= (int) KeyCode.Keypad0) && ((int) code <= (int) KeyCode.Keypad9));
	}
}