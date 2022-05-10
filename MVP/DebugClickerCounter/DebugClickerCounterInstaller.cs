using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class DebugClickerCounterInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<DebugClickerCounter.DebugClickerCounterModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.DebugClickerCounterPresenter>().AsCached().NonLazy();
		}
	}
}