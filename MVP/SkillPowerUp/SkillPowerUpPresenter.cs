using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
	using SkillPowerUp;

	public interface ISkillPowerUpPresenter
	{
		IObservable<string> OnLevelUpSkill { get; }
	}

	public class SkillPowerUpPresenter : ISkillPowerUpPresenter
	{
		private ISkillPowerUpView _skillPowerUpView = null;
		private ISkillPowerUpModel _skillPowerUpModel = null;
		private IScorePresenter _scorePresenter = null;

		public SkillPowerUpPresenter(
			ISkillPowerUpView view,ISkillPowerUpModel model,
			IScorePresenter score) 
		{
			_skillPowerUpView = view ?? throw new ArgumentNullException(nameof(view));
			_skillPowerUpModel = model ?? throw new ArgumentNullException(nameof(model));
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));

			_skillPowerUpView.Init(_skillPowerUpModel.EntityList);
			Bind();
		}

        public IObservable<string> OnLevelUpSkill => _onLevelUpSkill;
		private Subject<string> _onLevelUpSkill = new Subject<string>();

        private void Bind () 
		{
			_scorePresenter.OnScore.Subscribe(value =>
			{
				_skillPowerUpView.UpdatePoint(value);
			});

			_skillPowerUpView.OnSelectLevelUp.Subscribe(entity =>
			{
				var value = entity.NeedNextPoint;
				_skillPowerUpModel.LevelUpSkill(entity);
				_skillPowerUpView.UpdateListItem(entity);
				_scorePresenter.ReduceScore(value);

				_onLevelUpSkill.OnNext(entity.SkillDataKey);
			});

		}
	}
}