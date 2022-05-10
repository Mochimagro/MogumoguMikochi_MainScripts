using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MikochiClicker.Game.Component
{
	public interface ITaiyakiComponent
    {
		void Init(TaiyakiEntity entity);
		IObservable<ITaiyakiComponent> OnClickTarget { get; }
		IObservable<ITaiyakiComponent> OnDestroyObject { get; }
		TaiyakiEntity Entity { get; }
		GameObject GameObject { get; }
		void HideTaiyakiMesh();
		void Complete();
		void DestroyObject();
	}

	public class TaiyakiEntity
    {
		public TaiyakiEntity(int score )
        {
			_score = score;
        }

		public TaiyakiEntity (TaiyakiEntity entity) : this(entity.Score)
        {
        }

		public int Score { get => _score; set => _score = value; }

		private int _score;


    }

	public class TaiyakiComponent : MonoBehaviour ,ITaiyakiComponent
	{
		TaiyakiEntity _entity = null;

		[Header("Component")]
		[SerializeField] private MeshRenderer _renderer;

		[Header("Project")]
		[SerializeField] private Material _makingMaterial;
		[SerializeField] private Material _makedMaterial;

		public IObservable<ITaiyakiComponent> OnClickTarget => _onClickTarget;
		private Subject<ITaiyakiComponent> _onClickTarget = new Subject<ITaiyakiComponent>();

		public IObservable<ITaiyakiComponent> OnDestroyObject => _onDestroy;
		private Subject<ITaiyakiComponent> _onDestroy = new Subject<ITaiyakiComponent>();


        public TaiyakiEntity Entity => _entity;

		public GameObject GameObject => gameObject;

        public void Init (TaiyakiEntity entity) 
		{
			_entity = entity;

			_renderer.material = _makingMaterial;

			gameObject.SetActive(true);

		}

        public void Complete()
        {
			_renderer.material = _makedMaterial;

			var eventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

			eventTrigger.OnPointerClickAsObservable().Subscribe(eventData =>
			{
				_onClickTarget.OnNext(this);
			});

		}

        public void DestroyObject()
        {
			_onDestroy.OnNext(this);
			Destroy(this.GameObject);
        }

        public void HideTaiyakiMesh()
        {
			_renderer.enabled = false;
        }
    }
}