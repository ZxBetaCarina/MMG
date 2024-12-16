using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private Image pic;
    [SerializeField] private TMP_Text email;
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
        UIManager._onbackbuttonpressed += OnBack;
        GetAllUserData();
        back.onClick.AddListener(OnBack);
        edit.onClick.AddListener(OnEdit);
        Profile.OnProfileLoaded += GetAllUserData;
        Profile.GetProfile();
    }

    private void GetAllUserData()
    {
        email.text = UserData.GetData(UserDataSet.Email);
        firstName.text = UserData.GetData(UserDataSet.FirstName);
        lastName.text = UserData.GetData(UserDataSet.LastName);
        number.text = UserData.GetData(UserDataSet.Number);
        dob.text = UserData.GetData(UserDataSet.Dob);
        location.text = UserData.GetData(UserDataSet.Location);
        gender.text =ToFirstLetterUpperCase( UserData.GetData(UserDataSet.Gender));
        SetPic();
    }
    string ToFirstLetterUpperCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
    private void SetPic()
    {
        pic.sprite = UserData.GetImage();
    }

    private void OnDisable()
    {
        UIManager._onbackbuttonpressed -= OnBack;
        back.onClick.RemoveListener(OnBack);
        edit.onClick.RemoveListener(OnEdit);
        Profile.OnProfileLoaded -= GetAllUserData;
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