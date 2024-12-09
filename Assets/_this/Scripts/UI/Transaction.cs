using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private Button currentlySelectedButton;

    private void Start()
    {
        SelectButton(earnedPoint);
    }

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        earnedPoint.onClick.AddListener(OnEarnedPoint);
        gamePoint.onClick.AddListener(OnGamePoint);
        back.onClick.AddListener(OnBack);

        // Explicitly select earnedPoint at the start
        SelectButton(earnedPoint);

        // Adjust slider position to reflect earnedPoint's selection
        slider.transform.localPosition = new Vector3(positions.x, -206.1927f);
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
            SelectButton(gamePoint);
        }
        else
        {
            SelectButton(earnedPoint);
        }
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnEarnedPoint()
    {
        Slide(positions.x);
        SelectButton(earnedPoint);
    }

    private void OnGamePoint()
    {
        Slide(positions.y);
        SelectButton(gamePoint);
    }

    private void Slide(float pos)
    {
        slider.transform.DOLocalMove(new Vector3(pos, -206.1927f), 0.3f)
            .SetEase(Ease.OutExpo);
    }

    private void SelectButton(Button button)
    {
        currentlySelectedButton = button;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    private void Update()
    {
        // If no button is selected, reselect the currently selected button
        if (EventSystem.current.currentSelectedGameObject == null && currentlySelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(currentlySelectedButton.gameObject);
        }
    }
}
