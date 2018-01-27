using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => Input.GetKeyDown(x)))
			NetworkManager.client.Send(888, new StringMessage(key.ToString()));
	}
}
