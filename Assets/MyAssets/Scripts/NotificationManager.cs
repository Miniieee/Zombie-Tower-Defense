using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "notify1",
            Name = "playreminder",
            Importance = Importance.High,
            Description = "Remind the player to play the game",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);


        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Zombies are attacking!",
            Text = "Zombie apocalypse has started, defend the survivors! You are the their only hope!",
            SmallIcon = "z_td_small",
            LargeIcon = "z_td_large",
            FireTime = System.DateTime.Now.AddHours(2),
        };

        AndroidNotificationCenter.SendNotification(notification, "notify1");
    }
}
