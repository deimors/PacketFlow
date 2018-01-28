using Assets.Code.Utilities;
using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;

namespace Assets.Code.SLAMeter
{
	public class SLAMeterPresenter : DisposableMonoBehaviour
	{
		[SerializeField]
		private LineRenderer _lineRenderer;

		private ISLAMeter _slaMeter;
		private IObservable<NetworkEvent> _networkEvents;

		[SerializeField]
		public Vector2 BottomPoint = new Vector2();

		[SerializeField]
		public Vector2 TopPoint = new Vector2();

		[Inject]
		public void Initialize(ISLAMeter slaMeter, IObservable<NetworkEvent> networkEvents)
		{
			_slaMeter = slaMeter;
			_networkEvents = networkEvents;

			_slaMeter.InitializeMeter(10.0f, 10);

			_networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketLost>()
				.Subscribe(packetLost =>
				{
					if (!_slaMeter.TryAddPackets(1, PacketStatus.Failure))
					{
						// lose game?
					}
				});

			_networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketConsumed>()
				.Subscribe(packetConsumed =>
				{
					if (!_slaMeter.TryAddPackets(1, PacketStatus.Success))
					{
						// lose game?
					}
				});

			_slaMeter
				.SucceededCount
				.Subscribe(_ => UpdateMeter())
				.DisposeWith(this);

			_slaMeter
				.FailedCount
				.Subscribe(_ => UpdateMeter())
				.DisposeWith(this);
		}

		private void UpdateMeter()
		{
			var startPosition = _lineRenderer.GetPosition(0);
			var currentPosition = _lineRenderer.GetPosition(1);

			var total = _slaMeter.SucceededCount.Value + _slaMeter.FailedCount.Value;
			var successPercentage = _slaMeter.SucceededCount.Value / (float)total;
			var height = BottomPoint.y + successPercentage;

			_lineRenderer.SetPosition(1, new Vector3(currentPosition.x, height));
		}
	}
}
