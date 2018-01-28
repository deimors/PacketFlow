using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ClientBootup : MonoBehaviour {

	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start ()
	{
		var manager = NetworkManagerInstance;
		manager.StartMatchMaker();
		manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
		manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, (listSuccess, listExtendedInfo, matchInfoSnapshot) =>
		{
			Debug.Log("Attempting to retrieve available matches... " + string.Join(", ", matchInfoSnapshot.Select(x => x.ToString())));
			if (!listSuccess || (matchInfoSnapshot.Count == 0))
				return;

			var firstAvailableMatch = matchInfoSnapshot.First();
			manager.matchName = firstAvailableMatch.name;
			manager.matchMaker.JoinMatch(firstAvailableMatch.networkId, "", "", "", 0, 0, (success, extendedInfo, matchInfo) =>
			{
				if (!success) return;
				Debug.Log("Attempting to start client... " + matchInfo.ToString());
				manager.client = manager.StartClient(matchInfo);
				//manager.client.Send(Constants.SERVER_CLIENT_CONNECT, new StringMessage("yo sup"));
			});
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		var manager = NetworkManagerInstance;
		manager.StopClient();
		manager.StopMatchMaker();
	}
}
