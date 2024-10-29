using System;
using System.Collections;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Integrations;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Integrations
{
    public class ShowAdsByButton : MonoBehaviour
    {
        public UnityEvent OnRewaredeShown;
        public bool checkRewardedAvailable;
        private CanvasGroup canvasGroup;
        public RewardsType placement;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

        }

        private void OnEnable()
        {
            if (canvasGroup != null)
            {
                
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                if (checkRewardedAvailable && GetComponent<Button>().onClick.GetPersistentMethodName(0) == "ShowRewardedAd" /*&& !AdManager.Instance.IsRewardedAvailable()*/)
                {
                    canvasGroup.alpha = 0;
                    canvasGroup.blocksRaycasts = false;
                }
            }

        }        

        private void OnDisable()
        {

        }

        private void OnRewardedShown()
        {
            OnRewaredeShown?.Invoke();

        }

        // public void ShowInterstitial(AdEvents placement)
        // {
        //     if (PlayerPrefs.GetInt("tutorialShown", 0) == 0) return;
        //     AdsManager.THIS.ShowVideo(placement);
        //     EventsListener.CustomEvent("Show ads " +placement);
        // }

    }
}