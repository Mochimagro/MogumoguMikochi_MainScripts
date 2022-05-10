using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using System.Runtime.InteropServices;

namespace MikochiClicker.Game.Setting
{
	public interface ISettingModel
    {
		void AllDeleteData();
		void ReloadPage();
        void OpenFanbox();
        void OpenTweetPage(int score);
    }


	public class SettingModel : ISettingModel
	{
        [DllImport("__Internal")] static extern bool reload();
        [DllImport("__Internal")] private static extern string TweetFromUnity(string rawMessage);
        [DllImport("__Internal")] private static extern string openURL(string rawURL);
        public SettingModel()
		{
			

		}

        public void AllDeleteData()
        {
            ES3.DeleteFile();
        }

        public void OpenFanbox()
        {
            openURL("https://mochimagro.fanbox.cc/");
        }

        public void ReloadPage()
        {
#if UNITY_EDITOR
            Application.Quit();
            UnityEditor.EditorApplication.ExitPlaymode();
            return;
#endif

#if UNITY_WEBGL
            reload();
            // Application.OpenURL(Application.absoluteURL);
#endif
        }

        public void OpenTweetPage(int score)
        {
            var message = $"みこちにたい焼きをもぐちさせよう！%0a今のみこち満足度は{score}にぇ。%0a" +
                $"ゲームプレイはこちら↓%0a"
                + "%0a" + "https://mochimagro.github.io/MicochiClicker/"
                + "%0a%23" + "さくらみこ" + "%0a%23" + "もぐもぐみこち";

            TweetFromUnity(message);
        }
    }
}