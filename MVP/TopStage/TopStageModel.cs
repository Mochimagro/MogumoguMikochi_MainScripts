using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.TopStage
{
	public interface ITopStageModel
    {
		IObservable<Component.ITaiyakiComponent> OnAddTaiyaki { get; }
		void AddTaiyaki(Component.ITaiyakiComponent newTaiyaki);
		Component.ITaiyakiComponent StockTaiyaki { get; }
	}


	public class TopStageModel : ITopStageModel
	{
		private ReactiveCollection<Component.ITaiyakiComponent> _createdTaiyakis  = null;


		public TopStageModel()
		{
			_createdTaiyakis = new ReactiveCollection<Component.ITaiyakiComponent>();

			_createdTaiyakis.ObserveAdd().Subscribe(value =>
		   {
			   _onAddTaiyaki.OnNext(value.Value);
		   });

		}

        public IObservable<Component.ITaiyakiComponent> OnAddTaiyaki => _onAddTaiyaki;

        public Component.ITaiyakiComponent StockTaiyaki
        {
            get
            {
				return _createdTaiyakis.Count == 0 ? null : _createdTaiyakis[0];
            }
        }

        private Subject<Component.ITaiyakiComponent> _onAddTaiyaki = new Subject<Component.ITaiyakiComponent>();

        public void AddTaiyaki(Component.ITaiyakiComponent newTaiyaki)
        {
			newTaiyaki.OnDestroyObject.Subscribe(value =>
			{
				_createdTaiyakis.Remove(value);
			});

			_createdTaiyakis.Add(newTaiyaki);
        }
	}
}