using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Otp : MonoBehaviour
{
    [SerializeField] private TMP_InputField otp;
    [SerializeField] private GameObject invalidText;
    [SerializeField] private Button verify;
    [SerializeField] private Button resend;
    [SerializeField] private Button back;
}