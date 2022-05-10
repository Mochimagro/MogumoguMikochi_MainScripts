using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.Presenter
{
	using TakoyakiMakers;

	public interface ITakoyakiMakersPresenter
	{
		
	}

	public class TakoyakiMakersPresenter : ITakoyakiMakersPresenter
	{
		private ITakoyakiMakersView _takoyakiMakersView = null;
		private ITakoyakiMakersModel _takoyakiMakersModel = null;
		private ITopStagePresenter _topStagePresenter = null;
		private IPlayerSkillPresenter _playerSkillPresenter = null;

		public TakoyakiMakersPresenter(
			ITakoyakiMakersView view,ITakoyakiMakersModel model,
			ITopStagePresenter topStage, IPlayerSkillPresenter playerSkill) 
		{
			_takoyakiMakersView = view ?? throw new ArgumentNullException(nameof(view));
			_takoyakiMakersModel = model ?? throw new ArgumentNullException(nameof(model));
			_topStagePresenter = topStage ?? throw new ArgumentNullException(nameof(topStage));
			_playerSkillPresenter = playerSkill ?? throw new ArgumentNullException(nameof(playerSkill));

			_takoyakiMakersView.Init(_takoyakiMakersModel.TaiyakiEntity,_playerSkillPresenter.Parameter);
			Bind();
		}
		
		private void Bind () 
		{
			_takoyakiMakersView.OnAddTaiyaki.Subscribe(value =>
			{
				value.Entity.Score = _playerSkillPresenter.Parameter.NyeUpperLevel + 1;
				_topStagePresenter.AddCreatedTaiyaki(value);
			});

		}
	}
}