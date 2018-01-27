﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class KeyPressListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NetworkServer.RegisterHandler(1, networkMessage =>
		{
			var message = networkMessage.ReadMessage<StringMessage>();
			Debug.Log(message.value);
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}