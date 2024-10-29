using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
	#region User details

	public static int userID;
    public static string userName;
    public static string userEmail;
    public static string userPPURL;
    public static Sprite userDP;
    public static string socialMediaId;
    public static string firebaseToken;
    public static string referralCode;
    public static bool isReferalCodeUsed;

	#endregion

	public static int userCoins;
    public static int totalScore;


	#region Level related stuffs

	public static int currentLevel;
    public static int currentLevelStars;
    public static int currentLevelScore;
    public static int lifes;
    public static int totalUnlockedLevels;

    // To save number of levels unlocked
    public static Dictionary<int, int> levels = new Dictionary<int, int>();

	#endregion

	public static List<PowerUps> powerUp;



}
