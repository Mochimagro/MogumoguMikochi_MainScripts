using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using MikochiClicker.Game.Component;

namespace MikochiClicker.Game.PlayerSkill
{
	public interface IPlayerSkillModel
    {
		void UpdateSkill();
		void AllDeleteData();
		Component.PlayerSkillParameter Parameter { get; }
	}


	public class PlayerSkillModel : IPlayerSkillModel
	{
		private Data.ISkillData _skillData;
        public PlayerSkillParameter Parameter => _parameter;
		private Component.PlayerSkillParameter _parameter = null;


		public PlayerSkillModel(Data.ISkillData data)
		{
			_skillData = data;

			_parameter = new PlayerSkillParameter();

			UpdateSkill();
		}

        public void UpdateSkill()
        {
			_parameter.BurningLevel = ES3.Load("SKILL_BURNING", 0);
			_parameter.NyeUpperLevel = ES3.Load("SKILL_NYE_UPPER", 0);
        }

        public void AllDeleteData()
        {
			ES3.DeleteFile();
        }
    }
}

namespace MikochiClicker.Game.Component
{
	public class PlayerSkillParameter
    {

		private int _burningLevel = 0;
		public int BurningLevel { get => _burningLevel;set => _burningLevel = value; }

		private int _nyeUpperLevel = 0;
		public int NyeUpperLevel { get => _nyeUpperLevel; set => _nyeUpperLevel = value; }
	}
}