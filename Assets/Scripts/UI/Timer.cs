using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private RectTransform RectTF;
    private bool updateLock;
    private LightingManager LM;

    // Start 
    private void Start()
    {
        RectTF = GetComponent<RectTransform>();
        updateLock = true;
        LM = GameObject.Find("GameController").GetComponent<LightingManager>();
        StartCoroutine(UnlockUpdate());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // update time
        currentTime += Time.fixedDeltaTime;

        // rotate clock pointer
        float change = -Time.fixedDeltaTime * Config.MODIFIER_TIMER;
        RectTF.Rotate(0f, 0f, change * 2); // clock round is 12 hours

        // update the skybox and lightning corresponding to the current time
        LM.UpdateTime(change);

        // update date per round
        if (currentTime % Config.TIME_ONEDAY >= -0.1f && currentTime % Config.TIME_ONEDAY <= 0.1f && !updateLock)
        {
            //Debug.Log("Go Next Day");
            GameController.GC.GoToNextDay();
            updateLock = true;
            StartCoroutine(UnlockUpdate());
        }
    }

    IEnumerator UnlockUpdate()
    {
        yield return new WaitForSecondsRealtime(Config.TIME_DAY_UPDATE_DELAY);
        updateLock = false;
    }
}
