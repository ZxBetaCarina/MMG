using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refer : MonoBehaviour
{
    [SerializeField] private TMP_Text earningPoints;
    [SerializeField] private TMP_Text referCode;
    [SerializeField] private Button copy;
    
    private string referralCode;
    
    private void Start()
    {
        GenerateReferralCode(); // Generate the referral code on startup
        UpdateUI();
    }
    private void OnEnable()
    {
        copy.onClick.AddListener(OnCopy);
    }

    private void OnDisable()
    {
        copy.onClick.RemoveListener(OnCopy);
    }
    private void GenerateReferralCode()
    {
        const int codeLength = 8;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        System.Text.StringBuilder sb = new System.Text.StringBuilder(codeLength);
        for (int i = 0; i < codeLength; i++)
        {
            sb.Append(chars[Random.Range(0, chars.Length)]);
        }

        referralCode = sb.ToString();
    }
    private void UpdateUI()
    {
        referCode.text = referralCode; // Display the generated referral code
    }
    private void OnCopy()
    {
        GUIUtility.systemCopyBuffer = referralCode;
        Debug.Log("Referral code copied to clipboard: " + referralCode);
    }
}