using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.GUI;



public class ADsManager : MonoBehaviour
{

	public static ADsManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Advertisement.Initialize("4677377", true);
		//Advertisement.AddListener(this);

	}

	public void playInterstitial()
	{

		//if (Advertisement.IsReady("Interstitial_Android"))
		//{
		//	Advertisement.Show("Interstitial_Android");
		//}

	}


	public void playRewarded()
	{

		//if (Advertisement.IsReady("Rewarded_Android"))
		//{
		//	Advertisement.Show("Rewarded_Android");
		//}
		//else
		//{
		//	Debug.Log("Rewarded not loaded");
		//}
	}



	public void OnUnityAdsReady(string placementId)
	{
		Debug.Log("Ads are Ready...!");
	}

	public void OnUnityAdsDidError(string message)
	{
		Debug.Log("Failed to load Ads : " + message);
		
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		Debug.Log("Video Started ");
	}

	//public bool isAdsAreReady()
	//{
		//if (Advertisement.IsReady("Rewarded_Android"))
		//	return true;

		//return false;
	//}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{

		AnimationEventManager.Instance.CloseMenu();

		if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
		{
			InitScript.Instance.ShowReward();
		}

	}

}
