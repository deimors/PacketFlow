using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RouterInputDetectorAndHandler : MonoBehaviour, IArrowClickedObservable
{
	public PacketType PacketType;
	private readonly ISubject<PacketType> _arrowClickedSubject = new Subject<PacketType>();
	private Vector3 _desiredDirection;
	private const int RotationSpeed = 5;

	private static Vector3 RightDirection = new Vector3(0, 1) - Vector3.zero;
	private static Vector3 LeftDirection = new Vector3(0, -1) - Vector3.zero;
	private static Vector3 TopDirection = new Vector3(-1, 0) - Vector3.zero;
	private static Vector3 BottomDirection = new Vector3(1, 0) - Vector3.zero;

	public IDisposable Subscribe(IObserver<PacketType> observer)
	{
		return _arrowClickedSubject.Subscribe(observer);
	}

	private void OnMouseDown()
	{
		bool isLeftMouseButtonClicked = Input.GetKeyDown(KeyCode.Mouse0);
		if (isLeftMouseButtonClicked)
		{
			_arrowClickedSubject.OnNext(PacketType);
		}
	}

	void Update()
	{
		float angle = Mathf.Atan2(_desiredDirection.y, _desiredDirection.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
	}

	public void UpdateDirectionToFace(PortDirection direction)
	{
		switch (direction)
		{
			case PortDirection.Right: _desiredDirection = RightDirection; break;
			case PortDirection.Top: _desiredDirection = TopDirection; break;
			case PortDirection.Left: _desiredDirection = LeftDirection; break;
			default:
			case PortDirection.Bottom: _desiredDirection = BottomDirection; break;
		}
	}
}
