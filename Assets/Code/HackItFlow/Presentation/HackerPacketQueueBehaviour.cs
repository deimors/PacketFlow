using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Code.HackItFlow.HackerPacketQueue;
using Zenject;

namespace Assets.Code.HackItFlow.Presentation
{
	public class HackerPacketQueueBehaviour : MonoBehaviour
	{
		private IHackerPacketQueue _queue;

		[Inject]
		public void Initialize(IHackerPacketQueue queue)
		{
			_queue = queue;
		}

		/// <summary>
		/// Called once per frame.
		/// </summary>
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// emit stuff
			}
		}
	}
}
