using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace MVPTemplateEditor.Game.InGameSample
{
	
	public interface IInGameSampleView
    {
		string SampleText { set; }
		IObservable<Unit> OnClickButton { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class InGameSampleView : MonoBehaviour ,IInGameSampleView
	{
		[SerializeField] private Text _sampleText = null;
		[SerializeField] private Button _sampleButton = null;

        public string SampleText { set => _sampleText.text = value; }

        public IObservable<Unit> OnClickButton => _sampleButton.OnClickAsObservable();

        public void Init()
		{
			

		}
		
	}
}