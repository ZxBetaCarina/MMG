using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour
{

	public int ItemNumber = 0;
	public Image PowerUpImg;
	public Text quantityTxt;

	public Sprite[] ItemSprites;
	
	int quantity;

	/* 
	1. Multi color Bomb
	2. +5 Moves
	3. +30 secs.
	4. Hand toool
	5. Magic stick.
	6. Row Color Bomb.
	7. Col Color Bomb.
	8. Packages.
	9. Malmarade.
	 */





	private void Start()
	{
		////ApiManager.instance.displayUserPowerupInventory(GameData.userID);
		
		setPowerUps();
	}

	void setPowerUps()
	{
		List<PowerUps> powerUps = new List<PowerUps>();
		GameData.powerUp = powerUps;
		
		switch (ItemNumber)
		{
			case 0:
				Debug.Log("Enter correct index for powereup");

				break;

			case 1:
				PowerUpImg.sprite = ItemSprites[0];

				quantityTxt.text = GameData.powerUp[7].quantity.ToString();

				break;

			case 2:
				PowerUpImg.sprite = ItemSprites[1];

				//GameData.powerUp = powerUps;
				quantityTxt.text = GameData.powerUp[3].quantity.ToString();

				break;
				
			case 3:
				PowerUpImg.sprite = ItemSprites[2];

				//GameData.powerUp = powerUps;
				quantityTxt.text = GameData.powerUp[4].quantity.ToString();

				break;

			default:
				PowerUpImg.sprite = ItemSprites[2];

				//GameData.powerUp = powerUps;
				quantityTxt.text = GameData.powerUp[4].quantity.ToString();

				break;


		}


	}



}
