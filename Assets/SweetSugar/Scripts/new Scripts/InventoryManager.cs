using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject inventoryScreen;
    [SerializeField] GameObject MainScreen;

	private void Start()
	{
		inventoryScreen.SetActive(false);
	}

	public void onInventoryPressed()
	{
		inventoryScreen.SetActive(true);
		MainScreen.SetActive(false);

	}


}
