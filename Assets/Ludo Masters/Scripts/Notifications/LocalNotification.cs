using System;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using System.Collections.Generic;

public class LocalNotification
{
    // NEW IOS NOTIFICATION CODE 
    #if UNITY_ANDROID && !UNITY_EDITOR
    private static string fullClassName = "net.agasper.unitynotification.UnityNotificationManager";
#endif


    public static int SendNotification(TimeSpan delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        int id = new System.Random().Next();
        return SendNotification(id, (int)delay.TotalSeconds * 1000, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendNotification(int id, TimeSpan delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        return SendNotification(id, (int)delay.TotalSeconds * 1000, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendNotification(int id, long delayMs, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null)
        {
            pluginClass.CallStatic("SetNotification", id, delayMs, title, message, message, 
                sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, "notify_icon_small", 
                bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, Application.identifier);
        }
        return id;
#elif UNITY_IOS && !UNITY_EDITOR
        
        // Register for notifications (iOS specific)
        var request = new UnityEngine.iOS.UserNotifications.NotificationRequest
        {
            Title = title,
            Body = message,
            Sound = sound ? "default" : null,
            Badge = 1,
            CategoryIdentifier = "category1",
            ThreadIdentifier = "thread1"
        };

        // Create the notification trigger
        var trigger = new UnityEngine.iOS.UserNotifications.TimeIntervalNotificationTrigger()
        {
            TimeInterval = delayMs / 1000.0, // seconds
            Repeats = false
        };

        // Schedule notification
        var notification = new UnityEngine.iOS.UserNotifications.NotificationContent()
        {
            Title = title,
            Body = message,
            Sound = sound ? "default" : null
        };

        // Schedule local notification
        UnityEngine.iOS.UserNotifications.NotificationCenter.ScheduleNotification(notification, trigger);

        return id;  // Returning the id as a simple response
#else
        return 0;
#endif
    }

    public static int SendRepeatingNotification(TimeSpan delay, TimeSpan timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        int id = new System.Random().Next();
        return SendRepeatingNotification(id, (int)delay.TotalSeconds * 1000, (int)timeout.TotalSeconds, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendRepeatingNotification(int id, TimeSpan delay, TimeSpan timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        return SendRepeatingNotification(id, (int)delay.TotalSeconds * 1000, (int)timeout.TotalSeconds, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendRepeatingNotification(int id, long delayMs, long timeoutMs, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null)
        {
            pluginClass.CallStatic("SetRepeatingNotification", id, delayMs, title, message, message, timeoutMs, 
                sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, "notify_icon_small", 
                bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, Application.identifier);
        }
        return id;
#elif UNITY_IOS && !UNITY_EDITOR
        
        var trigger = new UnityEngine.iOS.UserNotifications.TimeIntervalNotificationTrigger()
        {
            TimeInterval = delayMs / 1000.0, // seconds
            Repeats = true // Repeating every timeoutMs duration
        };

        var content = new UnityEngine.iOS.UserNotifications.NotificationContent()
        {
            Title = title,
            Body = message,
            Sound = sound ? "default" : null,
            Badge = 1,
            CategoryIdentifier = "category1",
            ThreadIdentifier = "thread1"
        };

        // Schedule notification
        UnityEngine.iOS.UserNotifications.NotificationCenter.ScheduleNotification(content, trigger);
        
        return id;
#else
        return 0;
#endif
    }

    public static void CancelNotification(int id)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null) {
            pluginClass.CallStatic("CancelPendingNotification", id);
        }
#endif

#if UNITY_IOS && !UNITY_EDITOR
        // Cancel iOS notification by ID
        var notifications = UnityEngine.iOS.UserNotifications.NotificationCenter.GetScheduledNotifications();
        foreach (var notification in notifications)
        {
            if (notification.Identifier == id.ToString()) // Check if IDs match
            {
                UnityEngine.iOS.UserNotifications.NotificationCenter.RemoveScheduledNotification(notification.Identifier);
            }
        }
#endif
    }

    public static void ClearNotifications()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null) {
            pluginClass.CallStatic("ClearShowingNotifications");
        }
#endif

#if UNITY_IOS && !UNITY_EDITOR
        // Clear all notifications on iOS
        UnityEngine.iOS.UserNotifications.NotificationCenter.RemoveAllScheduledNotifications();
#endif
    }

    
    
    
    
    
    
    
    
    ////OLD IOS NOTIFICATION CODE
    
    
    
    
/*#if UNITY_ANDROID && !UNITY_EDITOR
    private static string fullClassName = "net.agasper.unitynotification.UnityNotificationManager";
#endif


    public static int SendNotification(TimeSpan delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        int id = new System.Random().Next();
        return SendNotification(id, (int)delay.TotalSeconds * 1000, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendNotification(int id, TimeSpan delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        return SendNotification(id, (int)delay.TotalSeconds * 1000, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendNotification(int id, long delayMs, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null)
        {
            pluginClass.CallStatic("SetNotification", id, delayMs, title, message, message, 
                sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, "notify_icon_small", 
        bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, Application.identifier);
        }
        return id;
#elif UNITY_IOS && !UNITY_EDITOR
        
        UnityEngine.iOS.Notification.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | 
                            UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);

        UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
        DateTime now = DateTime.Now;
        DateTime fireDate = DateTime.Now.AddSeconds(delayMs / 1000);
        notification.fireDate = fireDate;
        notification.alertBody = message;
        notification.alertAction = title;
        notification.hasAction = false;

        UnityEngine.iOS.Notification.ScheduleLocalNotification(notification);

        return (int)fireDate.Ticks;
#else
        return 0;
#endif


    }

    public static int SendRepeatingNotification(TimeSpan delay, TimeSpan timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        int id = new System.Random().Next();
        return SendRepeatingNotification(id, (int)delay.TotalSeconds * 1000, (int)timeout.TotalSeconds, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendRepeatingNotification(int id, TimeSpan delay, TimeSpan timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
        return SendRepeatingNotification(id, (int)delay.TotalSeconds * 1000, (int)timeout.TotalSeconds, title, message, bgColor, sound, vibrate, lights, bigIcon);
    }

    public static int SendRepeatingNotification(int id, long delayMs, long timeoutMs, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null)
        {
            pluginClass.CallStatic("SetRepeatingNotification", id, delayMs, title, message, message, timeoutMs, 
                sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, "notify_icon_small", 
                bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, Application.identifier);
        }
        return id;
#elif UNITY_IOS && !UNITY_EDITOR
        throw new System.NotImplementedException();
#else
        return 0;
#endif
    }

    public static void CancelNotification(int id)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null) {
            pluginClass.CallStatic("CancelPendingNotification", id);
        }
#endif

#if UNITY_IOS && !UNITY_EDITOR
        foreach (UnityEngine.iOS.LocalNotification notif in UnityEngine.iOS.Notification.scheduledLocalNotifications) 
        { 
            if ((int)notif.fireDate.Ticks == id)
            {
                UnityEngine.iOS.Notification.CancelLocalNotification(notif);
            }
        }
#endif
    }

    public static void ClearNotifications()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null) {
            pluginClass.CallStatic("ClearShowingNotifications");
        }
#endif

#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.Notification.ClearLocalNotifications();
#endif
    }*/
}
