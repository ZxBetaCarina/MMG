using UnityEngine.SceneManagement;
using UnityEngine;

public class DecideLogin : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("LoginType"))
		{
            SceneManager.LoadScene("Game");
        }

		else
		{
          //  SceneManager.LoadScene("Login_Page");
		}
        
    }

}
