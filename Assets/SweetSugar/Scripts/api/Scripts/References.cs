using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static string baseApi = "http://bearsaga.techymau.games/api/UserApi/";

    // User Login API's
    public static string userRegister = "UserRegister";
    public static string userLogin = "UserLogin";
    public static string appleLogin= "AppleLogin";
    public static string googleLogin = "GoogleLogin";
    public static string facebookLogin = "FacebookLogin";

    // User details API's
    public static string userDetails= "UserDetail";
    public static string updatePic= "UpdatePic";
    public static string forgotPassword= "ForgotPassword";

    // User Score related API's
    public static string debitCoins= "CoinDebit";
    public static string creditCoins= "CoinCredit";
    public static string coinHistory= "CoinHistory";
    public static string updateGameResult= "UpdateGameResult";
    public static string fetchMatchHistory= "FetchMatchHistory";

    // Power up related API's
    public static string addPowerUp= "AddPowerUp";
    public static string removePowerUp= "RemovePowerUp";
    public static string displayUserPowerupInventory = "DisplayUserPowerupInventory";


    // Tickets related API's

    public static string generateTicket = "GenerateTicket";
    public static string closeTicket = "CloseTicket";
    public static string supportListByUser = "SupportListByUser";
    public static string replyMessage = "ReplyMessage";








}
