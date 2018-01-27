using UnityEngine;
using System.Collections;

public class NetworkLinkTester : MonoBehaviour
{
	private NetworkLink _networkLink;


	// Use this for initialization
	void Start()
	{
		_networkLink = gameObject.AddComponent<NetworkLink>();

		_networkLink.StartPoint.x = -3f;
		_networkLink.StartPoint.y = -3f;

		_networkLink.EndPoint.x = 3f;
		_networkLink.EndPoint.y = 3f;

		
	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 50, 50), "send"))
		{
			var timeToLive = Random.Range(4f, 10f);
			var packet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Destroy(packet, timeToLive);

			_networkLink.AddGameObject(packet, timeToLive);



		}
	}

}
