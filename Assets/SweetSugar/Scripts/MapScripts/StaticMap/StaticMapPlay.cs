using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Localization;
using TMPro;
using UnityEngine;

namespace SweetSugar.Scripts.MapScripts
{
    public class StaticMapPlay : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private int level;

        private void OnEnable()
        {
            //level = LevelsMap._instance.GetLatestReachedLevel();
            //text.text = LocalizationManager.GetText(89, "Level") + " " + level;
            //print(level);
            
            
            int randomLevel = Random.Range(10, 20);
            level = randomLevel;
            text.text = LocalizationManager.GetText(89, "Level") + " " + level;
            print(level);
        }

        public void Play()
        {
            InitScript.OpenMenuPlay(level);
        }
    }
}