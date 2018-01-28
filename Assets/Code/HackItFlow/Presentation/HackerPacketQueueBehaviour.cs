using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Code.HackItFlow.HackerPacketQueue;
using Zenject;
using Assets.Code.HackItFlow.CommandLineSystem;

namespace Assets.Code.HackItFlow.Presentation
{
	public class HackerPacketQueueBehaviour : MonoBehaviour
	{
		private IHackerPacketQueue _queue;
		private ICommandLineConsole _commandLineConsole;

		[Inject]
		public void Initialize(IHackerPacketQueue queue, ICommandLineConsole commandLineConsole)
		{
			_queue = queue;
			_commandLineConsole = commandLineConsole;
		}

		/// <summary>
		/// Called once per frame.
		/// </summary>
		void Update()
		{
			bool tryAddItem = false;
			HackerPacketQueueItemColour colour = HackerPacketQueueItemColour.Red;
			HackerPacketQueueItemType type = HackerPacketQueueItemType.Type1;

			if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			{
				tryAddItem = true;
				colour = HackerPacketQueueItemColour.Red;
				type = HackerPacketQueueItemType.Type1;
			}
			else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			{
				tryAddItem = true;
				colour = HackerPacketQueueItemColour.Blue;
				type = HackerPacketQueueItemType.Type2;
			}
			else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			{
				tryAddItem = true;
				colour = HackerPacketQueueItemColour.Green;
				type = HackerPacketQueueItemType.Type3;
			}

			if (tryAddItem)
			{
				if (_queue.TryAddItem(type, colour))
				{
					_commandLineConsole.AddText($"inject-packet {colour.ToString()}");
				}
				else
				{
					_commandLineConsole.AddText($"inject-packet {colour.ToString()}", "SYSTEM ERROR, PACKET REJECTED", string.Empty);
				}
			}
		}
	}
}
