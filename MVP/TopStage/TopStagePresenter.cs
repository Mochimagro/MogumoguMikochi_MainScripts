using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{

    using TopStage;

	public interface ITopStagePresenter
	{
		void AddCreatedTaiyaki(Component.ITaiyakiComponent targetTaiyaki);
	}

	public class TopStagePresenter : ITopStagePresenter
	{
		private ITopStageView _topStageView = null;
		private ITopStageModel _topStageModel = null;
		private IScorePresenter _scorePresenter = null;

		public TopStagePresenter(
			ITopStageView view,ITopStageModel model,
			IScorePresenter score) 
		{
			_topStageView = view ?? throw new ArgumentNullException(nameof(view));
			_topStageModel = model ?? throw new ArgumentNullException(nameof(model));
			
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));

			_topStageView.Init();
			Bind();
		}


		public void AddCreatedTaiyaki(Component.ITaiyakiComponent targetTaiyaki)
        {
			_topStageView.SetPositionTaiyaki(targetTaiyaki);
			// _topStageModel.AddTaiyaki(targetTaiyaki);
		}

		private void Bind () 
		{
			_topStageView.OnEatComplete.Subscribe(value =>
			{
				_scorePresenter.AddScore(value.Entity.Score);
				_topStageView.DestroyTaiyakiObject(value);

			});

			_topStageView.OnIdleInvoke.Subscribe(_ =>
			{

				if (_topStageModel.StockTaiyaki != null)
				{
					_topStageView.TargetTaiyaki = _topStageModel.StockTaiyaki;
				}
			});

			_topStageView.OnCompletePutOnStage.Subscribe(value =>
			{
				_topStageModel.AddTaiyaki(value);
			});

			_topStageModel.OnAddTaiyaki.Subscribe(target =>
			{

				_topStageView.TargetTaiyaki = target;
			});

		}
	}
}