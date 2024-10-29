using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Integrations;
using SweetSugar.Scripts.MapScripts.StaticMap.Editor;
using SweetSugar.Scripts.System;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class LoadingBar : MonoBehaviour
{
    public GameObject[] childObjects;


	private void OnEnable()
	{
        StartCoroutine(loadingBar());
	}

    IEnumerator loadingBar()
    {
        yield return null;

        //ApiManager.instance.callFetchMatchData(GameData.userID);
        ////ApiManager.instance.callFetchMatchData(GameData.userID);

        yield return new WaitForSeconds(.5f);

        //LeanTween.delayedCall(1, ()=>SceneManager.LoadScene(Resources.Load<MapSwitcher>("Scriptable/MapSwitcher").GetSceneName()));
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Resources.Load<MapSwitcher>("Scriptable/MapSwitcher").GetSceneName());

        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            //Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");

            if (asyncOperation.progress >= 0f)
            {
                childObjects[0].SetActive(true);
            }
            if (asyncOperation.progress >= 0.1f)
            {
                childObjects[1].SetActive(true);
            }
            if (asyncOperation.progress >= 0.3f)
            {
                childObjects[2].SetActive(true);
            }
            if (asyncOperation.progress >= 0.5f)
            {
                childObjects[3].SetActive(true);
            }
            if (asyncOperation.progress >= 0.7f)
            {
                childObjects[4].SetActive(true);
            }
            if (asyncOperation.progress >= 0.8f)
            {
                childObjects[5].SetActive(true);
            }

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                asyncOperation.allowSceneActivation = true;
                childObjects[6].SetActive(true);
            }

            
            //InitScript.Instance.SaveLevelStarsCount(GameData.levels);



			//for (int i = 0; i < 10; i++)
			//{
   //             //ApiManager.instance.UpdateGameResult(GameData.userID, i, GameData.levels[i]);
			//}

            yield return new WaitForSeconds(2f);
            
        }
    }
}
