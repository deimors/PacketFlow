using Assets.Code.HackItFlow.CommandLineSystem;
using Assets.Code.HackItFlow.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.HackItFlow.IoC
{
	public class CommandLineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(IObserver<CommandLineTextAddedEvent>), typeof(IObservable<CommandLineTextAddedEvent>)).FromInstance(new ReplaySubject<CommandLineTextAddedEvent>());

			Container.BindInterfacesAndSelfTo<CommandLineConsole>().AsSingle();
		}
	}
}
