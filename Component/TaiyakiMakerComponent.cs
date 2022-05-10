using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MikochiClicker.Game.Component
{
	public interface ITaiyakiMakerComponent
    {
		void Init(TaiyakiEntity taiyakiEntity, PlayerSkillParameter parameter);
		void StartMake(float duration);
        IObservable<ITaiyakiComponent> OnClickTaiyaki { get; }
    }


	public class TaiyakiMakerComponent : MonoBehaviour ,ITaiyakiMakerComponent
	{
        [SerializeField] private TaiyakiComponent _myTaiyakiComponent = null;
        [SerializeField] private Animator _animator = null;
        [SerializeField] private ParticleSystem _FXSmoke = null;
        [SerializeField] private ParticleSystem _FXPick = null;

        private TaiyakiEntity _createTaiyaki;
        private PlayerSkillParameter _skillParameter;

        private MakerStatus _currentStatus;
        private MakerStatus _ready, _making, _completed;

        public IObservable<ITaiyakiComponent> OnClickTaiyaki => _onClickCompletedTaiyaki;
        private Subject<ITaiyakiComponent> _onClickCompletedTaiyaki = new Subject<ITaiyakiComponent>();

        public void Init (TaiyakiEntity taiyakiEntity,PlayerSkillParameter parameter) 
		{

            _createTaiyaki = taiyakiEntity;
            _skillParameter = parameter;

            _ready = new MakerStatus();
            _making = new MakerStatus();
            _completed = new MakerStatus();

            var eventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

            eventTrigger.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _currentStatus.Action();
            });

            _ready.OnClickAction.Subscribe(_ =>
            {
                StartMake(15 - _skillParameter.BurningLevel);

            });

            _making.OnClickAction.Subscribe(_ => 
            {

            });

            _completed.OnClickAction.Subscribe(_ =>
            {
                // _currentStatus = _ready;

            });

            _currentStatus = _ready;
		}


        private ITaiyakiComponent _makeTargetTaiyaki = null;
        public void StartMake(float duration)
        {
            _currentStatus = _making;

            _animator.SetBool("Making", true);

            _makeTargetTaiyaki = Instantiate(_myTaiyakiComponent, this.transform, false);

            _makeTargetTaiyaki.Init(new TaiyakiEntity(_createTaiyaki));

            Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe(_ =>
            {

                _animator.SetBool("Making", false);

                _makeTargetTaiyaki.Complete();

                _FXSmoke.Play();

                _makeTargetTaiyaki.OnClickTarget.Subscribe(createdTaiyaki =>
                {
                    _FXPick.Play();

                    _onClickCompletedTaiyaki.OnNext(createdTaiyaki);

                    _currentStatus = _ready;
                });

                _currentStatus = _completed;
            });
        }

        public class MakerStatus
        {
            private Subject<Unit> _onClickAction = new Subject<Unit>();
            public IObservable<Unit> OnClickAction => _onClickAction;

            public void Action()
            {
                _onClickAction.OnNext(Unit.Default);
            }

        }
    }
}