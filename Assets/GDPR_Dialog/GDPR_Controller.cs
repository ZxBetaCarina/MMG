using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
public class GDPR_Controller : MonoBehaviour
{
    public string policyUrl;

    public GameObject CookiesDialog;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("GPDR_DIALOG", 0) == 0)
        {
            CookiesDialog.SetActive(true);
            PlayerPrefs.SetInt("GPDR_DIALOG", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openPolicy()
    {
        Application.OpenURL(policyUrl);
    }

    public void GDPRAccepted()
    {
        PlayerPrefs.SetInt(GDPR_Strings.playerPrefsKey, 1);
    }

    public void GDPRDeclined()
    {
        PlayerPrefs.SetInt(GDPR_Strings.playerPrefsKey, 0);
    }
}
