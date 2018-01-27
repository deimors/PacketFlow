using Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Extensions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using static Assets.Code.Constants;

public class InputProcessor : MonoBehaviour {

	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (!SafeToSend)
			return;

		if (IsAServer) 
		{
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsNumber() && Input.GetKeyDown(x)))
			{
				var message = new PacketFlowMessage() { senderID = SenderID, senderType = ADMIN_TYPE, payload = key.ToString() };
				NetworkManagerInstance.client.Send(MESSAGE_TYPE_ID, message);
				NetworkServer.SendToAll(MESSAGE_TYPE_ID, message);
			}
		}
		else
		{
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsLetter() && Input.GetKeyDown(x)))
				NetworkManagerInstance.client.Send(MESSAGE_TYPE_ID, new PacketFlowMessage() { senderID = SenderID, senderType = HACKER_TYPE, payload = key.ToString() });
		}
	}

	private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
	private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
	private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
}
