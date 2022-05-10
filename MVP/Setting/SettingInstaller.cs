using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class SettingInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<Setting.SettingModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.SettingPresenter>().AsCached().NonLazy();
		}
	}
}