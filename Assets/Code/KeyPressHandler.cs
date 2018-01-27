using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class KeyPressHandler : MonoBehaviour {

	public NetworkManager networkManager;

	public NetworkClient client;

	// Use this for initialization
	void Start () {
		Debug.Log("help, I'm starting but I have no idea what I'm doing");
		client = networkManager.StartClient();
		Debug.Log("client started successfully");
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Updating - Let's send a message!");
		foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => Input.GetKeyDown(x)))
			client.Send(1, new StringMessage(key.ToString()));
		Debug.Log("I should have sent a message by now");
	}
}
