using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIgnoreFog : MonoBehaviour
{
    private bool isFoggy;

    void Start()
    {
        isFoggy = RenderSettings.fog;
    }

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }
    private void OnPostRender()
    {
        RenderSettings.fog = isFoggy;
    }
}
