using System;
using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scriptable.Rewards;
using SweetSugar.Scripts.GUI;
using SweetSugar.Scripts.GUI.Boost;
using SweetSugar.Scripts.Integrations;
using SweetSugar.Scripts.Integrations.Network;
using SweetSugar.Scripts.Level;
using SweetSugar.Scripts.MapScripts;
using SweetSugar.Scripts.System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace SweetSugar.Scripts.Core
{
    /// <summary>
    /// class for main system variables, ads control and in-app purchasing
    /// </summary>
    public class InitScript : MonoBehaviour
    {
        public static InitScript Instance;

        /// opening level in Menu Play
        public static int openLevel;

        ///life gaining timer
        public static float RestLifeTimer;

        ///date of exit for life timer
        public static string DateOfExit;

        //reward which can be receive after watching rewarded ads
        public RewardsType currentReward;

        ///amount of life
        public static int lifes { get; set; }

        //EDITOR: max amount of life
        public int CapOfLife = 5;

        //EDITOR: time for rest life
        public float TotalTimeForRestLifeHours;

        //EDITOR: time for rest life
        public float TotalTimeForRestLifeMin = 15;

        //EDITOR: time for rest life
        public float TotalTimeForRestLifeSec = 60;

        //EDITOR: coins gifted in start
        public int FirstGems = 20;

        //amount of coins
        public static int Gems;

        //wait for purchasing of coins succeed
        public static int waitedPurchaseGems;

        //EDITOR: how often to show the "Rate us on the store" popup
        public int ShowRateEvery;

        //EDITOR: rate url
        public string RateURL;

        public string RateURLIOS;

        //rate popup reference
        private GameObject rate;

        //EDITOR: amount for rewarded ads
        public int rewardedGems = 5;

        //EDITOR: should player lose a life for every passed level
        public bool losingLifeEveryGame;

        //daily reward popup reference
        public GameObject DailyMenu;

        // Use this for initialization
        void Awake()
        {
            Application.targetFrameRate = 60;
            Instance = this;
            RestLifeTimer = PlayerPrefs.GetFloat("RestLifeTimer");
            DateOfExit = PlayerPrefs.GetString("DateOfExit", "");
            if (DateOfExit == "" || DateOfExit == default(DateTime).ToString())
                DateOfExit = ServerTime.THIS.serverTime.ToString();
            DebugLogKeeper.Init();
            //Gems = PlayerPrefs.GetInt("Gems");
            Gems = GameData.userCoins;
            lifes = PlayerPrefs.GetInt("Lifes");
            if (PlayerPrefs.GetInt("Lauched") == 0)
            {
                //First lauching
                lifes = CapOfLife;
                PlayerPrefs.SetInt("Lifes", lifes);
                Gems = FirstGems;
                PlayerPrefs.SetInt("Gems", Gems);
                //PlayerPrefs.SetInt("Music", 1);
                PlayerPrefs.SetInt("Sound", 1);

                PlayerPrefs.SetInt("Lauched", 1);
                PlayerPrefs.Save();
            }

//            rate = Instantiate(Resources.Load("Prefabs/Rate")) as GameObject;
//            rate.SetActive(false);
//            rate.transform.SetParent(MenuReference.THIS.transform);
//            rate.transform.localPosition = Vector3.zero;
//            rate.GetComponent<RectTransform>().offsetMin = new Vector2(-5, -5);
//            rate.GetComponent<RectTransform>().offsetMax = new Vector2(5, 5);
////        rate.GetComponent<RectTransform>().anchoredPosition = (Resources.Load("Prefabs/Rate") as GameObject).GetComponent<RectTransform>().anchoredPosition;
//            rate.transform.localScale = Vector3.one;
            //var g = MenuReference.THIS.Reward.gameObject;
            //g.SetActive(true);
            //g.SetActive(false);
            //if (CrosssceneData.totalLevels == 0)
            //    CrosssceneData.totalLevels = LoadingManager.GetLastLevelNum();
#if FACEBOOK
            FacebookManager fbManager = new GameObject("FacebookManager").AddComponent<FacebookManager>();
#endif
#if GOOGLE_MOBILE_ADS
            var obj = FindObjectOfType<RewAdmobManager>();
            if (obj == null)
            {
                GameObject gm = new GameObject("AdmobRewarded");
                gm.AddComponent<RewAdmobManager>();
            }
#endif
        }


        public void SaveLevelStarsCount(int level, int starsCount)
        {
            Debug.Log(string.Format("Stars count {0} of level {1} saved.", starsCount, level));
            PlayerPrefs.SetInt(GetLevelKey(level), starsCount);

        }

        private string GetLevelKey(int number)
        {
            return string.Format("Level.{0:000}.StarsCount", number);
        }

        public void ShowRate()
        {
            rate.SetActive(true);
        }


        public void ShowReward()
        {
            var reward = MenuReference.THIS.Reward.GetComponent<RewardIcon>();
            if (currentReward == RewardsType.GetGems)
            {
                ShowGemsReward(rewardedGems);
                MenuReference.THIS.GemsShop.GetComponent<AnimationEventManager>().CloseMenu();
            }
            else if (currentReward == RewardsType.GetLifes)
            {
                reward.SetIconSprite(1);
                reward.gameObject.SetActive(true);
                RestoreLifes();
                MenuReference.THIS.LiveShop.GetComponent<AnimationEventManager>().CloseMenu();
            }
            else if (currentReward == RewardsType.GetGoOn)
            {
                MenuReference.THIS.PreFailed.GetComponent<AnimationEventManager>().GoOnFailed();
            }

        }

        public void ShowGemsReward(int amount)
        {
            var reward = MenuReference.THIS.Reward.GetComponent<RewardIcon>();
            reward.SetIconSprite(0);
            reward.gameObject.SetActive(true);
            AddGems(amount);
        }


        public void SetGems(int count)
        {
            Gems = count;
            PlayerPrefs.SetInt("Gems", Gems);
            PlayerPrefs.Save();
        }


        public void AddGems(int count)
        {
            //ApiManager.instance.CreditCoin(GameData.userID, count, "Added " + count + "Coins.");
            Gems += count;
			//PlayerPrefs.SetInt("Gems", Gems);
			//PlayerPrefs.Save();
#if PLAYFAB || GAMESPARKS
            NetworkManager.currencyManager.IncBalance(count);
#endif

		}

        public void SpendGems(int count)
        {
            //ApiManager.instance.DebitCoin(GameData.userID, count, "Removed coins " + count);
            SoundBase.Instance.PlayOneShot(SoundBase.Instance.cash);
            Gems -= count;
            PlayerPrefs.SetInt("Gems", Gems);
            PlayerPrefs.Save();
#if PLAYFAB || GAMESPARKS
            NetworkManager.currencyManager.DecBalance(count);
#endif

        }


        public void RestoreLifes()
        {
            lifes = CapOfLife;
            ////ApiManager.instance.AddPowerUp(GameData.userID, 2, CapOfLife);
            //ApiManager.instance.AddPowerUp(GameData.userID, 2, lifes);
            //PlayerPrefs.SetInt("Lifes", lifes);
            //PlayerPrefs.Save();
        }

        public void AddLife(int count)
        {
            lifes += count;
            if (lifes > CapOfLife)
                lifes = CapOfLife;
            //ApiManager.instance.AddPowerUp(GameData.userID, 2, lifes);
            PlayerPrefs.SetInt("Lifes", lifes);
            PlayerPrefs.Save();
        }

        public int GetLife()
        {
            if (lifes > CapOfLife)
            {

                lifes = CapOfLife;
                //ApiManager.instance.AddPowerUp(GameData.userID, 2, lifes);
                PlayerPrefs.SetInt("Lifes", lifes);
                PlayerPrefs.Save();
            }

            return lifes;
        }

        public void PurchaseSucceded()
        {
            SoundBase.Instance.PlayOneShot(SoundBase.Instance.cash);
            AddGems(waitedPurchaseGems);
            waitedPurchaseGems = 0;
        }

        public void SpendLife(int count)
        {
            if (lifes > 0)
            {
                lifes -= count;

				//ApiManager.instance.RemovePowerUp(GameData.userID, 2, lifes);
				////ApiManager.instance.RemovePowerUp(1, 2, lifes);

				PlayerPrefs.SetInt("Lifes", lifes);
                PlayerPrefs.Save();
            }

            //else
            //{
            //    GameObject.Find("Canvas").transform.Find("RestoreLifes").gameObject.SetActive(true);
            //}
        }

        public void BuyBoost(BoostType boostType, int price, int count)
        {

            //int playerID = 1;
			int playerID = GameData.userID;

			switch (boostType)
			{

                case BoostType.ExtraMoves:
                    //ApiManager.instance.AddPowerUp(playerID, 3 , count);
                    break;

                case BoostType.ExtraTime:
                    //ApiManager.instance.AddPowerUp(playerID, 4 , count);
                    break;

                case BoostType.Stripes:
                    //ApiManager.instance.AddPowerUp(playerID, 5 , count);
                    break;

                case BoostType.Bomb:
                    //ApiManager.instance.AddPowerUp(playerID, 6 , count);
                    break;
                    
                case BoostType.MulticolorCandy:
                    //ApiManager.instance.AddPowerUp(playerID, 7 , count);
                    break;
                     
                case BoostType.FreeMove:
                    //ApiManager.instance.AddPowerUp(playerID, 8 , count);
                    break;

                case BoostType.ExplodeArea:
                    //ApiManager.instance.AddPowerUp(playerID, 9 , count);
                    break;

                case BoostType.Packages:
                    //ApiManager.instance.AddPowerUp(playerID, 10 , count);
                    break;

                case BoostType.Marmalade:
                    //ApiManager.instance.AddPowerUp(playerID, 11 , count);
                    break;

                case BoostType.None:
                    //ApiManager.instance.AddPowerUp(playerID, 12 , count);
                    break;

			}

            ////ApiManager.instance.AddPowerUp(GameData.userID, PlayerPrefs.GetInt("" + boostType), count);
            PlayerPrefs.SetInt("" + boostType, PlayerPrefs.GetInt("" + boostType) + count);
            PlayerPrefs.Save();
#if PLAYFAB || GAMESPARKS
            NetworkManager.dataManager.SetBoosterData();
#endif
        }

        public void SpendBoost(BoostType boostType)
        {
            int count = 1;
            //int playerID = 1;
			int playerID = GameData.userID;

			switch (boostType)
            {
                case BoostType.ExtraMoves:
                    Achievements.extraMovesUsed += 1;
                    PlayerPrefs.SetInt("extraMovesUsed5X", Achievements.extraMovesUsed);
                    AchievementManager.instance.checkExtraMovesAchivement(Achievements.extraMovesUsed);
                    //ApiManager.instance.RemovePowerUp(playerID, 3, count);
                    break;

                case BoostType.ExtraTime:
                    Achievements.extraTimeUsed += 1;
                    PlayerPrefs.SetInt("extraTimeUsed5X", Achievements.extraTimeUsed);
                    AchievementManager.instance.checkExtraTimeAchivement(Achievements.extraTimeUsed);
                    //ApiManager.instance.RemovePowerUp(playerID, 4, count);
                    break;

                case BoostType.Stripes:
                    //ApiManager.instance.RemovePowerUp(playerID, 5, count);
                    break;

                case BoostType.Bomb:
                    //ApiManager.instance.RemovePowerUp(playerID, 6, count);
                    break;

                case BoostType.MulticolorCandy:
                    Achievements.multicolorCandyUsed += 1;
                    PlayerPrefs.SetInt("multicolorCandyUsed5X", Achievements.multicolorCandyUsed);
                    AchievementManager.instance.checkMultiColorAchivement(Achievements.multicolorCandyUsed);
                    //ApiManager.instance.RemovePowerUp(playerID, 7, count);
                    break;

                case BoostType.FreeMove:
                    Achievements.timesFreeMoveUsed += 1;
                    PlayerPrefs.SetInt("timesFreeMoveUsed5X", Achievements.timesFreeMoveUsed);
                    AchievementManager.instance.checkFreeMoveAchivement(Achievements.timesFreeMoveUsed);
                    //ApiManager.instance.RemovePowerUp(playerID, 8, count);
                    break;

                case BoostType.ExplodeArea:
                    //ApiManager.instance.RemovePowerUp(playerID, 9, count);
                    break;

                case BoostType.Packages:
                    //ApiManager.instance.RemovePowerUp(playerID, 10, count);
                    break;

                case BoostType.Marmalade:
                    //ApiManager.instance.RemovePowerUp(playerID, 11, count);
                    break;

                case BoostType.None:
                    //ApiManager.instance.RemovePowerUp(playerID, 12, count);
                    break;

            }

            PlayerPrefs.SetInt("" + boostType, PlayerPrefs.GetInt("" + boostType) - 1);
            PlayerPrefs.Save();
#if PLAYFAB || GAMESPARKS
            NetworkManager.dataManager.SetBoosterData();
#endif
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                if (RestLifeTimer > 0)
                {
                    PlayerPrefs.SetFloat("RestLifeTimer", RestLifeTimer);
                }

                PlayerPrefs.SetInt("Lifes", lifes);
                PlayerPrefs.SetString("DateOfExit", ServerTime.THIS.serverTime.ToString());
                PlayerPrefs.Save();
            }
        }

        void OnApplicationQuit()
        {
            //1.4  added 
            if (RestLifeTimer > 0)
            {
                PlayerPrefs.SetFloat("RestLifeTimer", RestLifeTimer);
            }

            PlayerPrefs.SetInt("Lifes", lifes);
            PlayerPrefs.SetString("DateOfExit", ServerTime.THIS.serverTime.ToString());
            PlayerPrefs.Save();
            //print(RestLifeTimer)
        }

        public void OnLevelClicked(object sender, LevelReachedEventArgs args)
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))
                return;
            if (!GameObject.Find("CanvasGlobal").transform.Find("MenuPlay").gameObject.activeSelf &&
                !GameObject.Find("CanvasGlobal").transform.Find("GemsShop").gameObject.activeSelf &&
                !GameObject.Find("CanvasGlobal").transform.Find("LiveShop").gameObject.activeSelf)
            {
                SoundBase.Instance.PlayOneShot(SoundBase.Instance.click);
                OpenMenuPlay(args.Number);
            }
        }

        public static void OpenMenuPlay(int num)
        {

            PlayerPrefs.SetInt("OpenLevel", num);
            PlayerPrefs.Save();
            LevelManager.THIS.MenuPlayEvent();
            LevelManager.THIS.LoadLevel();
            openLevel = num;
            CrosssceneData.openNextLevel = false;
            MenuReference.THIS.MenuPlay.gameObject.SetActive(true);
        }

        void OnEnable()
        {
            LevelsMap.LevelSelected += OnLevelClicked;
            LevelsMap.OnLevelReached += OnLevelReached;

        }

        void OnDisable()
        {
            LevelsMap.LevelSelected -= OnLevelClicked;
            LevelsMap.OnLevelReached -= OnLevelReached;

            if(RestLifeTimer>0){
            PlayerPrefs.SetFloat("RestLifeTimer", RestLifeTimer);
            }
            PlayerPrefs.SetInt("Lifes", lifes);
            PlayerPrefs.SetString("DateOfExit", ServerTime.THIS.serverTime.ToString());
            PlayerPrefs.Save();

        }

        void OnLevelReached()
        {
            var num = PlayerPrefs.GetInt("OpenLevel");
            if (CrosssceneData.openNextLevel && CrosssceneData.totalLevels >= num)
            {
                OpenMenuPlay(num);
            }

            Achievements.scoreAchievement += PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("OpenLevel"));
            AchievementManager.instance.checkLevelAchievement(num);

        }
        
        public void delayedCall(float sec, Action action)
        {
            StartCoroutine(DelayedCallCor(sec, action));
        }

        IEnumerator DelayedCallCor(float sec, Action action)
        {
            yield return new WaitForSeconds(sec);
            action?.Invoke();
        }
    }

    /// moves or time is level limit type
    public enum LIMIT
    {
        MOVES,
        TIME
    }

    /// reward type for rewarded ads watching
    public enum RewardsType
    {
        GetLifes,
        GetGems,
        GetGoOn,
        FreeAction
    }
}
