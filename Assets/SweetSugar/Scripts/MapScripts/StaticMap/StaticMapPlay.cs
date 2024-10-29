using System;
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
            level = LevelsMap._instance.GetLatestReachedLevel();
            text.text = LocalizationManager.GetText(89, "Level") + " " + level;
        }

        public void Start()
        {
            InitScript.OpenMenuPlay(level);
        }
    }
}