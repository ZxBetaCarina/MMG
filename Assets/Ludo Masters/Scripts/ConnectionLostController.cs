using UnityEngine;
using System.Collections;

public class ConnectionLostController : MonoBehaviour {

    // Use this for initialization
    public GameObject canvas;
    private static ConnectionLostController _instance;

    void Start() {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
     
        GameManager.Instance.connectionLost = this;

        // if (Application.internetReachability == NetworkReachability.NotReachable) {
        //     showDialog();
        // }
    }

    // Update is called once per frame
    void Update() {

    }

    public void destroy() {
        if (this.gameObject != null)
            DestroyImmediate(this.gameObject);
    }

    public void showDialog() {
       // canvas.SetActive(true);
    }

    public void closeApp() {
        canvas.SetActive(false);
      //  Application.Quit();
      
    }
}
