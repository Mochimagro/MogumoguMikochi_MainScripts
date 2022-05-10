using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MikochiClicker.Game.Component
{
	public interface IDebugClickerComponent
    {
		void Init();
		IObservable<int> OnClickTarget { get; }
    }


	public class DebugClickerComponent : MonoBehaviour ,IDebugClickerComponent
	{
		[SerializeField] private List<GameObject> _clickerTarget = null;
		private Subject<int> _onClickTarget = new Subject<int>();

        public IObservable<int> OnClickTarget => _onClickTarget;

        public void Init () 
		{
			foreach(GameObject target in _clickerTarget)
            {
				var eventTrigger = target.AddComponent<ObservableEventTrigger>();

				eventTrigger.OnPointerClickAsObservable().Subscribe(eventData =>
				{
					_onClickTarget.OnNext(1);
				});
            }

		}
		
	}
}