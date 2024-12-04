using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearData : MonoBehaviour
{
    public List<GameObject> pages=new List<GameObject>();
    public TMP_InputField[] inputFields;
//    public TMP_Text [] textFields;
    public Toggle[] toggles; // Make this static

    private void OnValidate()
    {
        // Clear previous references
        List<TMP_InputField> inputFieldList = new List<TMP_InputField>();
      //  List<TMP_Text> textFieldList = new List<TMP_Text>();
        List<Toggle> toggleList = new List<Toggle>();

        // Iterate through each GameObject in the pages list
        foreach (GameObject page in pages)
        {
            // Get components in children and add them to the lists
            inputFieldList.AddRange(page.GetComponentsInChildren<TMP_InputField>());
          //  textFieldList.AddRange(page.GetComponentsInChildren<TMP_Text>());
            toggleList.AddRange(page.GetComponentsInChildren<Toggle>());
        }

        // Convert lists to arrays
        inputFields = inputFieldList.ToArray();
      //  textFields = textFieldList.ToArray();
        toggles = toggleList.ToArray();
    }
    public void Clear()
    {
        // Clear input fields
        foreach (TMP_InputField inputField in inputFields)
        {
            inputField.text = "";
        }

        // Clear text fields
        // foreach (TMP_Text textField in textFields)
        // {
        //     textField.text = "";
        // }

        // Reset toggles
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }

}
