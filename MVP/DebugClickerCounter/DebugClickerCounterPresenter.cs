using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
	using DebugClickerCounter;

	public interface IDebugClickerCounterPresenter
	{
		
	}

	public class DebugClickerCounterPresenter : IDebugClickerCounterPresenter
	{
		private IDebugClickerCounterView _debugClickerCounterView = null;
		private IDebugClickerCounterModel _debugClickerCounterModel = null;
		private IScorePresenter _scorePresenter = null;

		public DebugClickerCounterPresenter(
			IDebugClickerCounterView view,IDebugClickerCounterModel model,
			IScorePresenter score) 
		{
			_debugClickerCounterView = view ?? throw new ArgumentNullException(nameof(view));
			_debugClickerCounterModel = model ?? throw new ArgumentNullException(nameof(model));
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));

			_debugClickerCounterView.Init();
			Bind();
		}
		
		private void Bind () 
		{
			_debugClickerCounterView.OnAction.Subscribe(value =>
			{
				_scorePresenter.AddScore(value);
			});

		}
	}
}