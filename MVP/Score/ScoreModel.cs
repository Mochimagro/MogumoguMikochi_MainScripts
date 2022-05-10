using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Score
{
	public interface IScoreModel
    {
		IntReactiveProperty Score { get; }
		void AddScore(int value);
		void ReduceScore(int value);
	}


	public class ScoreModel : IScoreModel
	{
		private string _saveKey = "MikochiScore";

		IntReactiveProperty _score = new IntReactiveProperty();
		public IntReactiveProperty Score => _score;
		public ScoreModel()
		{
			_score.Value = ES3.Load(_saveKey, 0);

			Score.Subscribe(value =>
			{
				ES3.Save(_saveKey, value);
			});

		}

		public void AddScore(int value)
        {
			_score.Value += value;
        }

		public void ReduceScore(int value)
        {
			_score.Value -= value;
        }

	}
}