using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkLink : MonoBehaviour
{
	private LineRenderer _lineRenderer; 

	[SerializeField]
	public Vector2 StartPoint = new Vector2();
	private Vector2 _previousStartPoint = new Vector2();

	[SerializeField]
	public Vector2 EndPoint = new Vector2();
	private Vector2 _previousEndPoint = new Vector2();

	private Vector3[] _postions;

	[SerializeField]
	public Color Color = Color.white;
	private Color _previousColor = Color.white;

	private float _midpointInterpolationAmount;

	internal class TransportingGameObject
	{
		internal GameObject gameObject;
		internal float timeToLive;
		internal float lifeTime;
	}
	
	private List<TransportingGameObject> _travelingGameObjects = new List<TransportingGameObject>();

	void Start()
	{
		InitializeLineRender();
	}
		
	void FixedUpdate ()
	{
		HandleEndpointPositionChanges();
		HandleColorChanges();
		
		HandleTravelingGameObjects(Time.deltaTime);
	}

	public void AddGameObject(GameObject gameObject, float travelTime)
	{
		if (travelTime <= 0)
			travelTime = float.Epsilon;

		gameObject.transform.position = _previousStartPoint;

		_travelingGameObjects.Add(new TransportingGameObject()
		{
			gameObject = gameObject,
			timeToLive = travelTime,
			lifeTime = 0f
		});
	}


	public Vector3 GetInterpolatedPostion(float amount)
	{
		if (amount < _midpointInterpolationAmount)
		 	return Vector3.Lerp(_postions[0], _postions[1], amount / _midpointInterpolationAmount);
		
		else
			return Vector3.Lerp(_postions[1], _postions[2], (amount - _midpointInterpolationAmount) / (1 - _midpointInterpolationAmount));
	}

	private void HandleTravelingGameObjects(float deltaTime)
	{
		_travelingGameObjects.ForEach(o => o.lifeTime += deltaTime);
				
		_travelingGameObjects.RemoveAll(o => !o.gameObject.activeSelf || o.lifeTime > o.timeToLive);

		_travelingGameObjects.ForEach(o => o.gameObject.transform.position = GetInterpolatedPostion(GetTransportingGameObjectInterpolationAmount(o)));
	}

	private float GetTransportingGameObjectInterpolationAmount(TransportingGameObject gameObject)
	{
		return gameObject.lifeTime / gameObject.timeToLive;
	}

	private void InitializeLineRender()
	{
		_lineRenderer = gameObject.AddComponent<LineRenderer>();
		_lineRenderer.widthMultiplier = 0.2f;

		_lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
	}

	private void HandleColorChanges()
	{
		if (_previousColor != Color)
		{
			_previousColor = Color;
			SetLineRendererColor(_previousColor);
		}
	}

	private void HandleEndpointPositionChanges()
	{
		if (StartPoint != _previousStartPoint || EndPoint != _previousStartPoint)
		{
			_previousStartPoint = StartPoint;
			_previousEndPoint = EndPoint;
			
			_postions = CalculateLineRendererPositions(_previousStartPoint, _previousEndPoint);
			SetLineRendererPositions(_postions);

			_midpointInterpolationAmount = CalculateMidpointInterpolationAmount(_postions);
		}
	}
	
	private Vector3[] CalculateLineRendererPositions(Vector2 startPosition, Vector2 endPosition)
	{
		return new Vector3[]
		{
			new Vector3(startPosition.x, startPosition.y),
			new Vector3(endPosition.x, startPosition.y),
			new Vector3(endPosition.x, endPosition.y)
		};
	}

	private float CalculateMidpointInterpolationAmount(Vector3[] postions)
	{
		var midpointLength = Vector3.Distance(postions[0], postions[1]);			
		var totalLength = midpointLength + Vector3.Distance(postions[1], postions[2]);
		return midpointLength / totalLength;
	}

	private void SetLineRendererPositions(Vector3[] positions)
	{
		_lineRenderer.SetPositions(positions);
		_lineRenderer.positionCount = positions.Length;
	}

	private void SetLineRendererColor(Color color)
	{
		_lineRenderer.material.color = color;
	}
}
