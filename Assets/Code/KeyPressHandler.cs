using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class KeyPressHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => Input.GetKeyDown(x)))
			NetworkServer.SendToAll(1, new StringMessage(key.ToString()));

	}
}
