using System;
using System.Collections;
using System.Collections.Generic;
using HandyButtons;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ApiManager : MonoBehaviour
{
    private static MonoBehaviour _coroutineRunner;
    private static string _authToken;

    #region ApiTester

    [Header("API Tester Settings")]
    [SerializeField] private string url; // URL input

    [SerializeField] private bool isPost; // Toggle between GET and POST
    [SerializeField] private List<RequestData> requestData = new List<RequestData>(); // Request data list

    [Serializable]
    public class RequestData
    {
        public string key; // Data key (parameter name)
        public string value; // Data value (parameter value)
    }

    [Button]
    public void SendRequest()
    {
        Initialize(this);
        // Check if it's a GET or POST request
        if (isPost)
        {
            // Build request data dictionary from the inspector input
            var dataDict = new Dictionary<string, string>();
            foreach (var data in requestData)
            {
                dataDict.Add(data.key, data.value);
            }

            Post(url, dataDict);
        }
        else
        {
            Get(url);
        }
    }

    private void Get(string url)
    {
        Get<object>(url, OnSuccess, OnError);
    }

    // POST Request Method
    private void Post(string url, Dictionary<string, string> data)
    {
        Post<Dictionary<string, string>, object>(url, data, OnSuccess, OnError);
    }

    // Success Callback
    private void OnSuccess(object response)
    {
        Debug.Log("Response: " + JsonConvert.SerializeObject(response, Formatting.Indented));
    }

    // Error Callback
    private void OnError(string error)
    {
        Debug.LogError("Error: " + error);
    }

    #endregion

    #region PublicMethods

    public static void Initialize(MonoBehaviour runner)
    {
        _coroutineRunner = runner;
    }

    public static void SetAuthToken(string token)
    {
        _authToken = token;
    }

    public static void Get<T>(string url, Action<T> onSuccess, Action<string> onError)
    {
        _coroutineRunner.StartCoroutine(GetRequest(url, onSuccess, onError));
    }

    public static void Post<Req, Res>(string url, Req requestData, Action<Res> onSuccess, Action<string> onError)
    {
        _coroutineRunner.StartCoroutine(PostRequest(url, requestData, onSuccess, onError));
    }

    public static void PostForm<T>(string url, WWWForm form, Action<T> onSuccess, Action<string> onError)
    {
        _coroutineRunner.StartCoroutine(PostFormRequest(url, form, onSuccess, onError));
    }

    public static void GetImage(string url, Action<Texture2D> onSuccess, Action<string> onError)
    {
        _coroutineRunner.StartCoroutine(GetImageRequest(url, onSuccess, onError));
    }

    #endregion

    #region Coroutines

    private static IEnumerator GetRequest<T>(string url, Action<T> onSuccess, Action<string> onError)
    {
        UIManager.ShowLoading(true);
        using UnityWebRequest request = UnityWebRequest.Get(url);
        AuthSetter(request);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                onSuccess?.Invoke(result);
                UIManager.ShowLoading(false);
            }
            catch (Exception e)
            {
                onError?.Invoke($"JSON parsing error: {e.Message}");
                UIManager.ShowLoading(false);
            }
        }
        else
        {
            onError?.Invoke(request.error);
            UIManager.ShowLoading(false);
        }
    }

    private static IEnumerator PostRequest<Req, Res>(string url, Req requestData, Action<Res> onSuccess,
        Action<string> onError)
    {
        UIManager.ShowLoading(true);
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        AuthSetter(request);
        string jsonToSend = JsonConvert.SerializeObject(requestData);
        byte[] jsonToSendBytes = new System.Text.UTF8Encoding().GetBytes(jsonToSend);
        request.uploadHandler = new UploadHandlerRaw(jsonToSendBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                Res result = JsonConvert.DeserializeObject<Res>(request.downloadHandler.text);
                onSuccess?.Invoke(result);
                UIManager.ShowLoading(false);
            }
            catch (Exception e)
            {
                onError?.Invoke($"JSON parsing error: {e.Message}");
                UIManager.ShowLoading(false);
            }
        }
        else
        {
            onError?.Invoke(request.error);
            UIManager.ShowLoading(false);
        }
    }

    private static IEnumerator PostFormRequest<T>(string url, WWWForm form, Action<T> onSuccess, Action<string> onError)
    {
        UIManager.ShowLoading(true);
        using UnityWebRequest request = UnityWebRequest.Post(url, form);
        AuthSetter(request);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                onSuccess?.Invoke(result);
                UIManager.ShowLoading(false);
            }
            catch (Exception e)
            {
                onError?.Invoke($"JSON parsing error: {e.Message}");
                UIManager.ShowLoading(false);
            }
        }
        else
        {
            onError?.Invoke(request.error);
            UIManager.ShowLoading(false);
        }
    }

    private static IEnumerator GetImageRequest(string url, Action<Texture2D> onSuccess, Action<string> onError)
    {
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        AuthSetter(request);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(DownloadHandlerTexture.GetContent(request));
        }
        else
        {
            onError?.Invoke(request.error);
        }
    }

    #endregion

    private static void AuthSetter(UnityWebRequest request)
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            request.SetRequestHeader("Authorization", $"Bearer {_authToken}");
        }
    }
}