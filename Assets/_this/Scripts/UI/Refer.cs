using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refer : MonoBehaviour
{
    [SerializeField] private TMP_Text earningPoints;
    [SerializeField] private TMP_Text referCode;
    [SerializeField] private Button copy;
    
    public string referralCode;
    
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        UpdateUI();
        copy.onClick.AddListener(OnCopy);
    }

    private void OnDisable()
    {
        copy.onClick.RemoveListener(OnCopy);
    }
    private void UpdateUI()
    {
        referCode.text = UserData.GetData(UserDataSet.ReferNumber);
    }
    private void OnCopy()
    {
        GUIUtility.systemCopyBuffer = referralCode;
        Debug.Log("Referral code copied to clipboard: " + referralCode);
    }
}