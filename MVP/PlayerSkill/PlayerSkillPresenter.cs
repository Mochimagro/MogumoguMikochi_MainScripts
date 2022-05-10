using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
    using MikochiClicker.Game.Component;
    using PlayerSkill;

	public interface IPlayerSkillPresenter
	{
		Component.PlayerSkillParameter Parameter { get; }
	}

	public class PlayerSkillPresenter : IPlayerSkillPresenter
	{
		private IPlayerSkillView _playerSkillView = null;
		private IPlayerSkillModel _playerSkillModel = null;
		private ISkillPowerUpPresenter _skillPowerUpPresenter = null;

		public PlayerSkillPresenter(IPlayerSkillView view,IPlayerSkillModel model,ISkillPowerUpPresenter skillPowerUp) 
		{
			_playerSkillView = view ?? throw new ArgumentNullException(nameof(view));
			_playerSkillModel = model ?? throw new ArgumentNullException(nameof(model));
			_skillPowerUpPresenter= skillPowerUp ?? throw new ArgumentNullException(nameof(skillPowerUp));

			_playerSkillView.Init();
			Bind();
		}

        public PlayerSkillParameter Parameter => _playerSkillModel.Parameter;

        private void Bind () 
		{
			_skillPowerUpPresenter.OnLevelUpSkill.Subscribe(key =>
			{
				_playerSkillModel.UpdateSkill();
			});

		}



	}
}