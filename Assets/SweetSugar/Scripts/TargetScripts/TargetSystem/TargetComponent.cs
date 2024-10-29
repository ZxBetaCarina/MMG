using UnityEngine;

namespace SweetSugar.Scripts.TargetScripts.TargetSystem
{
    /// <summary>
    /// Target component adding for object
    /// </summary>
    public class TargetComponent : MonoBehaviour
    {
        private bool quit;

        public delegate void DestroyDelegate(GameObject obj);
        public static event DestroyDelegate OnDestroyEvent;

        // void OnEnable()
        // {
        //     this.OnDestroyEvent += LevelManager.This.target.DestroyEvent;
        // }

        // void OnDisable()
        // {
        //     this.OnDestroyEvent -= LevelManager.This.target.DestroyEvent;
        // }

        void OnApplicationQuit()
        {
            quit = true;
        }

        void OnDestroy()
        {
            if (!quit) OnDestroyEvent(gameObject);
			switch (gameObject.name) {

                case "SugarSquare":
                    Achievements.sugarSquareAchievementCount += 1;
                    PlayerPrefs.SetInt("sugarSquareAchievementCount", Achievements.sugarSquareAchievementCount);
                    AchievementManager.instance.checksugarSquareAchievement(Achievements.sugarSquareAchievementCount);

                    break;

				case "SolidBlock":
					Achievements.solidBlockCount += 1;
					PlayerPrefs.SetInt("solidBlockCount", Achievements.solidBlockCount);
					AchievementManager.instance.checksolidBlockAchievement(Achievements.solidBlockCount);

					break;

                default:
                    Debug.Log(gameObject.name);
                    
                    break;

			}
		
        
        
        }
	}
}