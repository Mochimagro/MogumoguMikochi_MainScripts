using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.SkillPowerUp
{
	public interface ISkillPowerUpModel
    {
		IReadOnlyList<Component.SkillPowerUpListItemEntity> EntityList { get; }
		void LevelUpSkill(Component.SkillPowerUpListItemEntity target);

    }


	public class SkillPowerUpModel : ISkillPowerUpModel
	{
		Data.ISkillData _skillData;
        public IReadOnlyList<Component.SkillPowerUpListItemEntity> EntityList => _entityList;
		List<Component.SkillPowerUpListItemEntity> _entityList = new List<Component.SkillPowerUpListItemEntity>();

		public SkillPowerUpModel(Data.ISkillData skillData)
		{
			_skillData = skillData;

			foreach (var data in _skillData.SkillPlayerDatas)
            {
				var entity = new Component.SkillPowerUpListItemEntity(
					data.SkillKey,
					data.Icon,
					data.SkillName,
					data.SkillInfo,
					ES3.Load($"{data.SkillKey}", 0),
					data.NeedNextLEvelsPoint);

				_entityList.Add(entity);
            }

		}

        public void LevelUpSkill(Component.SkillPowerUpListItemEntity target)
        {

			ES3.Save($"{target.SkillDataKey}",target.LevelUp());


        }

    }
}