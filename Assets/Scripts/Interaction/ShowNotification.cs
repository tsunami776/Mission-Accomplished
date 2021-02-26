using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNotification : MonoBehaviour
{
    public GameObject notificationText;

    // Show the notification text
    public void ShowText ()
    {
        notificationText.SetActive(true);
    }

    // Hide the notification text
    public void HideText ()
    {
        notificationText.SetActive(false);
    }
}
