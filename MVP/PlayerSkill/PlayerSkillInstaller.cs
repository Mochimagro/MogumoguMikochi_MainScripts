using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class PlayerSkillInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<PlayerSkill.PlayerSkillModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.PlayerSkillPresenter>().AsCached().NonLazy();
		}
	}
}