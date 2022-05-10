using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class TakoyakiMakersInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<TakoyakiMakers.TakoyakiMakersModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.TakoyakiMakersPresenter>().AsCached().NonLazy();
		}
	}
}