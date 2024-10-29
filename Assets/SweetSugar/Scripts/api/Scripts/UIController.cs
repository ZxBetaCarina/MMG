using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

	public Transform registerObj;
	public Transform loginObj;
	public Transform chooseObj;

	public GameObject googleObj;
	public GameObject appleObj;

	public GameObject disconnectPanel;


	TMP_InputField _userName;
	TMP_InputField _email;
	TMP_InputField _password;
	TMP_InputField _confirmPassword;
	TMP_InputField _googleID;
	TMP_InputField _facebookID;
	TMP_InputField _appleID;

	//public void chooseMenu()
	//{
	//	registerObj.gameObject.SetActive(false);
	//	loginObj.gameObject.SetActive(false);
	//	chooseObj.gameObject.SetActive(true);
	//}

	//public void chooseLogin(int i)
	//{
	//	if (i == 0)
	//	{
	//		registerObj.gameObject.SetActive(true);
	//		loginObj.gameObject.SetActive(false);
	//		chooseObj.gameObject.SetActive(false);
	//	}
	//	else
	//	{
	//		registerObj.gameObject.SetActive(false);
	//		loginObj.gameObject.SetActive(true);
	//		chooseObj.gameObject.SetActive(false);
	//	}
	//}


	//public bool validateFieldData(Transform obj)
	//{
	//	foreach (Transform child in obj)
	//	{
	//		if (child.gameObject.GetComponent<Button>() == null)
	//			if (child.gameObject.GetComponent<TMP_InputField>().text == "")
	//				return false;
	//	}
	//	return true;
	//}

	#region Login 

	//public void login()
	//{
	//	if (!validateFieldData(loginObj))
	//	{
	//		Debug.LogError("Input field is empty...!");
	//		return;
	//	}

	//	//Gettiing all child and getting all childs inputfield
	//	foreach (Transform child in loginObj)
	//	{
	//		if (child.gameObject.name == "password")
	//		{
	//			_password = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//		if (child.gameObject.name == "email")
	//		{
	//			_email = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//	}

	//	StartCoroutine(//ApiManager.instance.LoginUser(_email.text, _password.text, "Android"));
	//}

	#endregion

	#region Register 

	//public void register()
	//{
	//	if (!validateFieldData(registerObj))
	//	{
	//		Debug.LogError("Input field is empty...!");
	//		return;
	//	}

	//	//Gettiing all child and getting all childs inputfield
	//	foreach (Transform child in registerObj)
	//	{
	//		if (child.gameObject.name == "username")
	//		{
	//			_userName = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//		if (child.gameObject.name == "password")
	//		{
	//			_password = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//		if (child.gameObject.name == "email")
	//		{
	//			_email = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//		if (child.gameObject.name == "confirm_pass")
	//		{
	//			_confirmPassword = child.gameObject.GetComponent<TMP_InputField>();
	//		}
	//	}

	//	if (_password.text.Length <= 4 || _password.text != _confirmPassword.text)
	//	{
	//		Debug.LogError("Enter password correctly");
	//		return;
	//	}

	//	StartCoroutine(//ApiManager.instance.RegisterUser(_userName.text, _email.text, _password.text, _confirmPassword.text, "Android"));

	//}

	#endregion

	public void googleLogin()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("Error. Check internet connection!");
			callDisconnect(true);
			Invoke("callDisconnect", 3f);
			callDisconnect(false);
			
			return;
		}

		//googleObj.GetComponent<GoogleSignInDemo>().SignInWithGoogle();

	}

	void callDisconnect(bool active)
	{
		disconnectPanel.SetActive(active);
	}

	public void forgotPassword()
	{
		////ApiManager.instance.DebitCoin(GameData.email);
		//ApiManager.instance.forgotPassword("ajad.patel@gmail.com");
	}


	public void appleLogin()
	{
		//appleObj.GetComponent<AppleLogin>().SignInWithApple();

	}


	#region Coin stuffs API's

	public void creditCoins()
	{
		////ApiManager.instance.DebitCoin(GameData.userID, GameData.userCoins, "Coins debited..");
		//ApiManager.instance.CreditCoin(GameData.userID, 200, "Coins Credited..");
	}

	public void debitCoins()
	{
		////ApiManager.instance.DebitCoin(GameData.userID, GameData.userCoins, "Coins debited..");
		//ApiManager.instance.DebitCoin(GameData.userID, 200, "Coins debited..");
	}

	public void coinHistory()
	{
		////ApiManager.instance.DebitCoin(GameData.usereID);
		//ApiManager.instance.coinHistory(GameData.userID);
	}

	#endregion


	#region Power ups stuffs related API's

	public void addPowerUp()
	{
		////ApiManager.instance.DebitCoin(GameData.userID, GameData.currentLevel, GameData.currentLevelStars );
		//ApiManager.instance.AddPowerUp(GameData.userID, 6, 2);
	}

	public void removePowerUp()
	{
		////ApiManager.instance.DebitCoin(GameData.userID, GameData.currentLevel, GameData.currentLevelStars );
		//ApiManager.instance.RemovePowerUp(GameData.userID, 6, 2);
	}

	public void displayUserPowerupInventory()
	{
		////ApiManager.instance.DebitCoin(GameData.userID);
		//ApiManager.instance.displayUserPowerupInventory(GameData.userID);
	}

	#endregion


	#region Ticket stuffs all API calling methods

	public void generateTicket()
	{
		////ApiManager.instance.DebitCoin(GameData.userID);
		//ApiManager.instance.generateTicket(GameData.userID, "How to get power up?", "Play game and get power");
	}

	public void closeTicket()
	{
		////ApiManager.instance.DebitCoin(GameData.userID);
		//ApiManager.instance.closeTicket(GameData.userID);
	}

	
	public void supportListByUser()
	{
		////ApiManager.instance.DebitCoin(GameData.userID);
		//ApiManager.instance.supportListByUser(GameData.userID);
	}

	public void replyMessage()
	{
		////ApiManager.instance.DebitCoin(GameData.userID);
		//ApiManager.instance.replyMessage(GameData.userID, "Hey there, I'm testing reply Message..!");
	}

	#endregion



}
