using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

public class SetMyData : MonoBehaviour
{

    public GameObject avatar;
    public GameObject name;
    public GameObject matchCanvas;
    public GameObject controlAvatars;
    public GameObject backButton;
    public GameObject hidePalyerMatch;
    public bool isCancle = false;


    // Use this for initialization


    public void MatchPlayer()
    {

        //name.GetComponent<Text>().text = GameManager.Instance.nameMy;
        if (GameManager.Instance.avatarMy != null)
            avatar.GetComponent<Image>().sprite = GameManager.Instance.avatarMy;


        controlAvatars.GetComponent<ControlAvatars>().reset();

    }

    public void canclebotmatch()
    {
        hidePalyerMatch.gameObject.SetActive(false); 
        CancelInvoke("PressedStartGame1v1WithBots");
        CancelInvoke("StartGame");
       
      PhotonNetwork.BackgroundTimeout = StaticStrings.photonDisconnectTimeoutLong;
       //GameManager.Instance.cueController.removeOnEventCall();
       SceneManager.LoadScene(0);
       GameManager.Instance.playfabManager.roomOwner = false;
       GameManager.Instance.roomOwner = false;
       GameManager.Instance.resetAllData();

    }
    public void setBackButton(bool active)
    {
        backButton.SetActive(active);
    }
}
