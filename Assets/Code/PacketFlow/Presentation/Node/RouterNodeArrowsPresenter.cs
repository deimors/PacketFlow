using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RouterNodeArrowsPresenter : MonoBehaviour, IArrowClickedObservable
{
	public RouterInputDetector[] ArrowInputDetectors;

	private readonly ISubject<PacketType> _subject = new Subject<PacketType>();

	void Start()
	{
		foreach (var arrow in ArrowInputDetectors)
		{
			arrow.Subscribe(_subject);
		}
	}
		
	public IDisposable Subscribe(IObserver<PacketType> observer)
	{
		return _subject.Subscribe(observer);
	}
}
