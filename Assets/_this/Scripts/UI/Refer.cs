using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refer : MonoBehaviour
{
    [SerializeField] private TMP_Text earningPoints;
    [SerializeField] private TMP_Text referCode;
    [SerializeField] private Button copy;
    
    
    private void OnEnable()
    {
        copy.onClick.AddListener(OnCopy);
    }

    private void OnDisable()
    {
        copy.onClick.RemoveListener(OnCopy);
    }
    private void OnCopy()
    {
        
    }
}