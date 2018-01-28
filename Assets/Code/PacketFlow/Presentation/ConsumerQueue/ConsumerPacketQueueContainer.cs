using Assets.Code.PacketFlow.UseCases;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Code.PacketFlow.Presentation.ConsumerQueue
{
	class ConsumerPacketQueueContainer : MonoInstaller
	{ 
			public class Factory : Factory<Queue<GameObject>, ConsumerPacketQueueContainer> { }

			[Inject]
			public Queue<GameObject> GameObjectQueue { set; get; }

			public override void InstallBindings()
			{
				Container.BindInstance(GameObjectQueue);
				Container.BindInstance(this);

				Container.Bind<UpdateQueueOnPacketEnqueued>().AsSingle().NonLazy();
			}
		}
	}
}
