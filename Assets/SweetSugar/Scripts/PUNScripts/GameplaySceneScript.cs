using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SweetSugar.Scripts.PUNScripts
{
    public class GameplaySceneScript : MonoBehaviour
    {
        public static GameplaySceneScript Instance;

        public bool gameEnded = false, opponentEnded = false;



        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gameEnded = false;
            opponentEnded = false;
        }       

        public void TestingGameState()
        {
            if (Core.LevelManager.THIS.gameStatus == Core.GameState.Playing)
                Core.LevelManager.THIS.gameStatus = Core.GameState.Pause;
            else
                Core.LevelManager.THIS.gameStatus = Core.GameState.Playing;
        }

        public void Exit()
        {
            SoundBase.Instance.PlayOneShot(SoundBase.Instance.click);
            
            if(gameEnded)
                SceneManager.LoadScene(0);
        }
    }
}
