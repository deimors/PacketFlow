using PacketFlow.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterInputDetector : MonoBehaviour {

	public PacketType PacketType;

	private void OnMouseDown()
	{
		bool isLeftMouseClicked = Input.GetKeyDown(KeyCode.Mouse0);

		if (isLeftMouseClicked)
		{
			//Rotate();
			UpdateDirectionToFace(PortDirection.Right);
		}
	}

	private void Rotate()
	{
		transform.Rotate(new Vector3(0, 0, 90));
	}






	private Vector3 _desiredDirection;
	private const int RotationSpeed = 5;

	// Update is called once per frame
	void Update()
	{
		float angle = Mathf.Atan2(_desiredDirection.y, _desiredDirection.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
	}

	private void UpdateDirectionToFace(PortDirection direction)
	{
		switch (direction)
		{
			case PortDirection.Right: _desiredDirection = new Vector3(0, 1) - Vector3.zero; break;
			case PortDirection.Top: _desiredDirection = new Vector3(-1, 0) - Vector3.zero; break;
			case PortDirection.Left: _desiredDirection = new Vector3(0, -1) - Vector3.zero; break;
			default:
			case PortDirection.Bottom: _desiredDirection = new Vector3(1, 0) - Vector3.zero; break;
		}
	}
}
