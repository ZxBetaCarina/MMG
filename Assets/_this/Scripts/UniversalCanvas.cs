using System;
using UnityEngine;

public class UniversalCanvas : MonoBehaviour
{
    private static UniversalCanvas _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApiManager.Initialize(this);
    }
}
