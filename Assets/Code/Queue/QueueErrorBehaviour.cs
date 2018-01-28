using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QueueErrorBehaviour : MonoBehaviour
{
	public Image ParentImage;
	private DateTime _errorUntil;

	[Inject]
	public void Initialize(IPacketLossEvent packetLossEvent) 
	{
		packetLossEvent	// Here until it is hooked up with an event
			.Subscribe(_ => _errorUntil = DateTime.Now.AddSeconds(2));
	}

	private void UpdateErrorColorState()
	{
		if (DateTime.Now < _errorUntil)
			ParentImage.color = Color.Lerp(Color.black, Color.red, Mathf.PingPong(Time.time * 3, 1));
		else
		{
			if (ParentImage.color != Color.black)
				ParentImage.color = Color.black;
		}
	}

	void Update()
	{
		UpdateErrorColorState();
	}
}

public interface IPacketLossEvent : IObservable<Unit>
{
}
