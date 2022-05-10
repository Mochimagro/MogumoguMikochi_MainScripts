using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
	using Setting;

	public interface ISettingPresenter
	{
		
	}

	public class SettingPresenter : ISettingPresenter
	{
		private ISettingView _settingView = null;
		private ISettingModel _settingModel = null;
		private IScorePresenter _scorePresenter = null;

		public SettingPresenter(ISettingView view,ISettingModel model,IScorePresenter score) 
		{
			_settingView = view ?? throw new ArgumentNullException(nameof(view));
			_settingModel = model ?? throw new ArgumentNullException(nameof(model));
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));

			_settingView.Init();
			Bind();
		}
		
		private void Bind () 
		{
			_settingView.OnDeleteDataButton.Subscribe(_ =>
			{
				_settingModel.AllDeleteData();
				_settingModel.ReloadPage();
			});

			_settingView.OnFanboxButton.Subscribe(_ =>
			{
				_settingModel.OpenFanbox();
			});

			_settingView.OnTweetPage.Subscribe(_ =>
			{
				_settingModel.OpenTweetPage(_scorePresenter.Score);
			});
		}
	}
}