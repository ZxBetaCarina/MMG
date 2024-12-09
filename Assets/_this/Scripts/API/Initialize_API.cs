using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize_API : MonoBehaviour
{
    private static Initialize_API instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ApiManager.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
