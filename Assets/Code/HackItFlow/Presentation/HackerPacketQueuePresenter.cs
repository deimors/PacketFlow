using Assets.Code.HackItFlow.HackerPacketQueue;
using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.HackItFlow.Presentation
{
	public class HackerPacketQueuePresenter : DisposableMonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _textMesh;

		[SerializeField]
		private HackerPacketQueueItemColour _colour;

		[Inject]
		public void Initialize(IHackerPacketQueue packetQueue)
		{
			packetQueue
				.Items
				.ObserveReset()
				.Merge(packetQueue.Items.ObserveAdd().Select(_ => Unit.Default))
				.Merge(packetQueue.Items.ObserveRemove().Select(_ => Unit.Default))
				.Merge(packetQueue.Items.ObserveMove().Select(_ => Unit.Default))
				.Merge(packetQueue.Items.ObserveReplace().Select(_ => Unit.Default))
				.Subscribe(_ => UpdateText(packetQueue.Items.ToArray()));

			UpdateText(packetQueue.Items.ToArray());
		}

		private void UpdateText(IHackerPacketQueueItem[] items)
		{
			if (items.Length > 4 && items.Length < 9)
			{
				items = items
					.Take(4)
					.Concat(new IHackerPacketQueueItem[] { null })
					.Concat(items.Skip(4))
					.ToArray();
			}

			var characters = items
				.Select(item => item?.Colour == _colour ? '#' : ' ')
				.Concat(Enumerable.Repeat(' ', 9 - items.Length))
				.ToArray();

			characters = characters
				.Take(4)
				.Concat(new[] { ' ', characters[4], ' ' })
				.Concat(characters.Skip(5))
				.ToArray();

			_textMesh.text = new String(characters);
		}
	}
}
