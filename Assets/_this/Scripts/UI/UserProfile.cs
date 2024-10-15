using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private TMP_Text firstName;
    [SerializeField] private TMP_Text lastName;
    [SerializeField] private TMP_Text number;
    [SerializeField] private TMP_Text dob;
    [SerializeField] private TMP_Text location;
    [SerializeField] private TMP_Text gender;
    [SerializeField] private Button back;
    [SerializeField] private Button edit;

    private void OnEnable()
    {
        back.onClick.AddListener(OnBack);
        edit.onClick.AddListener(OnEdit);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(OnBack);
        edit.onClick.RemoveListener(OnEdit);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnEdit()
    {
        UIManager.LoadScreenAnimated(UIScreen.EditUserProfile);
    }
}