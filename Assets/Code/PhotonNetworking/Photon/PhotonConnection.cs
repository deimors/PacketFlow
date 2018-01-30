using Photon;
using System;
using UniRx;

namespace PhotonNetworking.Photon
{
	public class PhotonConnection : PunBehaviour, IConnection
	{
		private readonly ISubject<ConnectionEvent> _connectionSubject = new Subject<ConnectionEvent>();

		void Start()
		{
			ConnectionEvents.Subscribe(_connectionSubject);
		}

		public void Connect()
		{
			PhotonNetwork.ConnectUsingSettings("v0.1");
		}
		
		public void Disconnect()
		{
			PhotonNetwork.Disconnect();
		}

		public IDisposable Subscribe(IObserver<ConnectionEvent> observer)
			=> _connectionSubject.Subscribe(observer);
		
		private static IObservable<ConnectionEvent> ConnectionEvents
			=> Observable
				.EveryUpdate()
				.Select(_ => PhotonNetwork.connectionState)
				.DistinctUntilChanged()
				.Select(ToConnectionEvent);

		private static ConnectionEvent ToConnectionEvent(ConnectionState newState)
		{
			switch (newState)
			{
				case ConnectionState.Connecting: return new ConnectionEvent.Connecting();
				case ConnectionState.Connected: return new ConnectionEvent.Connected();
				case ConnectionState.Disconnecting: return new ConnectionEvent.Disconnecting();
				default: return new ConnectionEvent.Disconnected();
			}
		}
	}
}