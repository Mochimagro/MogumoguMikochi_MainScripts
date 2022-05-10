using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MVPTemplateEditor.Game.Presenter
{
	using InGameSample;

	public interface IInGameSamplePresenter
	{
		
	}

	public class InGameSamplePresenter : IInGameSamplePresenter
	{
		private IInGameSampleView _inGameSampleView = null;
		private IInGameSampleModel _inGameSampleModel = null;

		public InGameSamplePresenter(IInGameSampleView view,IInGameSampleModel model) 
		{
			_inGameSampleView = view ?? throw new ArgumentNullException(nameof(view));
			_inGameSampleModel = model ?? throw new ArgumentNullException(nameof(model));
			
			Bind();
		}
		
		private void Bind () 
		{
			_inGameSampleView.OnClickButton.Subscribe(_ =>
			{
				_inGameSampleModel.AddValue(1);
			});

			_inGameSampleModel.CountValue.Subscribe(value =>
			{
				_inGameSampleView.SampleText = $"{value}";
			});

		}
	}
}