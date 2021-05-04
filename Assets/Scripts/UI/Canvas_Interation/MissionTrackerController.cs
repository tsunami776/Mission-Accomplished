using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrackerController : MonoBehaviour
{
    // tracker info
    [SerializeField] private Text missionTrackerText;
    [SerializeField] private Text missionTrackerTimer;

    // start a new mission
    public void TrackerStartNewMission(string trackerText, float trackerTimer)
    {
        StartCoroutine(NewMission(trackerText, trackerTimer));
    }

    IEnumerator NewMission(string trackerText, float trackerTimer)
    {
        missionTrackerText.text = "Mission: " + trackerText;
        missionTrackerTimer.text = "00:" + trackerTimer.ToString();

        // TODO: add timer count down

        yield return new WaitForSecondsRealtime(trackerTimer);

        missionTrackerText.text = "Current Mission: ";
        missionTrackerTimer.text = "00:00";
    }
}
