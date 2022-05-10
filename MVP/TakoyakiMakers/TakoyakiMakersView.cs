using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.TakoyakiMakers
{
	using Component;
	
	public interface ITakoyakiMakersView
    {
		void Init(TaiyakiEntity entity, PlayerSkillParameter parameter);
		IObservable<ITaiyakiComponent> OnAddTaiyaki { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class TakoyakiMakersView : MonoBehaviour ,ITakoyakiMakersView
	{
		[Inject(Id = "ITaiyakiMakerComponent")] private List<ITaiyakiMakerComponent> _taiyakiMakerComponents = null;

        public IObservable<ITaiyakiComponent> OnAddTaiyaki => _onAddTaiyaki;
		private Subject<ITaiyakiComponent> _onAddTaiyaki = new Subject<ITaiyakiComponent>();

        public void Init(TaiyakiEntity entity,PlayerSkillParameter parameter)
		{
			foreach(var component in _taiyakiMakerComponents)
            {
				component.Init(entity,parameter);

				component.OnClickTaiyaki.Subscribe(value =>
				{
					_onAddTaiyaki.OnNext(value);
				});

            }

		}
		
	}
}