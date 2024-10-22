﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class Profile
{
    public static event Action OnProfileLoaded;
    public static void GetProfile()
    {
        ApiManager.Get<ProfileResponseData>(ServiceURLs.GetProfile, OnSuccessGetProfile, OnErrorGetProfile);
    }

    private static void OnSuccessGetProfile(ProfileResponseData obj)
    {
        if (obj.status)
        {
            var token = UserData.GetData(UserDataSet.Token);
            UserData.SetTotalData(obj.data);
            UserData.SetData(UserDataSet.Token, token);
            CustomLog.SuccessLog("UserData Updated");
            GetSetPic();
        }
        else
        {
            CustomLog.ErrorLog(obj.message);
        }
    }

    private static void OnErrorGetProfile(string obj)
    {
        CustomLog.ErrorLog(obj);
    }

    private static void GetSetPic()
    {
        if (UserData.GetData(UserDataSet.ProfileImage) != null)
        {
            ApiManager.GetImage(ServiceURLs.Image + UserData.GetData(UserDataSet.ProfileImage), OnGetPicSuccess,
                OnGetPicError);
        }
        else
        {
            CustomLog.ErrorLog("Image url is null");
        }
    }

    private static void OnGetPicSuccess(Texture2D obj)
    {
        if (obj != null)
        {
            var image = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height), new Vector2(0.5f, 0.5f));
            UserData.SetImage(image);
            OnProfileLoaded?.Invoke();
        }
        else
        {
            CustomLog.ErrorLog("Image is Null");
        }
    }

    private static void OnGetPicError(string obj)
    {
        CustomLog.ErrorLog(obj);
    }
}