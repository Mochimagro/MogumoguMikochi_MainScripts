using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AI;

namespace MikochiClicker.Game.Component
{
	public interface IMikochiComponent
    {
		void Init();
		ITaiyakiComponent TargetTaiyaki { set; }
		IObservable<ITaiyakiComponent> OnCompleteEat { get; }
		IObservable<Unit> OnInvokeIdle { get; }
    }

	public enum DebugMikochiState
    {
		Idle,
		Move,
		Eat,
    }

	public class MikochiComponent : MonoBehaviour ,IMikochiComponent
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private Transform _grabTaiyakiPivot;

		[SerializeField] private ParticleSystem _FXEating;
		[SerializeField] private ParticleSystem _FXNye;

		// [SerializeField] private DebugMikochiState _debugCurrentState = default;

		MikochiState _currentState;

		MikochiState ChangeState
        {
            set
			{
				// Debug.Log($"StateInvoke : {value._StateName}");
				// _debugCurrentState = value._StateName;
				_currentState?.Complete();
				_currentState = value;
				_currentState.Invoke();
            }
        }

		MikochiState _idle, _moving, _eating;

		ReactiveProperty<ITaiyakiComponent> _targetTaiyaki = null;

        public IObservable<ITaiyakiComponent> OnCompleteEat => _onCompleteEat;
		private Subject<ITaiyakiComponent> _onCompleteEat = new Subject<ITaiyakiComponent>();


		public IObservable<Unit> OnInvokeIdle => _onInvokeIdle;
		private Subject<Unit> _onInvokeIdle = new Subject<Unit>();

		public void Init () 
		{
			InitStates();

			ChangeState = _idle;
		}

        private void LateUpdate()
        {

			_currentState.Update();
		}

        private void InitStates()
		{
			_idle = new MikochiState();
			_moving = new MikochiState();
			_eating = new MikochiState();

			_idle._StateName = DebugMikochiState.Idle;
			_moving._StateName = DebugMikochiState.Move;
			_eating._StateName = DebugMikochiState.Eat;

			_idle.Init();
			_moving.Init();
			_eating.Init();

			_targetTaiyaki = new ReactiveProperty<ITaiyakiComponent>();

			_targetTaiyaki.Subscribe(value =>
			{

				if(value == null)
                {
					return;
                }

				var targetDestination = value.GameObject.transform.position;

				targetDestination.y = 0;

				_navMeshAgent.destination = targetDestination;

				Debug.Log($"RemainingDistance : {_navMeshAgent.remainingDistance}");

				ChangeState = _moving;

			});

			_idle.OnInvokeState.Subscribe(_ =>
			{
				_onInvokeIdle.OnNext(Unit.Default);
			});



			_moving.OnInvokeState.Subscribe(_ =>
			{
				_navMeshAgent.isStopped = false;
			});

			_moving.OnUpdate.Subscribe(_ =>
			{
				_animator.SetFloat("Move", _navMeshAgent.speed);

				if(_navMeshAgent.remainingDistance == 0)
                {
					return;
                }

                if (_navMeshAgent.remainingDistance <= 0.75f)
                {
					_navMeshAgent.isStopped = true;
					ChangeState = _eating;
                }
			});

			_moving.OnCompleted.Subscribe(_ =>
			{
				_animator.SetFloat("Move", 0);
			});



			_eating.OnInvokeState.Subscribe(_ =>
			{
				var taiyakiTarnsform = _targetTaiyaki.Value.GameObject.transform;

				taiyakiTarnsform.SetParent(_grabTaiyakiPivot, false);
				taiyakiTarnsform.localPosition = Vector3.zero;
				taiyakiTarnsform.localRotation = default;

				_animator.SetTrigger("EatInvoke");

				_FXEating.Play();

			});

			_eating.OnUpdate.Subscribe(_ =>
			{
			});

			_eating.OnCompleted.Subscribe(_ =>
			{
				_FXEating.Stop();
				_FXNye.Play();

				var eated = _targetTaiyaki.Value;
				_targetTaiyaki.Value = null;
				_onCompleteEat.OnNext(eated);
			});

		}

		public void OnEatAnimationComplete()
        {
			ChangeState = _idle;
		}

		public void OnHideEatTarget()
        {
			_targetTaiyaki.Value.HideTaiyakiMesh();
        }

		public ITaiyakiComponent TargetTaiyaki
		{
			set
			{
				if(_targetTaiyaki.Value != null)
                {
					return;
                }

				_targetTaiyaki.Value = value;
			}
        }

    }

	public class MikochiState
    {
		public DebugMikochiState _StateName = default;

		public IObservable<Unit> OnInvokeState => _onInvokeState;
		private Subject<Unit> _onInvokeState = new Subject<Unit>();

		public IObservable<Unit> OnCompleted => _onComplete;
		private Subject<Unit> _onComplete = new Subject<Unit>();

		public IObservable<Unit> OnUpdate => _updateAction;
		private Subject<Unit> _updateAction = new Subject<Unit>();

		public void Init()
        {

        }

		public void Update()
        {
			_updateAction.OnNext(Unit.Default);
        }

		public void Invoke()
        {
			_onInvokeState.OnNext(Unit.Default);
        }

		public void Complete()
        {
			_onComplete.OnNext(Unit.Default);
        }
		
    }

}