using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.PUNScripts;

public class NavigationManager : MonoBehaviour
{
    public GameObject MultiplayerCanvas, LoadingScreen;

    #region Singleton

    public static NavigationManager navigate;

    private void Awake()
    {
        if (navigate != null && navigate != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            navigate = this;
        }
    }

    public static NavigationManager GetInstance()
    {
        return navigate;
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "main")
            {
                if (MultiplayerCanvas.activeInHierarchy)
                {
                    DisconnectAndGotoHome();
                }
                else if (LoadingScreen.activeInHierarchy)
                {
                    DisconnectAndGotoHome();
                }
                //else
                //{
                //    Application.Quit();
                //}
            }
            else if(SceneManager.GetActiveScene().name == "game")
            {
                if(LevelManager.THIS.gameStatus == GameState.Win)
                {
                    GameplaySceneScript.Instance.Exit();
                }
                else
                {
                    GameObject settings = GameObject.Find("CanvasGlobal").transform.GetChild(0).gameObject;
                    SweetSugar.Scripts.GUI.AnimationEventManager.Instance.OpneMenu(settings);
                }
            }
        }
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void DisconnectAndGotoHome()
    {
        MultiplayerCanvas.SetActive(false);
        LoadingScreen.SetActive(false);
    }
}
