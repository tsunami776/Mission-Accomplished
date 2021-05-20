using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsuffientResourceWindow : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.GC.player.GetComponent<PlayerMovementController>().Lock();
        GameController.GC.player.GetComponent<PlayerLookController>().Lock();
    }

    private void OnDisable()
    {
        GameController.GC.player.GetComponent<PlayerMovementController>().Unlock();
        GameController.GC.player.GetComponent<PlayerLookController>().Unlock();
    }
}
