using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MikochiClicker.Game.Setting
{
	using Component;
	
	public interface ISettingView
    {
		void Init();
		IObservable<Unit> OnDeleteDataButton { get; }
		IObservable<Unit> OnFanboxButton { get; }
		IObservable<Unit> OnTweetPage { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class SettingView : MonoBehaviour ,ISettingView
	{

		[SerializeField] private Button _deleteDataButton = null;
		[SerializeField] private Button _fanboxButton = null;
		[SerializeField] private Button _tweetButton = null;

        public IObservable<Unit> OnDeleteDataButton => _deleteDataButton.OnClickAsObservable();

        public IObservable<Unit> OnFanboxButton => _fanboxButton.OnClickAsObservable();

        public IObservable<Unit> OnTweetPage => _tweetButton.OnClickAsObservable();

        public void Init()
		{
			

		}
		
	}
}