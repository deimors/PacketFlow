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

public class ActorServerConsumer : MonoBehaviour, IActorServer<NetworkEvent, NetworkCommand>
{
	private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();

	private readonly ISubject<NetworkCommand> _commandSubject = new Subject<NetworkCommand>();
	public IObservable<NetworkCommand> ReceivedCommands => _commandSubject;

	public NetworkManager NetworkManagerInstance;

	public void SendEvent(NetworkEvent @event)
	{
		var eventToSendAcrossWire = NetworkEventAndPacketFlowMessageBidirectionalMapper.Map(@event);
		NetworkServer.SendToAll(SERVER_MESSAGE_TYPE_ID, eventToSendAcrossWire);
	}

	void Start()
	{
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

	void Update()
	{
		while (!_messageQueue.IsEmpty)
		{
			PacketFlowMessage message;
			if (_messageQueue.TryDequeue(out message))
			{
				var command = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(message);
				_commandSubject.OnNext(command);
			}
		}
	}		
}