using System;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Integrations;
using UnityEngine;
using UnityEngine.UI;

namespace SweetSugar.Scripts.Monetization
{
    public class VideoButtonMap : MonoBehaviour
    {
        public Animator anim;
        public Button button;

        public GameObject coinBG;


        private void OnEnable()
        {
            button.interactable = true;
            Invoke("Prepare",2);
        }

        private void Prepare()
        {
            //if (AdsManager.THIS.GetRewardedUnityAdsReady(RewardsType.GetCoinsMap))
            //if (ADsManager.instance.isAdsAreReady())
            //{
            //    ShowButton();
            //}
        }
       
        private void ShowButton()
        {
            anim.SetTrigger("show");
        }

        public void Hide()
        {
            button.interactable = false;
            coinBG.SetActive(true);
        }

        public void onAdsBtnClicked()
		{
            ADsManager.instance.playRewarded();
            InitScript.Instance.currentReward = RewardsType.GetGems;
            Hide();

        }

    }
}