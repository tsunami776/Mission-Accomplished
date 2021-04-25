using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private RectTransform RectTF;
    private bool updateLock;
    private bool clockLock;

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
        if (!clockLock)
        {
            // update time
            currentTime += Time.fixedDeltaTime;

            // rotate clock pointer
            float change = -Time.fixedDeltaTime * Config.MODIFIER_TIMER;
            RectTF.Rotate(0f, 0f, change * 2f); // clock round is 12 hours

            // update the skybox and lightning corresponding to the current time
            LM.UpdateTime(change);
        }
        
        // update date per round
        if (RectTF.rotation.eulerAngles.z % 360f >= -0.1f && RectTF.rotation.eulerAngles.z % 360f <= 0.1f && !updateLock)
        {
            //Debug.Log("Go Next Day");
            GameController.GC.GoToNextDay();
            LM.DayTransition();
            updateLock = true;
            clockLock = true;
            StartCoroutine(UnlockUpdate());
        }
    }

    public void Unlock()
    {
        updateLock = false;
        clockLock = false;
    }

    IEnumerator UnlockUpdate()
    {
        yield return new WaitForSecondsRealtime(Config.TIME_DAY_UPDATE_DELAY);
        updateLock = false;
    }
}
