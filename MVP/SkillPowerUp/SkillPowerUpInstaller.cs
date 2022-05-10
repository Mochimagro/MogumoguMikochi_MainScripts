using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class SkillPowerUpInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<SkillPowerUp.SkillPowerUpModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.SkillPowerUpPresenter>().AsCached().NonLazy();
		}
	}
}