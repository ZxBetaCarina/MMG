using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserProfileManager : MonoBehaviour
{

	public static UserProfileManager instance;

    public Image userdp;
    public GameObject profilePanel;

	[SerializeField] TMP_Text nameTxt;
	[SerializeField] TMP_Text emailTxt;
	[SerializeField] TMP_Text coinTxt;
	[SerializeField] TMP_Text levelTxt;
	[SerializeField] TMP_Text scoreTxt;
	[SerializeField] Image DPSprite;

	[SerializeField] Button facebook;
	[SerializeField] Button google;
	[SerializeField] GameObject warningPopup;


	public GameObject mainUI;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		{ 
			string loginType = PlayerPrefs.GetString("LoginType");
			if (loginType == "Facebook")
			{
				facebook.interactable = false;

				facebook.GetComponentInChildren<Text>().text = "Connected With Facebook";
				google.GetComponentInChildren<Text>().text = "Connect With Google";
			
			}

			if (loginType == "Google")
			{
				google.interactable = false;
				
				facebook.GetComponentInChildren<Text>().text = "Connect With Facebook";
				google.GetComponentInChildren<Text>().text = "Connected With Google";
			
			}

		} // this code is to check user login details


		profilePanel.SetActive(false);
	}
	
	public void callChange()
	{
		StartCoroutine(change());
	}

	IEnumerator change()
	{
		yield return new WaitForSeconds(1f);
		nameTxt.text = GameData.userName;
		emailTxt.text = GameData.userEmail;
		coinTxt.text = "Coins  :  " + GameData.userCoins.ToString();
		levelTxt.text = "Level  :  " + (PlayerPrefs.GetInt("CurrentLvl"));
		scoreTxt.text = "Score  :  " + GameData.totalScore.ToString();
		DPSprite.sprite = GameData.userDP;
		//PlayerPrefs.SetInt("CurLevel");

		yield return new WaitForSeconds(.5f);
		userdp.sprite = GameData.userDP;
	}

	public void onProfileClick()
	{
		StartCoroutine(change());
		mainUI.SetActive(false);
		profilePanel.SetActive(true);
	}

	public void onBackClicked()
	{
		mainUI.SetActive(true);
		profilePanel.SetActive(false);

	}

	public void showWarningPopup()
	{
		warningPopup.SetActive(true);
	}



	public void logoutButton()
	{
		StartCoroutine(logout());
	}

	IEnumerator logout()
	{
		yield return null;

		string loginType = PlayerPrefs.GetString("LoginType");

		if (loginType == "Facebook")
		{
			//FacebookLogin.instance.OnLogOut();
		}

		if (loginType == "Google")
		{
			//GoogleSignInDemo.instance.SignOutFromGoogle();
		}

		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("Login_Page");

	}

}
