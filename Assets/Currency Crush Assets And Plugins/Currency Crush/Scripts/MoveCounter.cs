using System.Collections;
using System.Linq;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.Level;
using SweetSugar.Scripts.TargetScripts.TargetSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SweetSugar.Scripts.GUI
{
    /// <summary>
    /// various GUi counters
    /// </summary>
    public class MoveCounter : MonoBehaviour
    {
        TextMeshProUGUI txt;
        private float lastTime;
        bool alert;

        private LevelData _thisLevelData;

        public LevelData ThisLevelData
        {
            get
            {
                if (_thisLevelData == null) _thisLevelData = LevelData.THIS;
                return _thisLevelData;
            }
            set => _thisLevelData = value;
        }

        // Use this for initialization
        void Awake()
        {

            // txt = GetComponent<Text>();
            txt = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            ThisLevelData = LevelManager.THIS.levelData;

        }

        void OnEnable()
        {
            UpdateText();
            alert = false; StartCoroutine(UpdateRare());
        }

        // Update is called once per frame
        IEnumerator UpdateRare()
        {
            while (true)
            {
                if (txt == null) continue;

                UpdateText();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void UpdateText()
        {
            if (name == "Limit" && ThisLevelData != null)
            {
                if (ThisLevelData.limitType == LIMIT.MOVES)
                {
                    txt.text = "" + Mathf.Clamp(ThisLevelData.limit, 0, ThisLevelData.limit);
                    txt.transform.localScale = Vector3.one;
                    
                    if (Mathf.Clamp(ThisLevelData.limit, 0, ThisLevelData.limit) == 0)
                    {
                        ThisLevelData.limit = null;
                        SceneManager.LoadScene(0);
                        PopUpManager.ShowPopUp("Message", "No Moves Left Try Again Next Time");
                    }
                    
                    if (ThisLevelData.limit <= 5)
                    {
                        txt.color = new Color(255f / 255f, 132f / 255, 222f / 255);
                        txt.outlineColor = Color.white;
                        if (!alert)
                        {
                            alert = true;
//                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.alert);
                        }
                    }
                    else
                    {
                        alert = false;
                        txt.color = Color.white;
                        // txt.GetComponent<Outline>().effectColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
                else
                {
                    var minutes = Mathf.FloorToInt(ThisLevelData.limit / 60F);
                    var seconds = Mathf.FloorToInt(ThisLevelData.limit - minutes * 60);
                    txt.text = "" + $"{minutes:00}:{seconds:00}";
                    txt.transform.localScale = Vector3.one * 0.68f;
                    txt.fontSize = 80;
                    if (ThisLevelData.limit <= 5 && LevelManager.THIS.gameStatus == GameState.Playing)
                    {
                        // txt.color = new Color(216f / 255f, 0, 0);
                        // txt.outlineColor = Color.white;
                        if (lastTime + 5 < Time.time)
                        {
                            lastTime = Time.time;
                            SoundBase.Instance.PlayOneShot(SoundBase.Instance.timeOut);
                        }
                    }
                    else
                    {
                        txt.color = Color.white;
                        txt.outlineColor = new Color(148f / 255f, 61f / 255f, 95f / 255f);
                    }
                }
            }

            
        }
    }
}
