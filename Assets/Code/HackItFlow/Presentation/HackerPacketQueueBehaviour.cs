using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.HackItFlow.Presentation
{
	public class HackerPacketQueueBehaviour : MonoBehaviour
	{
		/// <summary>
		/// Called once per frame.
		/// </summary>
		void Update()
		{
			if (Event.current.Equals(Event.KeyboardEvent("space")))
			{
				// emit stuff
			}
		}
	}
}
