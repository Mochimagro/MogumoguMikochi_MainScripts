using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace MikochiClicker.Game.TakoyakiMakers
{
	public interface ITakoyakiMakersModel
    {
		Component.TaiyakiEntity TaiyakiEntity { get; set; }

		int TaiyakiScore { set; }
	}


	public class TakoyakiMakersModel : ITakoyakiMakersModel
	{
		public Component.TaiyakiEntity TaiyakiEntity { get => _taiyakiEntity; set => _taiyakiEntity = value; }
        public int TaiyakiScore { set => _taiyakiEntity.Score = value; }

        Component.TaiyakiEntity _taiyakiEntity = null;

		public TakoyakiMakersModel()
		{
			_taiyakiEntity = new Component.TaiyakiEntity(1);

		}

		

	}
}