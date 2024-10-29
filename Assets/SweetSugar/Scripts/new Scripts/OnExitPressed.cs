using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitPressed : MonoBehaviour
{
	[SerializeField] GameObject exitPopUp;
	[SerializeField] GameObject mainBG;
	bool isPressed = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPressed = !isPressed;
			mainBG.SetActive(!isPressed);
			exitPopUp.SetActive(isPressed);
		}
	}

	public void onBtnPressed(bool exit)
	{
		if (exit)
		{
			Application.Quit();
			Debug.Log("Quit");
		}

		else
		{
			isPressed = !isPressed;
			exitPopUp.SetActive(false);
			mainBG.SetActive(true);
		}
	}


}
