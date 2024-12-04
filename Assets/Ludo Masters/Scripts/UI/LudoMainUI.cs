using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LudoMainUI : MonoBehaviour
{
    [SerializeField] private Button Back;
    
    private void OnEnable()
    {
        Back.onClick.AddListener(OnBack);
    }
    private void OnDisable()
    {
        Back.onClick.RemoveListener(OnBack);
    }
    public void ActivateLudoScreen()
    {
        UIManager.LoadScreenAnimated(UIScreen.LudoBetUI);
    }
    public void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }
}