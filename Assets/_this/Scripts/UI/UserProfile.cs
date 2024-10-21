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
        back.onClick.AddListener(OnBack);
        edit.onClick.AddListener(OnEdit);
        GetAllUserData();
    }

    private void GetAllUserData()
    {
        email.text = UserData.GetData(UserDataSet.Email);
        firstName.text = UserData.GetData(UserDataSet.FirstName);
        lastName.text = UserData.GetData(UserDataSet.LastName);
        number.text = UserData.GetData(UserDataSet.Number);
        dob.text = UserData.GetData(UserDataSet.Dob);
        location.text = UserData.GetData(UserDataSet.Location);
        gender.text = UserData.GetData(UserDataSet.Gender);
        GetSetPic();
    }

    private void GetSetPic()
    {
        if (UserData.GetData(UserDataSet.ProfileImage) != null)
        {
            ApiManager.GetImage(UserData.GetData(UserDataSet.ProfileImage), OnGetPicSuccess, OnGetPicError);
        }
        else
        {
            CustomLog.ErrorLog("Image url is null");
        }
    }

    private void OnGetPicSuccess(Texture2D obj)
    {
        if (obj != null)
        {
            pic.sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            CustomLog.ErrorLog("Image is Null");
        }
    }

    private void OnGetPicError(string obj)
    {
        CustomLog.ErrorLog(obj);
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