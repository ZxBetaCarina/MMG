using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Achievements
{

	#region Achievement section 5 X times

	public static int timesFreeMoveUsed;
    public static bool unlockedFreeMoveUsed5X;
    public static string IDtimesFreeMoveUsed5X = "CgkIoMTJrqgZEAIQBA";
    
    public static int multicolorCandyUsed;
    public static bool unlockedMulticolorCandyUsed5X;
    public static string IDmulticolorCandyUsed5X = "CgkIoMTJrqgZEAIQBQ";

    public static int extraMovesUsed;
    public static bool unlockedExtraMovesUsed5X;
    public static string IDextraMovesUsed5X = "CgkIoMTJrqgZEAIQBg";

    public static int extraTimeUsed;
    public static bool unlockedExtraTimeUsed5X;
    public static string IDextraTimeUsed5X = "CgkIoMTJrqgZEAIQBw";

	#endregion


	#region Achievement section 25 X times

    public static bool unlockedFreeMoveUsed25X;
    public static string IDtimesFreeMoveUsed25X = "CgkIoMTJrqgZEAIQCA";
    
    public static int multicolorCandyUsed25X;
    public static bool unlockedMulticolorCandyUsed25X;
    public static string IDmulticolorCandyUsed25X = "CgkIoMTJrqgZEAIQCQ";

    public static int extraMovesUsed25X;
    public static bool unlockedExtraMovesUsed25X;
    public static string IDextraMovesUsed25X = "CgkIoMTJrqgZEAIQCg";

    public static int extraTimeUsed25X;
    public static bool unlockedExtraTimeUsed25X;
    public static string IDextraTimeUsed25X = "CgkIoMTJrqgZEAIQCw";

	#endregion

    
	#region Achievement section 50 X times

    public static bool unlockedFreeMoveUsed50X;
    public static string IDtimesFreeMoveUsed50X = "CgkIoMTJrqgZEAIQDA";
    
    public static int multicolorCandyUsed50X;
    public static bool unlockedMulticolorCandyUsed50X;
    public static string IDmulticolorCandyUsed50X = "CgkIoMTJrqgZEAIQDQ";

    public static int extraMovesUsed50X;
    public static bool unlockedExtraMovesUsed50X;
    public static string IDextraMovesUsed50X = "CgkIoMTJrqgZEAIQDg";

    public static int extraTimeUsed50X;
    public static bool unlockedExtraTimeUsed50X;
    public static string IDextraTimeUsed50X = "CgkIoMTJrqgZEAIQDw";

    #endregion


    #region Level Achievement stuffs

    public static string IDlevelAchievement = "CgkIoMTJrqgZEAIQEA";



    #endregion


    #region Score Achievement stuffs

    public static int scoreAchievement;

    public static bool unlockedScoreAchievement1st;
    public static bool unlockedScoreAchievement2st;
    public static bool unlockedScoreAchievement3st;
    public static bool unlockedScoreAchievement4st;
    public static bool unlockedScoreAchievement5st;


    // ALl the Achievement ID related to score.
    public static string IDScoreAchievement1st = "CgkIoMTJrqgZEAIQEQ";
    public static string IDScoreAchievement2st = "CgkIoMTJrqgZEAIQEg";
    public static string IDScoreAchievement3st = "CgkIoMTJrqgZEAIQEw";
    public static string IDScoreAchievement4st = "CgkIoMTJrqgZEAIQFA";
    public static string IDScoreAchievement5st = "CgkIoMTJrqgZEAIQFQ";

    #endregion


    #region Chocolate Block related stuffs 

    public static int chocolateAchievementCount;

    public static bool unlockedChocolateAchievement1;
    public static bool unlockedChocolateAchievement2;
    public static bool unlockedChocolateAchievement3;
    public static bool unlockedChocolateAchievement4;

    // ALl the Achievement ID related to chocolate block.
    public static string IDChocoAchievment1 = "CgkIoMTJrqgZEAIQFg";
    public static string IDChocoAchievment2 = "CgkIoMTJrqgZEAIQFw";
    public static string IDChocoAchievment3 = "CgkIoMTJrqgZEAIQGA";
    public static string IDChocoAchievment4 = "CgkIoMTJrqgZEAIQGQ";

    #endregion


    #region sugar square Block related stuffs 

    public static int sugarSquareAchievementCount;

    public static bool unlockedsugarSquareAchievement1;
    public static bool unlockedsugarSquareAchievement2;
    public static bool unlockedsugarSquareAchievement3;
    public static bool unlockedsugarSquareAchievement4;

    // ALl the Achievement ID related to sugarSquare block.
    public static string IDsugarSquareAchievment1 = "CgkIoMTJrqgZEAIQGg";
    public static string IDsugarSquareAchievment2 = "CgkIoMTJrqgZEAIQGw";
    public static string IDsugarSquareAchievment3 = "CgkIoMTJrqgZEAIQHA";
    public static string IDsugarSquareAchievment4 = "CgkIoMTJrqgZEAIQHQ";

    #endregion


    #region Solid block related stuffs

    public static int solidBlockCount;

    public static bool unlockedSolidBLock1;
    public static bool unlockedSolidBLock2;
    public static bool unlockedSolidBLock3;
    public static bool unlockedSolidBLock4;

    public static string IDsolidBlockAchievement1 = "CgkIoMTJrqgZEAIQHg";
    public static string IDsolidBlockAchievement2 = "CgkIoMTJrqgZEAIQHw";
    public static string IDsolidBlockAchievement3 = "CgkIoMTJrqgZEAIQIA";
    public static string IDsolidBlockAchievement4 = "CgkIoMTJrqgZEAIQIQ";


    #endregion


    #region Level failed achievement

    public static int timesLevelFailed = PlayerPrefs.GetInt("timesLevelFailed");

    public static bool unlockedLevelFailed1;
    public static bool unlockedLevelFailed2;
    public static bool unlockedLevelFailed3;
    public static bool unlockedLevelFailed4;

    public static string IDtimesLevelFailed1 = "CgkIoMTJrqgZEAIQIg";
    public static string IDtimesLevelFailed2 = "CgkIoMTJrqgZEAIQIw";
    public static string IDtimesLevelFailed3 = "CgkIoMTJrqgZEAIQJA";
    public static string IDtimesLevelFailed4 = "CgkIoMTJrqgZEAIQJQ";

    #endregion


    #region Collected daily bonus

    public static int timesCollectedBonus;

    public static bool unlockedDailyBonus1;
    public static bool unlockedDailyBonus2;
    public static bool unlockedDailyBonus3;
    public static bool unlockedDailyBonus4;

    public static string IDtimesDailyBonus1 = "CgkIoMTJrqgZEAIQJg";
    public static string IDtimesDailyBonus2 = "CgkIoMTJrqgZEAIQJw";
    public static string IDtimesDailyBonus3 = "CgkIoMTJrqgZEAIQKA";
    public static string IDtimesDailyBonus4 = "CgkIoMTJrqgZEAIQKQ";

	#endregion





}
