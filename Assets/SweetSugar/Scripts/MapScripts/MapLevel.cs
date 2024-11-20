using SweetSugar.Scripts.GUI;
using SweetSugar.Scripts.System;
using UnityEngine;

namespace SweetSugar.Scripts.MapScripts
{
    public class MapLevel : MonoBehaviour
    {

        private Vector3 _originalScale;
        private bool _isScaled;
        public float OverScale = 1.05f;
        public float ClickScale = 0.95f;

        public int Number =2;
        public bool IsLocked;
        public Transform Lock;
        public Transform PathPivot;
        public Object LevelScene;
        public string SceneName;

        public int StarsCount;
        public Transform StarsHoster;
        public Transform Star1;
        public Transform Star2;
        public Transform Star3;

        public Transform SolidStarsHoster;
        public Transform SolidStars0;
        public Transform SolidStars1;
        public Transform SolidStars2;
        public Transform SolidStars3;
        public GameObject idleEffect;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

		void Start()
		{
            ////ApiManager.instance.callFetchMatchData(GameData.userID);
            initialData();
		}

        void initialData()
		{
            if (GameData.levels.ContainsKey(Number))
                if (GameData.levels[Number] != 0)
                {
                    //Debug.Log("\n" + GameData.levels[Number]);
                    if (GameData.levels.ContainsKey(Number))
                    {
                        IsLocked = false;
                        StarsCount = GameData.levels[Number];
                        UpdateStars(StarsCount);
                        LevelsMap.CompleteLevel(Number, StarsCount);
                    }
                }
        }


		//private void Update()
		//{
		//	if (Input.GetKeyDown(KeyCode.LeftArrow) && Number < 4)
		//	{
  //              Debug.Log(Number + " \t" + StarsCount + "\t" + GameData.levels[Number]);
		//	}
		//}


		#region Enable click

		public void OnMouseEnter()
        {
            if (LevelsMap.GetIsClickEnabled())
                Scale(OverScale);
        }

        public void OnMouseDown()
        {
            if (LevelsMap.GetIsClickEnabled())
                Scale(ClickScale);

        }

        public void OnMouseExit()
        {
            if (LevelsMap.GetIsClickEnabled())
                ResetScale();
        }

        private void Scale(float scaleValue)
        {
            transform.localScale = _originalScale * scaleValue;
            _isScaled = true;
        }

        public void OnDisable()
        {
            if (LevelsMap.GetIsClickEnabled())
                ResetScale();
        }

        public void OnMouseUpAsButton()
        {
            Debug.Log(LevelsMap.GetIsClickEnabled());
            if (LevelsMap.GetIsClickEnabled())
            {
                ResetScale();
                LevelsMap.OnLevelSelected(Number);

                //GameObject.Find("SettingsButton").SetActive(false);
				GameObject.Find("SettingsButton").LeanScaleX(0f, 0f);
			}
        }

        private void ResetScale()
        {
            if (_isScaled)
                transform.localScale = _originalScale;
        }

        #endregion

        public void UpdateState(int starsCount, bool isLocked)
        {
            StarsCount = starsCount;
            UpdateStars(isLocked ? 0 : starsCount);
            IsLocked = isLocked;
            //IsLocked = false;
            Lock.gameObject.SetActive(isLocked);
        }

        public void UpdateStars(int starsCount)
        {
            Star1?.gameObject.SetActive(starsCount >= 1);
            Star2?.gameObject.SetActive(starsCount >= 2);
            Star3?.gameObject.SetActive(starsCount >= 3);

            SolidStars0?.gameObject.SetActive(starsCount == 0);
            SolidStars1?.gameObject.SetActive(starsCount == 1);
            SolidStars2?.gameObject.SetActive(starsCount == 2);
            SolidStars3?.gameObject.SetActive(starsCount == 3);

            GameData.currentLevelStars = starsCount;

        }

        public void UpdateStarsType(StarsType starsType)
        {
            StarsHoster.gameObject.SetActive(starsType == StarsType.Separated);
            SolidStarsHoster.gameObject.SetActive(starsType == StarsType.Solid);
        }
        public void SetEffect()
        {
            FindObjectsOfType<IdleCircleMapEffect>().ForEachY(x=>Destroy(x.gameObject));
            var i = Instantiate(idleEffect, transform.position, Quaternion.identity, transform);
            i.transform.localScale = new Vector3(1.24f,1,1);
        }
    }
}




