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

	#region LOCAL FUNCTIONS ARE NOT ALLOWED :(
	private NodePosition Position => new NodePosition(0.0f, 0.0f);
	private NodeIdentifier ID => new NodeIdentifier();
	private int Capacity => 10;
	private PortDirection Direction => PortDirection.Top;
	private PacketType PT => PacketType.Blue;
	#endregion

	// Update is called once per frame
	void Update ()
	{
		if (!SafeToSend)
			return;

		if (IsAServer) // Packet
		{
			if (Input.GetKeyDown(KeyCode.G))
			{
				var command = new NetworkCommand.AddGatewayNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				var command = new NetworkCommand.AddRouterNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				var command = new NetworkCommand.AddConsumerNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				var command = new NetworkCommand.LinkNodes(ID, Direction, ID, Direction);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				var command = new NetworkCommand.AddPacket(new PacketIdentifier(), PT, ID);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				var command = new NetworkCommand.IncrementPacketTypeDirection(ID, PT);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, ADMIN_PLAYER_TYPE, command);
				NetworkServer.SendToAll(ADMIN_PLAYER_MESSAGE_TYPE_ID, message);
			}		
		}
		else // Hackit
		{
			if (Input.GetKeyDown(KeyCode.G))
			{
				var command = new NetworkCommand.AddGatewayNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				var command = new NetworkCommand.AddRouterNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				var command = new NetworkCommand.AddConsumerNode(ID, Position, Capacity);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				var command = new NetworkCommand.LinkNodes(ID, Direction, ID, Direction);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				var command = new NetworkCommand.AddPacket(new PacketIdentifier(), PT, ID);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				var command = new NetworkCommand.IncrementPacketTypeDirection(ID, PT);
				var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
				NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
			}
		}
	}

	

	private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
	private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
	private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
}
