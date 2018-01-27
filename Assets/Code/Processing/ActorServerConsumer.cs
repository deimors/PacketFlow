using Assets.Code;
using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Networking;
using static Assets.Code.Constants;

namespace Assets.Code.Processing
{
	public class ActorServerConsumer<TEvent> : MonoBehaviour, IActorServer<TEvent, NetworkCommand>
	{
		private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
		private readonly ClientEventDispatcher _eventDispatcher = new ClientEventDispatcher();

		public NetworkManager NetworkManagerInstance;			

		void Update()
		{

		}

		public void SendEvent(TEvent @event)
		{
			throw new NotImplementedException();
		}

		private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
		private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
		private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)

		public IObservable<NetworkCommand> ReceivedCommands
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
internal class ActorServerEventDispatcher
{
	public void Dispatch(PacketFlowMessage message)
	{
		Debug.Log("Server received message: " + message.payload);
	}
}