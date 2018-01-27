using Assets.Code;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using static Assets.Code.Constants;

public class KeyPressListener : MonoBehaviour {

	private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();

	// Use this for initialization
	void Start () {
		NetworkServer.RegisterHandler(MESSAGE_TYPE_ID, networkMessage =>
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
			{
				Debug.Log(message.payload);
			}
		}
	}
}
