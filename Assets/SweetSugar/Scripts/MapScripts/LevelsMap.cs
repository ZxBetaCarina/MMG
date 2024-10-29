using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SweetSugar.Scripts.Level;
using SweetSugar.Scripts.System;
using UnityEngine;
using UnityEngine.UI;
using SweetSugar.Scripts.GUI;

namespace SweetSugar.Scripts.MapScripts
{
    public class LevelsMap : MonoBehaviour {
        public static LevelsMap _instance;
        private static IMapProgressManager _mapProgressManager = new PlayerPrefsMapProgressManager ();

        public bool IsGenerated;

        public MapLevel MapLevelPrefab;
        public Transform CharacterPrefab;
        public int Count = 10;

        public WaypointsMover WaypointsMover;
        public MapLevel CharacterLevel;
        public TranslationType TranslationType;

        public bool StarsEnabled;
        public StarsType StarsType;

        public bool ScrollingEnabled;
        public MapCamera MapCamera;

        public bool IsClickEnabled;
        public bool IsConfirmationEnabled;

        public void Awake () {
            _instance = this;
        }

        public void OnDestroy () {
            _instance = null;
        }

        public void OnEnable () {

            //initialLevels();

            if (IsGenerated) {
                Reset ();
            }
        }

        List<MapLevel> MapLevels = new List<MapLevel>();

        public List<MapLevel> GetMapLevels()
        {

            if (MapLevels.Count == 0)//1.4.4
                MapLevels = FindObjectsOfType<MapLevel>().OrderBy(ml => ml.Number).WhereNotNull().ToList();

            return MapLevels;
        }

        private void Start()
        {
            //OnLevelSelected(MusicBase.LevelToLoad);
            //GUIUtils.THIS.StartGame();
            //StartCoroutine(loadLevel(MusicBase.LevelToLoad));
            //temp.transform.GetChild(2).transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => AnimationEventManager.Instance.Play());
            //Invoke("_loadLevel", .5f);
        }

        IEnumerator loadLevel(int levelNumber)
        {
            //Debug.Log("Inside Enumerator");
            yield return new WaitForSeconds(.5f);
            //Debug.LogError("After 2 seconds");
            OnLevelSelected(MusicBase.LevelToLoad);
        }

        void _loadLevel()
        {
            OnLevelSelected(MusicBase.LevelToLoad);
            
        }

        public void Reset()
        {
            ////ApiManager.instance.callFetchMatchData(GameData.userID);
            UpdateMapLevels();
            PlaceCharacterToLastUnlockedLevel();
            int number = GetLatestReachedLevel();
            if (number > 1 && CrosssceneData.win)
                WalkToLevelInternal(number);
            else TeleportToLevelInternal(number,true);
            SetCameraToCharacter();
        }

        void initialLevels()
        {
            //ApiManager.instance.callFetchMatchData(GameData.userID);

            //foreach (MapLevel mapLevel in GetMapLevels())
            //{
            //    //ApiManager.instance.UpdateGameResult(GameData.userID, mapLevel.Number, mapLevel.StarsCount);
            //}
            //MapLevel.instance.onStart();

        }

        private void UpdateMapLevels()
        {
            foreach (MapLevel mapLevel in GetMapLevels())
            {
                //if(GameData.levels[mapLevel.Number] != 0)
                //    //ApiManager.instance.UpdateGameResult(GameData.userID, mapLevel.Number, mapLevel.StarsCount);
                mapLevel.UpdateState(
                    _mapProgressManager.LoadLevelStarsCount(mapLevel.Number),
                    IsLevelLocked(mapLevel.Number));
            }

        }

        private void PlaceCharacterToLastUnlockedLevel()
        {
            int lastUnlockedNumber = GetMapLevels().Where(l => !l.IsLocked).Select(l => l.Number).Max() - 1;
            lastUnlockedNumber = Mathf.Clamp(lastUnlockedNumber, 1, lastUnlockedNumber);
            TeleportToLevelInternal(lastUnlockedNumber, true);
        }

        public int GetLatestReachedLevel()
        {//1.3.3
            return GetMapLevels().Where(l => !l.IsLocked).Select(l => l.Number).Max();
        }

        private void SetCameraToCharacter()
        {
            MapCamera mapCamera = FindObjectOfType<MapCamera>();
            if (mapCamera != null)
                mapCamera.SetPosition(WaypointsMover.transform.position);
        }

        #region Events

        public static event EventHandler<LevelReachedEventArgs> LevelSelected;
        public static event EventHandler<LevelReachedEventArgs> LevelReached;

        #endregion

        #region Static API

        public static void CompleteLevel(int number)
        {
            CompleteLevelInternal(number, 1);
            //ApiManager.instance.UpdateGameResult(GameData.userID, number, 1);
        }

        public static void CompleteLevel(int number, int starsCount)
        {
            CompleteLevelInternal(number, starsCount);
            //ApiManager.instance.UpdateGameResult(GameData.userID, number, starsCount);
        }

        internal static void OnLevelSelected(int number)
        {
            Debug.Log(LevelSelected + ", " + IsLevelLocked(number));
            if (LevelSelected != null && !IsLevelLocked(number))  //need to fix in the map plugin
                LevelSelected(_instance, new LevelReachedEventArgs(number));

            Debug.Log(_instance.IsConfirmationEnabled);
            if (!_instance.IsConfirmationEnabled)
                GoToLevel(number);
        }

        public static void GoToLevel(int number)
        {

			//GameObject.Find("SettingsButton").SetActive(false);
			GameObject.Find("SettingsButton").LeanScaleX(0f, 0f);
			switch (_instance.TranslationType)
            {
                case TranslationType.Teleportation:
                    _instance.TeleportToLevelInternal(number, false);
                    break;
                case TranslationType.Walk:
                    _instance.WalkToLevelInternal(number);
                    break;
            }
            PlayerPrefs.SetInt("CurLevel", number);
            GameData.currentLevel = number;
        }

        public static bool IsLevelLocked(int number)
        {
			return number > 1 && _mapProgressManager.LoadLevelStarsCount(number - 1) == 0;
			//return false;
		}

        public static void OverrideMapProgressManager(IMapProgressManager mapProgressManager)
        {
            _mapProgressManager = mapProgressManager;
        }

        public static void ClearAllProgress()
        {
            _instance.ClearAllProgressInternal();
        }

        public static bool IsStarsEnabled()
        {
            return _instance.StarsEnabled;
        }

        public static bool GetIsClickEnabled()
        {
            return _instance.IsClickEnabled;
        }

        public static bool GetIsConfirmationEnabled()
        {
            return _instance.IsConfirmationEnabled;
        }

        #endregion

        private static void CompleteLevelInternal(int number, int starsCount)
        {
            //if (IsLevelLocked(number))
            //{
            //    Debug.Log(string.Format("Can't complete locked level {0}.", number));
            //}
            //else if (starsCount < 1 || starsCount > 3)
            //{
            //    Debug.Log(string.Format("Can't complete level {0}. Invalid stars count {1}.", number, starsCount));
            //}
            //else
            //{
                int curStarsCount = _mapProgressManager.LoadLevelStarsCount(number);
                int maxStarsCount = Mathf.Max(curStarsCount, starsCount);
                _mapProgressManager.SaveLevelStarsCount(number, maxStarsCount);

                if (_instance != null)
                    _instance.UpdateMapLevels();
            //}
        }

        private void TeleportToLevelInternal(int number, bool isQuietly)
        {
            MapLevel mapLevel = GetLevel(number);
            mapLevel.SetEffect();
            if (mapLevel.IsLocked)
            {
                Debug.Log(string.Format("Can't jump to locked level number {0}.", number));
            }
            else
            {
                WaypointsMover.transform.position = mapLevel.PathPivot.transform.position;   //need to fix in the map plugin
                CharacterLevel = mapLevel;
                if (!isQuietly)
                    RaiseLevelReached(number);
            }
            //PlayerPrefs.SetInt("CurLevel", number);
            //GameData.currentLevel = number;
        }
    
        public delegate void ReachedLevelEvent();
        public static ReachedLevelEvent OnLevelReached;

        private void WalkToLevelInternal(int number)
        {
            MapLevel mapLevel = GetLevel(number);
            mapLevel.SetEffect();
            CharacterLevel = GetLevel(number - 1);
            if (mapLevel.IsLocked)
            {
                Debug.Log(string.Format("Can't go to locked level number {0}.", number));
            }
            else
            {
                WaypointsMover.Move(CharacterLevel.PathPivot, mapLevel.PathPivot,
                    () =>
                    {
                        RaiseLevelReached(number);
                        CharacterLevel = mapLevel;
                        OnLevelReached?.Invoke();
                    });
            }

            //PlayerPrefs.SetInt("CurLevel", number);
            //GameData.currentLevel = number;

        }

        private void RaiseLevelReached(int number)
        {
            MapLevel mapLevel = GetLevel(number);
            mapLevel.SetEffect();
            if (!string.IsNullOrEmpty(mapLevel.SceneName))
                Application.LoadLevel(mapLevel.SceneName);

            if (LevelReached != null)
                LevelReached(this, new LevelReachedEventArgs(number));
        }

        public MapLevel GetLevel(int number)
        {
            return GetMapLevels().SingleOrDefault(ml => ml.Number == number);
        }

        private void ClearAllProgressInternal()
        {
            foreach (MapLevel mapLevel in GetMapLevels())
                _mapProgressManager.ClearLevelProgress(mapLevel.Number);
            Reset();
        }

        public void SetStarsEnabled(bool bEnabled)
        {
            StarsEnabled = bEnabled;
            int starsCount = 0;
            foreach (MapLevel mapLevel in GetMapLevels().WhereNotNull())
            {
                mapLevel.UpdateStars(starsCount);
                starsCount = (starsCount + 1) % 4;
                mapLevel.StarsHoster.gameObject.SetActive(bEnabled);
                mapLevel.SolidStarsHoster.gameObject.SetActive(bEnabled);
            }
        }

        public void SetStarsType(StarsType starsType)
        {
            StarsType = starsType;
            foreach (MapLevel mapLevel in GetMapLevels().WhereNotNull())
                mapLevel.UpdateStarsType(starsType);
        }

    }
}
