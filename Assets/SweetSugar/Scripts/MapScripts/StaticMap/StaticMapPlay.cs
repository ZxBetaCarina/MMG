using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Localization;
using TMPro;
using UnityEngine;

namespace SweetSugar.Scripts.MapScripts
{
    public class StaticMapPlay : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private int level=10;

        private void OnEnable()
        {
            // Generate a random level between 10 and 23, excluding 20
            int randomLevel;
            do
            {
                randomLevel = Random.Range(10, 23); // Upper bound is exclusive
            } while (randomLevel == 20);

            //level = randomLevel;
            text.text = LocalizationManager.GetText(89, "Level") + " " + level;
            Debug.Log(level); // Use Debug.Log for consistency in Unity
        }

        public void Play()
        {
            InitScript.OpenMenuPlay(level);
        }
    }
}