﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RateUs : MonoBehaviour
{
    [SerializeField] private Button[] startBttArray; // Array of star buttons
    [SerializeField] private TMP_InputField feedback;
    [SerializeField] private Button submit;
    [SerializeField] private Button back;

    private void OnEnable()
    {
        UIManager._onbackbuttonpressed += OnBack;
        back.onClick.AddListener(OnBack);
        submit.onClick.AddListener(OnSubmit);

        // Assign click listeners to all star buttons
        for (int i = 0; i < startBttArray.Length; i++)
        {
            int index = i; // Capture the index
            startBttArray[i].onClick.AddListener(() => OnStarClick(index));
        }
        
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
        submit.onClick.RemoveListener(OnSubmit);

        // Remove listeners for all star buttons
        for (int i = 0; i < startBttArray.Length; i++)
        {
            startBttArray[i].onClick.RemoveListener(() => OnStarClick(i));
        }

        Reset();
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnSubmit()
    {
        // Play Store or App Store in-app rate API
        
        PopUpManager.ShowPopUpAction("Message", "Your rating and review have been submitted successfully.",OnBack);
    }

    private void OnStarClick(int index)
    {
        // Highlight stars up to the clicked button
        for (int i = 0; i < startBttArray.Length; i++)
        {
            GameObject star = startBttArray[i].transform.GetChild(0).gameObject; // Assuming the star is the first child
            star.SetActive(i <= index); // Turn on/off star based on the clicked index
        }
    }
    public void Reset()
    {
        // Reset all stars
        /*foreach (Button starButton in startBttArray)
        {
            GameObject star = starButton.transform.GetChild(0).gameObject; // Assuming the star is the first child
            star.SetActive(true); // De-highlight all stars
        }*/

        // Clear the feedback input field
        feedback.text = string.Empty; // Clear the text of the feedback field
    }
}