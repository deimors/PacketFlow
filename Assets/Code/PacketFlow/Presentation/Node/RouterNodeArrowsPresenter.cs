using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RouterNodeArrowsPresenter : MonoBehaviour, IArrowClickedObservable, IDisplayArrowDirection
{
	public RouterInputDetectorAndHandler RedInputDetector;
	public RouterInputDetectorAndHandler GreenInputDetector;
	public RouterInputDetectorAndHandler BlueInputDetector;

	private readonly ISubject<PacketType> _subject = new Subject<PacketType>();

	void Start()
	{
		RedInputDetector.Subscribe(_subject);
		GreenInputDetector.Subscribe(_subject);
		BlueInputDetector.Subscribe(_subject);
	}
		
	public IDisposable Subscribe(IObserver<PacketType> observer)
	{
		return _subject.Subscribe(observer);
	}

	public void Display(PacketType packetType, PortDirection direction)
	{
		switch (packetType)
		{
			case PacketType.Red: RedInputDetector.UpdateDirectionToFace(direction); break;
			case PacketType.Green: GreenInputDetector.UpdateDirectionToFace(direction); break;
			case PacketType.Blue: BlueInputDetector.UpdateDirectionToFace(direction); break;
		}
	}
}
