using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerBootup : MonoBehaviour {

	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start ()
	{
		var manager = NetworkManagerInstance;
		manager.StartMatchMaker();
		manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
		manager.matchMaker.CreateMatch(manager.matchName, 2, true, "", "", "", 0, 0, (success, extendedInfo, matchInfo) =>
		{
			if (!success) return;
			Debug.Log("Attempting to start server... " + matchInfo.ToString());
			manager.StartServer(matchInfo);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		var manager = NetworkManagerInstance;
		manager.StopClient();
		manager.StopServer();
		manager.StopMatchMaker();
	}
}
