using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class KeyPressListener : MonoBehaviour {

	private readonly ConcurrentQueue<StringMessage> _messageQueue = new ConcurrentQueue<StringMessage>();

	// Use this for initialization
	void Start () {
		NetworkServer.RegisterHandler(1, networkMessage =>
		{
			var message = networkMessage.ReadMessage<StringMessage>();
			_messageQueue.Enqueue(message);
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		while (!_messageQueue.IsEmpty)
		{
			StringMessage stringMessage;
			if (_messageQueue.TryDequeue(out stringMessage))
			{
				Debug.Log(stringMessage.value);
			}
		}
	}
}
