using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCloseButton : MonoBehaviour
{
    [SerializeField] private GameObject closeButton;

    private void OnEnable()
    {
        closeButton.SetActive(true);
    }

    private void OnDisable()
    {
        closeButton.SetActive(false);
    }
}
