using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class InventoryOption : MonoBehaviour
{

	[SerializeField] GameObject inventoryMenu;
	[SerializeField] GameObject shopMenu;

	private void Start()
	{
		onInventoryPressed();
	}

	public void onInventoryPressed()
	{
		inventoryMenu.SetActive(true);
		shopMenu.SetActive(false);
		
		transform.GetChild(0).GetComponent<Button>().interactable = false;
		transform.GetChild(1).GetComponent<Button>().interactable = true;

		Color color;
		if (ColorUtility.TryParseHtmlString("#FAFF00", out color))
        {
			transform.GetChild(0).GetComponentInChildren<Text>().color = color;
		}
		if (ColorUtility.TryParseHtmlString("#1795D6", out color))
        {
			transform.GetChild(1).GetComponentInChildren<Text>().color = color;
		}
	}
	

	public void onShopPressed()
	{
		inventoryMenu.SetActive(false);
		shopMenu.SetActive(true);
		
		transform.GetChild(0).GetComponent<Button>().interactable = true;
		transform.GetChild(1).GetComponent<Button>().interactable = false;
		
		Color color;
		if (ColorUtility.TryParseHtmlString("#FAFF00", out color))
        {
			transform.GetChild(1).GetComponentInChildren<Text>().color = color;
		}
		if (ColorUtility.TryParseHtmlString("#1795D6", out color))
        {
			transform.GetChild(0).GetComponentInChildren<Text>().color = color;
		}
		
	}


}
