using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;

namespace MikochiClicker.Game.Component
{
	public interface ISkillPowerUpListItemComponent
    {
		void Init(SkillPowerUpListItemEntity entity);
		void UpdatePoint(int point);
		void UIUpdate(SkillPowerUpListItemEntity entity);
		IObservable<SkillPowerUpListItemEntity> OnSelectSkillLevelUp { get; }
    }


	public class SkillPowerUpListItemComponent : MonoBehaviour ,ISkillPowerUpListItemComponent
	{
		SkillPowerUpListItemEntity _entity = null;

		[SerializeField] private Button _button = null;
		[SerializeField] private Image _iconImage = null;
		[SerializeField] private TextMeshProUGUI _skillNameText = null;
		[SerializeField] private TextMeshProUGUI _skillInfoText = null;
		[SerializeField] private TextMeshProUGUI _skillLevelText = null;
		[SerializeField] private TextMeshProUGUI _needLevelUpPointText = null;
        public IObservable<SkillPowerUpListItemEntity> OnSelectSkillLevelUp => _onSelectSkilLevelUp;
		private Subject<SkillPowerUpListItemEntity> _onSelectSkilLevelUp = new Subject<SkillPowerUpListItemEntity>();

        public void Init (SkillPowerUpListItemEntity entity) 
		{
			UIUpdate(entity);

			_button.OnClickAsObservable().Subscribe(_ =>
			{
				_onSelectSkilLevelUp.OnNext(_entity);
			});
		}

        public void UpdatePoint(int point)
        {
			_button.interactable = point >= _entity.NeedNextPoint && _entity.NeedNextPoint != -1;
        }

        public void UIUpdate(SkillPowerUpListItemEntity entity)
        {
			_entity = entity;
			_iconImage.sprite = _entity.Icon;
			_skillNameText.text = _entity.Name;
			_skillInfoText.text = _entity.Info;
			_needLevelUpPointText.text = _entity.NeedNextPoint != -1 ? $"{_entity.NeedNextPoint}にぇ": $"";
			_skillLevelText.text = _entity.NeedNextPoint != -1 ? $"レベル{_entity.SkillLevel}" : $"まっくすれべる";
		}
    }

	public class SkillPowerUpListItemEntity
    {
		public Sprite Icon => _iconSprite;
		public string Name => _skillName;
		public string Info => _skillInfo;
		public int	SkillLevel => _skillLevel;
		public int NeedNextPoint { get 
			{
                try
                {
					var target = _needNextLevel[_skillLevel];
					return target;
                }
                catch (ArgumentOutOfRangeException)
                {
					return -1;
                }
			}
		}
		public string SkillDataKey => _skillDataKey;

		private string _skillDataKey;
		private Sprite _iconSprite;
		private string _skillName;
		private string _skillInfo;
		private int _skillLevel;
		private List<int> _needNextLevel;

		public SkillPowerUpListItemEntity(string skillDataKey,Sprite icon,string name,string info,int skillLevel,List<int> needNextsPoint)
        {
			_skillDataKey = skillDataKey;
			_iconSprite = icon;
			_skillName = name;
			_skillInfo = info;
			_skillLevel = skillLevel;
			_needNextLevel = needNextsPoint;
        }

		public int LevelUp()
        {
			_skillLevel++;
			return _skillLevel;
        }
    }
}