using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using UnityEngine.Networking;
using static Assets.Code.Constants;

/// <summary>
/// HackItFlow
/// </summary>
public class PacketFlowMessageClientConsumer : MonoBehaviour
{
	private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
	private readonly ClientEventDispatcher _eventDispatcher = new ClientEventDispatcher();

	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!NetworkManagerInstance.IsClientConnected())
			return;

		if (!NetworkManagerInstance.client.handlers.ContainsKey(ADMIN_PLAYER_MESSAGE_TYPE_ID))
		{
			NetworkManagerInstance.client.RegisterHandler(ADMIN_PLAYER_MESSAGE_TYPE_ID, networkMessage =>
			{
				var message = networkMessage.ReadMessage<PacketFlowMessage>();
				_messageQueue.Enqueue(message);
			});
		}

		while (!_messageQueue.IsEmpty)
		{
			PacketFlowMessage message;
			if (_messageQueue.TryDequeue(out message))
				_eventDispatcher.Dispatch(message);
		}
	}
}

internal class ClientEventDispatcher
{
	public void Dispatch(PacketFlowMessage message)
	{
		Debug.Log("Client received message: " + message.payload);
	}
}