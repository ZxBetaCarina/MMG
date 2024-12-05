// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;
//
// public class UpdateWalletApi
// {
//     private static int _earnedPoints;
//     private static int _gamingPoints;
//
//     private static void GetWallet(Action<(string, string)> onSuccess, Action<string> onError)
//     {
//         ApiManager.Get<GetWalletResponseData>(ServiceURLs.GetWallet,
//             (GetWalletResponseData obj) =>
//             {
//                 OnSuccessGetWallet(obj);
//                 onSuccess?.Invoke((_earnedPoints.ToString(), _gamingPoints.ToString()));
//             },
//             onError);
//     }
//
//     private static void OnSuccessGetWallet(GetWalletResponseData obj)
//     {
//         if (obj.status)
//         {
//             CustomLog.SuccessLog(obj.message);
//             _earnedPoints = obj.data.earnedPoints;
//             _gamingPoints = obj.data.gamingPoints;
//         }
//     }
//
//     private static void OnErrorGetWallet(string obj)
//     {
//         CustomLog.ErrorLog(obj);
//     }
//
//     /// <summary>
//     /// Get Wallet Amount From Server Directly with a callback.
//     /// </summary>
//     /// <returns>
//     /// item1 = Earned Points,
//     /// item2 = Gaming Points
//     /// </returns>
//     public static void GetWalletAmount(Action<(string, string)> onSuccess, Action<string> onError)
//     {
//         GetWallet(onSuccess, onError);
//     }
// }
// public class UpdateWalletRequest
// {
//     public bool isCredit { get; set; }
//     public int gamingPoints { get; set; }
//     public int earnedPoints { get; set; }
// }
// public class UpdateWalletResponse
// {
//     public bool status { get; set; }
//     public string message { get; set; }
//     public UpdateWalletData data { get; set; }
// }
//
// public class UpdateWalletData
// {
//     public string _id { get; set; }
//     public string gameUserId { get; set; }
//     public int gamingPoints { get; set; }
//     public int earnedPoints { get; set; }
//     public List<TransactionHistory> transactionHistory { get; set; }
//     public DateTime createdAt { get; set; }
//     public DateTime updatedAt { get; set; }
//     public int __v { get; set; }
// }
// public class TransactionHistory
// {
//     public string transactionType { get; set; }
//     public string pointType { get; set; }
//     public int points { get; set; }
//     public string description { get; set; }
//     public string _id { get; set; }
//     public DateTime date { get; set; }
// }