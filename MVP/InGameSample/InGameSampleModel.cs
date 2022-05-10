using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MVPTemplateEditor.Game.InGameSample
{
	using Data;
	public interface IInGameSampleModel
    {
		void AddValue(int value);
		IObservable<int> CountValue { get; }
    }


	public class InGameSampleModel : IInGameSampleModel
	{
		IntReactiveProperty _countValue = null;
        public IObservable<int> CountValue => _countValue;
		
		public InGameSampleModel(IInGameSampleData data)
		{
			_countValue = new IntReactiveProperty(data.DefaultValue);

		}

        public void AddValue(int value)
        {
			_countValue.Value += value;
        }
    }
}