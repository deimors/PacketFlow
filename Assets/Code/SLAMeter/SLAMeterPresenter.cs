using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Code.SLAMeter
{
	public class SLAMeterPresenter : MonoBehaviour
	{
		[SerializeField]
		private LineRenderer _lineRenderer;

		private ISLAMeter _slaMeter;

		[SerializeField]
		public Vector2 BottomPoint = new Vector2();

		[SerializeField]
		public Vector2 TopPoint = new Vector2();

		[Inject]
		public void Initialize(ISLAMeter slaMeter)
		{
			_slaMeter = slaMeter;

			_slaMeter.InitializeThreashold(100);

			_slaMeter
				.CurrentPackets
				.Subscribe(_ => UpdateLine(_slaMeter.CurrentPackets.Value)).DisposeWith(this);
		}

		private void UpdateLine(int numOfPackets)
		{
			var startPosition = _lineRenderer.GetPosition(0);
			var currentPosition = _lineRenderer.GetPosition(1);

			float barHeight = ((float)_slaMeter.CurrentPackets / (float)_slaMeter.MaxPackets) - 
		}
	}
}
