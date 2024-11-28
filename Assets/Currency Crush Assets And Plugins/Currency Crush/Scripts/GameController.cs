using System.Collections;
using System.Collections.Generic;
using HandyButtons;
using SweetSugar.Scripts.Core;
using SweetSugar.Scripts.TargetScripts.TargetSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float time;
    [SerializeField] private List<TargetCounter> targets;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += GetTarget;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= GetTarget;
    }

    private void GetTarget()
    {
        targets = LevelManager.THIS.levelData.TargetCounters;
        //time = LevelManager.THIS.levelData.limit;
        StartTimer();
    }

    private void Update()
    {
        if (targets.Count <= 0) return;
        if (targets[0].count == 0 && targets[1].count == 0 && targets[2].count == 0 && targets[3].count == 0)
        {
            AfterTargetAchieved();
        }
    }

    [Button]
    private void AfterTargetAchieved()
    {
        SceneManager.LoadScene(0);
        PopUpManager.ShowPopUp("Message", "Yey You Did It");
    }


    private void StartTimer()
    {
        time *= 60; // Convert minutes to seconds
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        while (time > 0)
        {
            time -= 1;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timeText.text = $"{minutes:00}:{seconds:00}"; // Display as "MM:SS"
            yield return new WaitForSeconds(1f);
        }

        time = 0;
        timeText.text = "00:00";
        OnTimeExpired();
    }

    [Button]
    private void OnTimeExpired()
    {
        SceneManager.LoadScene(0);
        PopUpManager.ShowPopUp("Message", "Time's Up Try Again Next Time");
    }

    private void AfterTimeUp()
    {
        SceneManager.LoadScene(0);
    }
    private void GetReward()
    {
        SceneManager.LoadScene(0);
        //todo give reward
    }
}