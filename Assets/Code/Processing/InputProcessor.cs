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
using PacketFlow.Domain;
using Assets.Code.Processing;

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

			if (Input.GetKeyDown(KeyCode.G))
			{
				var domainCommand = new NetworkCommand.AddGatewayNode(new NodeIdentifier(), new NodePosition(0.0f, 0.0f), 10);
				var commandToSendAcrossWire = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, domainCommand);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, commandToSendAcrossWire);
			}
			/*
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsNumber() && Input.GetKeyDown(x)))
			{
				var message = new PacketFlowMessage() { senderID = SenderID, senderType = ADMIN_PLAYER_TYPE, payload = key.ToString() };
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}
			*/
		}
		else
		{
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(x => x.IsLetter() && Input.GetKeyDown(x)))
			{
				var message = new PacketFlowMessage() { senderID = SenderID, senderType = HACKER_PLAYER_TYPE, payload = key.ToString() };
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}				
		}
	}

	private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
	private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
	private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
}
