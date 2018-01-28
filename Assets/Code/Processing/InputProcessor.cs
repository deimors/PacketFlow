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

public class InputProcessor : MonoBehaviour
{ 
	private bool _safeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
	private int _senderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
	private static bool _isAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start () {
		
	}

	#region LOCAL FUNCTIONS ARE NOT ALLOWED 
	private NodePosition _position => new NodePosition(0.0f, 0.0f);
	private NodeIdentifier _id => new NodeIdentifier();
	private int _capacity => 10;
	private PortDirection _direction => PortDirection.Top;
	private PacketType _pt => PacketType.Blue;
	#endregion

	// Update is called once per frame
	void Update ()
	{
		if (!_safeToSend)
			return;

		if (_isAServer) // Packet
		{
			if (Input.GetKeyDown(KeyCode.G))
			{
				var command = new NetworkCommand.AddGatewayNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				var command = new NetworkCommand.AddRouterNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				var command = new NetworkCommand.AddConsumerNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				var command = new NetworkCommand.LinkNodes(_id, _direction, _id, _direction);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				var command = new NetworkCommand.AddPacket(new PacketIdentifier(), _pt, _id);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				var command = new NetworkCommand.IncrementPacketTypeDirection(_id, _pt);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, ADMIN_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}
		}
		else // Hackit
		{
			if (Input.GetKeyDown(KeyCode.G))
			{
				var command = new NetworkCommand.AddGatewayNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				var command = new NetworkCommand.AddRouterNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				var command = new NetworkCommand.AddConsumerNode(_id, _position, _capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				var command = new NetworkCommand.LinkNodes(_id, _direction, _id, _direction);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				var command = new NetworkCommand.AddPacket(new PacketIdentifier(), _pt, _id);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				var command = new NetworkCommand.IncrementPacketTypeDirection(_id, _pt);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}
		}
	}	
}
