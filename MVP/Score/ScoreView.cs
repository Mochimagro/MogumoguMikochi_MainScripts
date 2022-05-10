using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.Score
{
	using Component;
	
	public interface IScoreView
    {
		void Init();
		int Score { set; }
		IObservable<Unit> OnDebugAddButton { get; }

	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class ScoreView : MonoBehaviour ,IScoreView
	{
		[SerializeField] private TextMeshProUGUI _scoreText = null;
		[SerializeField] private Button _debugAddButton = null;

        public int Score { set { _scoreText.text = $"{value}にぇ"; } }

		public IObservable<Unit> OnDebugAddButton => _debugAddButton.OnClickAsObservable();

        public void Init()
		{
			

		}
		
	}
}