using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorFreer : MonoBehaviour
{
    //[SerializeField] private GameObject closeButton;

    private void OnEnable()
    {
        //closeButton.SetActive(true);
        GameController.GC.EnableCursor();
    }

    private void OnDisable()
    {
        //closeButton.SetActive(false);
        GameController.GC.DisableCursor();
    }
}
