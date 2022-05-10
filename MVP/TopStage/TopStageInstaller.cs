using UnityEngine;
using Zenject;

namespace MikochiClicker.Game.Installer
{
	public class TopStageInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<TopStage.TopStageModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.TopStagePresenter>().AsCached().NonLazy();
		}
	}
}