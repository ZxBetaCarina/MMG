using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButton : MonoBehaviour
{

	public GameObject login;
	public GameObject register;
	public GameObject chooseMenu;
	public GameObject mainUiMenu;

	private void Start()
	{
		login.SetActive(false);
		register.SetActive(false);
		chooseMenu.SetActive(false);
		mainUiMenu.SetActive(true);
	}

	public void hideMainUI()
	{
		mainUiMenu.SetActive(false);
	}


	public void onBackClick(int index)
	{
		switch (index)
		{

			case 0:
				login.SetActive(false);
				register.SetActive(false);
				chooseMenu.SetActive(false);
				mainUiMenu.SetActive(false);
				break;


			case 1:
				login.SetActive(false);
				register.SetActive(false);
				chooseMenu.SetActive(true);
				mainUiMenu.SetActive(false);
				break;

			case 2:
				login.SetActive(false);
				register.SetActive(false);
				chooseMenu.SetActive(false);
				mainUiMenu.SetActive(true);
				break;

			case 3:
				login.SetActive(false);
				register.SetActive(false);
				chooseMenu.SetActive(true);
				mainUiMenu.SetActive(false);
				break;

			default:
				break;

		}


	}

}
