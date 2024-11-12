using SweetSugar.Scripts.MapScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BetScreen : MonoBehaviour
{
    [SerializeField] private Button plus;
    [SerializeField] private Button minus;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button play;
    [SerializeField] private StaticMapPlay staticMapPlay;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private int _count = 0;
    [SerializeField] private int _countmultiplyer = 10;

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
        _count += +_countmultiplyer;
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
    private void OnPlayClick()
    {
        if (_count > 0)
        {
            staticMapPlay.Play();
            mainCanvas.SetActive(false);
        }
    }
}
