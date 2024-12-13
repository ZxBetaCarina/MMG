using System;
using SweetSugar.Scripts.MapScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BetScreen : MonoBehaviour
{
    public static BetScreen instance;
    
    [SerializeField] private Button plus;
    [SerializeField] private Button minus;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button play;
    [SerializeField] private StaticMapPlay staticMapPlay;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] public int _count = 0;
    [SerializeField] private int _countmultiplyer = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        plus.onClick.AddListener(OnPlusClick);
        minus.onClick.AddListener(OnMinusClick);
        play.onClick.AddListener(OnPlayClick);
        UpdateTextField();
    }
    private void UpdateTextField()
    {
        text.text = _count.ToString();
    }

    private void OnDisable()
    {
        plus.onClick.RemoveListener(OnPlusClick);
        minus.onClick.RemoveListener(OnMinusClick);
        play.onClick.RemoveListener(OnPlayClick);
    }
    private void OnPlusClick()
    {
        // Get the totalPoints from the TotalPoints singleton
        float totalPoints = TotalPoints.instance.gamePoints;
        int bonusPoints = TotalPoints.instance.BonusPoints;

        // Increase _count, but ensure it doesn't exceed totalPoints
        if (_count + _countmultiplyer <= totalPoints + bonusPoints)
        {
            _count += _countmultiplyer;
        }
        else
        {
            _count = (int)totalPoints; // Set _count to the totalPoints if adding would exceed it
        }

        UpdateTextField();
    }
    private void OnMinusClick()
    {
        if (_count > 0)
        {
            _count += -_countmultiplyer;
        }
        UpdateTextField();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            staticMapPlay.Play();
            mainCanvas.SetActive(false);
        }
    }

    private void OnPlayClick()
    {
        int totalPoints = TotalPoints.instance.gamePoints;
        int bonusPoints = TotalPoints.instance.BonusPoints;

        // Check if _count is greater than 0 and less than or equal to total points (bonus + game points)
        if (_count > 0 && _count <= totalPoints + bonusPoints)
        {
            // Deduct points first from BonusPoints
            if (_count <= bonusPoints)
            {
                TotalPoints.instance.SetBonusPoints(bonusPoints - _count);
                TotalPoints.instance.UpdateWalletPoints();
            }
            else
            {
                int remainingPointsToDeduct = _count - bonusPoints;
                TotalPoints.instance.SetBonusPoints(0);
                TotalPoints.instance.SetGamePoints(totalPoints - remainingPointsToDeduct);
                TotalPoints.instance.UpdateWalletPoints();
            }

            // Play the game
            staticMapPlay.Play();
            mainCanvas.SetActive(false);
        }
        else
        {
            // Check if both _count and totalPoints are 0
            if (_count == 0 && totalPoints == 0)
            {
                PopUpManager.ShowPopUp("Message", "Insufficient points!");
                Debug.Log("Insufficient points!");
            }
            // Check if _count is greater than 0 but more than totalPoints
            else if (_count > 0)
            {
                PopUpManager.ShowPopUp("Message", "Insufficient points!");
                Debug.Log("Insufficient points!");
            }
            // Check if _count is 0 or less
            else if (_count <= 0)
            {
                PopUpManager.ShowPopUp("Message", "Must enter a quantity");
                Debug.Log("Must enter a quantity");
            }
        }
    }
}
