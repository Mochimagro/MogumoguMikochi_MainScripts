using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.PlayerSkill
{
	using Component;
	
	public interface IPlayerSkillView
    {
		void Init();

	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class PlayerSkillView : MonoBehaviour ,IPlayerSkillView
	{

		public void Init()
		{
			

		}
		
	}
}