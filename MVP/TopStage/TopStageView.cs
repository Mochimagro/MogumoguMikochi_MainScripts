using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace MikochiClicker.Game.TopStage
{
	using Game.Component;
	
	public interface ITopStageView
    {
		void Init();
		IObservable <ITaiyakiComponent> OnCompletePutOnStage { get; }
		ITaiyakiComponent TargetTaiyaki { set; }
		IObservable<ITaiyakiComponent> OnEatComplete { get; }
		IObservable<Unit> OnIdleInvoke { get; }
		void DestroyTaiyakiObject(ITaiyakiComponent eatedTaiyaki);
		void SetPositionTaiyaki(ITaiyakiComponent value);
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class TopStageView : MonoBehaviour ,ITopStageView
	{
		[Inject(Id = "IMikochiComponent")] private IMikochiComponent _mikochiComponent = null;
		[Inject(Id = "TopStageTaiyakiParent")] private Transform _topStageTaiyakiParent = null;

        public IObservable<ITaiyakiComponent> OnCompletePutOnStage => _onCompletePutOnStage;
		private Subject<ITaiyakiComponent> _onCompletePutOnStage = new Subject<ITaiyakiComponent>();

        public IObservable<ITaiyakiComponent> OnEatComplete => _mikochiComponent.OnCompleteEat;

        public IObservable<Unit> OnIdleInvoke => _mikochiComponent.OnInvokeIdle;

        public void Init()
		{
			_mikochiComponent.Init();

		}

		public void SetPositionTaiyaki(ITaiyakiComponent value)
        {
			value.GameObject.transform.SetParent(_topStageTaiyakiParent, false);
			value.GameObject.transform.position = new Vector3(UnityEngine.Random.Range(4f, -3.5f), 10f, UnityEngine.Random.Range(5, -1.5f));
			value.GameObject.transform.rotation = Quaternion.Euler(0,0f,0);
			value.GameObject.transform.DOMoveY(1.5f, 1.35f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
				_onCompletePutOnStage.OnNext(value);
            });

		}

		public ITaiyakiComponent TargetTaiyaki
		{
			set
			{
				_mikochiComponent.TargetTaiyaki = value;
			}
		}

        public void DestroyTaiyakiObject(ITaiyakiComponent eatedTaiyaki)
        {
			eatedTaiyaki.DestroyObject();
        }
    }
}