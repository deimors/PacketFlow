using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Extensions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class KeyPressHandler : MonoBehaviour {

	public NetworkManager NetworkManager;

	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (!NetworkManager.IsClientConnected())
			return;

		if (NetworkServer.connections.Count > 0) // server has connections, client does not (https://stackoverflow.com/a/41685717)
		{
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsNumber() && Input.GetKeyDown(x)))
				NetworkManager.client.Send(888, new StringMessage(key.ToString()));
		}
		else
		{
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsLetter() && Input.GetKeyDown(x)))
				NetworkManager.client.Send(888, new StringMessage(key.ToString()));
		}
	}
}
