using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
	using Score;

	public interface IScorePresenter
	{
		int Score { get; }
		void AddScore(int value);
		void ReduceScore(int value);
		IObservable<int> OnScore { get; }
	}

	public class ScorePresenter : IScorePresenter
	{
		private IScoreView _scoreView = null;
		private IScoreModel _scoreModel = null;

		public ScorePresenter(IScoreView view,IScoreModel model) 
		{
			_scoreView = view ?? throw new ArgumentNullException(nameof(view));
			_scoreModel = model ?? throw new ArgumentNullException(nameof(model));

			_scoreView.Init();
			Bind();
		}

        public IObservable<int> OnScore => _scoreModel.Score;

        public int Score { get { return _scoreModel.Score.Value; } }

        public void AddScore(int value)
        {
			_scoreModel.AddScore(value);
        }

		public void ReduceScore(int value)
        {
			_scoreModel.ReduceScore(value);
        }

        private void Bind () 
		{
			_scoreModel.Score.Subscribe(value =>
			{
				_scoreView.Score = value;
			});

			_scoreView.OnDebugAddButton.Subscribe(_ =>
			{
				_scoreModel.AddScore(100000);
			});

		}
	}
}