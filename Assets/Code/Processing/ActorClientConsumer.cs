using Assets.Code;
using Assets.Code.Processing;
using Assets.Code.Processing.TransportEvents.Mapping;
using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using System.Collections.Concurrent;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using static Assets.Code.Constants;

public class ActorClientConsumer : MonoBehaviour, IActorClient<NetworkEvent, NetworkCommand>
{
	private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
	private readonly ISubject<NetworkEvent> _eventSubject = new Subject<NetworkEvent>();

	public NetworkManager NetworkManagerInstance;
	public IObservable<NetworkEvent> ReceivedEvents => _eventSubject;
	private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
	private bool IsClientConnected => NetworkManagerInstance?.IsClientConnected() ?? false;

	public void SendCommand(NetworkCommand command)
	{
		// TODO: fire off presentation commands
		Debug.Log(command);
	}				
		
	void Update()
	{
		if (!IsClientConnected)
			return;

		if (!NetworkManagerInstance.client.handlers.ContainsKey(SERVER_MESSAGE_TYPE_ID))
		{
			NetworkManagerInstance.client.RegisterHandler(SERVER_MESSAGE_TYPE_ID, networkMessage =>
			{
				var message = networkMessage.ReadMessage<PacketFlowMessage>();
				_messageQueue.Enqueue(message);
			});
		}

		while (!_messageQueue.IsEmpty)
		{
			PacketFlowMessage message;
			if (!_messageQueue.TryDequeue(out message))
				continue;

			var @event = NetworkEventAndPacketFlowMessageBidirectionalMapper.Map(message);
			_eventSubject.OnNext(@event);
			Debug.Log("Client consumer received message: " + message.payload);
		}
	}		
}