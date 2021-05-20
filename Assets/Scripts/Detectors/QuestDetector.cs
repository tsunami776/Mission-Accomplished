using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetector : MonoBehaviour
{
    [SerializeField] private int whichQuest;

    private void OnTriggerEnter(Collider other)
    {
        GameController.GC.currentQuest = whichQuest;
    }

    private void OnTriggerExit(Collider other)
    {
        GameController.GC.currentQuest = -1;
    }
}
