using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Transaction : MonoBehaviour
{
    [SerializeField] private Button earnedPoint;
    [SerializeField] private Button gamePoint;
    [SerializeField] private Button back;
    [SerializeField] private GameObject datePrefab;
    [SerializeField] private GameObject transactionBox;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject slider;
    [SerializeField] private Vector2 positions;

    [Header("Api References")]
    [SerializeField] private GameObject parent1;
    [SerializeField] private GameObject parent2;

    private void Start()
    {
        earnedPoint.Select();
    }

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        earnedPoint.onClick.AddListener(OnEarnedPoint);
        gamePoint.onClick.AddListener(OnGamePoint);
        back.onClick.AddListener(OnBack);
        ShowSelectedBtt();
    }


    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        earnedPoint.onClick.RemoveListener(OnEarnedPoint);
        gamePoint.onClick.RemoveListener(OnGamePoint);
        back.onClick.RemoveListener(OnBack);
    }

    private void ShowSelectedBtt()
    {
        if (slider.GetComponent<RectTransform>().position.x <= positions.x)
        {
            gamePoint.Select();
        }
        else
        {
            earnedPoint.Select();
        }
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnEarnedPoint()
    {
        Slide(positions.x);
    }

    private void OnGamePoint()
    {
        Slide(positions.y);
    }

    private void Slide(float pos)
    {
        slider.transform.DOLocalMove(new Vector3(pos, -206.1927f), 0.3f)
            .SetEase(Ease.OutExpo);
    }
}