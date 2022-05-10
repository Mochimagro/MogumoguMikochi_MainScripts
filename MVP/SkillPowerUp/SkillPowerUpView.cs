using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.SkillPowerUp
{
	using Component;
	
	public interface ISkillPowerUpView
    {
		void Init(IReadOnlyList<SkillPowerUpListItemEntity> entities);
		void UpdatePoint(int value);
		IObservable<SkillPowerUpListItemEntity> OnSelectLevelUp { get; }
		void UpdateListItem(SkillPowerUpListItemEntity target);
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class SkillPowerUpView : MonoBehaviour ,ISkillPowerUpView
	{
		[SerializeField] private TextMeshProUGUI _pointText = null;
		[SerializeField] private Transform _listParent = null;
		//[Inject(Id = "ISkillPowerUpListItemComponent")] private ISkillPowerUpListItemComponent _skillPowerUpListItemComponent = null;
		[SerializeField] private SkillPowerUpListItemComponent _skillPowerUpListItemComponent = null;
		private Dictionary<string,ISkillPowerUpListItemComponent> _skillPowerUpListItemComponents = new Dictionary<string, ISkillPowerUpListItemComponent>();

        public IObservable<SkillPowerUpListItemEntity> OnSelectLevelUp => _onSelectLevelUp;
		private Subject<SkillPowerUpListItemEntity> _onSelectLevelUp = new Subject<SkillPowerUpListItemEntity>();

        public void Init(IReadOnlyList<SkillPowerUpListItemEntity> entities)
		{
			foreach(var entity in entities)
            {
				var item = Instantiate(_skillPowerUpListItemComponent, _listParent, false);
				item.Init(entity);
				item.OnSelectSkillLevelUp.Subscribe(entity =>
				{
					_onSelectLevelUp.OnNext(entity);
				});
				_skillPowerUpListItemComponents.Add(entity.SkillDataKey,item);
			}

		}

        public void UpdatePoint(int value)
        {
			_pointText.text = $"{value}にぇ";
			foreach(var item in _skillPowerUpListItemComponents)
            {
				item.Value.UpdatePoint(value);
            }
        }

		public void UpdateListItem(SkillPowerUpListItemEntity target)
        {
			_skillPowerUpListItemComponents[target.SkillDataKey].UIUpdate(target);
        }

    }
}