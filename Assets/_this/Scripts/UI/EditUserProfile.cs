using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditUserProfile : MonoBehaviour
{
    [SerializeField] private Image pic;
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField number;
    [SerializeField] private TMP_InputField dob;
    [SerializeField] private TMP_InputField location;
    [SerializeField] private ToggleGroup gender;
    [SerializeField] private Button backBtt;
    [SerializeField] private Button editPic;
    [SerializeField] private Button done;

    private void OnEnable()
    {
        done.onClick.AddListener(OnDone);
        editPic.onClick.AddListener(OnEditPic);
        backBtt.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        done.onClick.RemoveListener(OnDone);
        editPic.onClick.RemoveListener(OnEditPic);
        backBtt.onClick.RemoveListener(OnBack);
    }

    private void OnBack()
    {
        UIManager.LoadScreenAnimated(UIScreen.Home);
    }

    private void OnDone()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserProfile);
    }

    private void OnEditPic()
    {
        //call file manager to load file
    }
}