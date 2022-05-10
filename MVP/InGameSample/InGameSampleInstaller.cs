using UnityEngine;
using Zenject;

namespace MVPTemplateEditor.Game.Installer
{
	public class InGameSampleInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<InGameSample.InGameSampleModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.InGameSamplePresenter>().AsCached().NonLazy();
		}
	}
}