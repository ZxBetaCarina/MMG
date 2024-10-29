using UnityEngine;
using TMPro;

public class QuerySection : MonoBehaviour
{

	public TMP_InputField titleField;
	public TMP_InputField questionField;

	public GameObject queryPanel;
	public GameObject mainUI;


    public void onFormSubmitCLick()
	{
		mainUI.SetActive(true);
		string title = titleField.text.ToString();
		string question = questionField.text.ToString();


		//ApiManager.instance.generateTicket(GameData.userID, title, question);
		////ApiManager.instance.generateTicket(3, title, question);
		queryPanel.SetActive(false);
	}

	public void backButtonPressed()
	{
		mainUI.SetActive(true);
		titleField.text = "";
		questionField.text = "";

		queryPanel.SetActive(false);
	}

	public void onPostQueryPressed()
	{
		mainUI.SetActive(false);
		titleField.text = "";
		questionField.text = "";
 		queryPanel.SetActive(true);
	}



}
