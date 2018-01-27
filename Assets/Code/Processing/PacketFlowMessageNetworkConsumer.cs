using Assets.Code;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using static Assets.Code.Constants;

/// <summary>
/// PacketFlow
/// </summary>
public class PacketFlowMessageNetworkConsumer : MonoBehaviour {

	private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
	private readonly ServerEventDispatcher _eventDispatcher = new ServerEventDispatcher();

	// Use this for initialization
	void Start () {
		NetworkServer.RegisterHandler(HACKER_PLAYER_MESSAGE_TYPE_ID, networkMessage =>
		{
			var message = networkMessage.ReadMessage<PacketFlowMessage>();
			_messageQueue.Enqueue(message);
		});

		NetworkServer.RegisterHandler(ADMIN_PLAYER_MESSAGE_TYPE_ID, networkMessage =>
		{
			var message = networkMessage.ReadMessage<PacketFlowMessage>();
			_messageQueue.Enqueue(message);
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		while (!_messageQueue.IsEmpty)
		{
			PacketFlowMessage message;
			if (_messageQueue.TryDequeue(out message))
				_eventDispatcher.Dispatch(message);
		}
	}
}

internal class ServerEventDispatcher
{
	public void Dispatch(PacketFlowMessage message)
	{
		// TODO: to actor proxy or whatever
		Debug.Log(message.payload);
	}
}
