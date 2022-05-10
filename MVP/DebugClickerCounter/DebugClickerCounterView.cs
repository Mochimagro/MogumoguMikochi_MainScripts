using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.DebugClickerCounter
{
	using Component;
	
	public interface IDebugClickerCounterView
    {
		void Init();
		IObservable<int> OnAction { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class DebugClickerCounterView : MonoBehaviour ,IDebugClickerCounterView
	{
		[Inject(Id = "IDebugClickerComponent")] private IDebugClickerComponent _debugClickerComponent = null;

        public IObservable<int> OnAction => _onAction;
		Subject<int> _onAction = new Subject<int>();

        public void Init()
		{
			_debugClickerComponent.Init();

			_debugClickerComponent.OnClickTarget.Subscribe(value =>
			{
				_onAction.OnNext(value);
			});

		}
		
	}
}