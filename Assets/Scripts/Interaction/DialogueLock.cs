using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLock : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    public void DLock() {
        player.GetComponent<PlayerMovementController>().Lock();
        player.GetComponent<PlayerLookController>().Lock();
    }

    public void DUnlock()
    {
        player.GetComponent<PlayerMovementController>().Unlock();
        player.GetComponent<PlayerLookController>().Unlock();
    }
}
