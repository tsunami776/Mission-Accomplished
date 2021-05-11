using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrackerController : MonoBehaviour
{
    // tracker info
    [SerializeField] private MissionPopUpOnCompletion popUp;
    [SerializeField] private Text missionTrackerText;
    [SerializeField] private Image missionProgress;

    public List<GameObject> allMissions = new List<GameObject>();
    private float countDown;

    // start a new mission
    public void NewMission(int whichMission)
    {
        // modify the template
        string missionName = "";
        switch (whichMission)
        {
            case 1:
                {
                    missionName = Config.STRING_MISSION_NAME_1;
                    break;
                }
            case 2:
                {
                    missionName = Config.STRING_MISSION_NAME_2;
                    break;
                }
            case 3:
                {
                    missionName = Config.STRING_MISSION_NAME_3;
                    break;
                }
            case 4:
                {
                    missionName = Config.STRING_MISSION_NAME_4;
                    break;
                }
        }
        missionTrackerText.text = missionName;

        // instantiate
        var newMission = Instantiate(gameObject, transform.parent);
        newMission.SetActive(true);
        allMissions.Add(newMission);

        // change its transform by index
        Vector3 tempPos = GetComponent<RectTransform>().localPosition;
        Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (allMissions.Count - 1) * (-55f), tempPos.z);
        newMission.GetComponent<RectTransform>().localPosition = newPos;

        // start countdown
        newMission.GetComponent<MissionTrackerController>().StartTracking(whichMission, missionName, allMissions);
    }

    // start tracking
    private void StartTracking(int whichMission, string missionName, List<GameObject> missionList)
    {
        // start counting
        StartCoroutine(Tracking(whichMission, missionName, missionList));
    }

    IEnumerator Tracking(int whichMission, string missionName, List<GameObject> missionList)
    {
        // TODO: Take the cost





        float time = 0;
        string missionReward = "";
        switch (whichMission)
        {
            case 1:
                {
                    time = Config.TIME_MISSION_TIME_1;
                    missionReward = Config.STRING_MISSION_REWARD_1;
                    break;
                }
            case 2:
                {
                    time = Config.TIME_MISSION_TIME_2;
                    missionReward = Config.STRING_MISSION_REWARD_2;
                    break;
                }
            case 3:
                {
                    time = Config.TIME_MISSION_TIME_3;
                    missionReward = Config.STRING_MISSION_REWARD_3;
                    break;
                }
            case 4:
                {
                    time = Config.TIME_MISSION_TIME_4;
                    missionReward = Config.STRING_MISSION_REWARD_4;
                    break;
                }
        }


        // DEBUG: the progress bar running during day switching


        // count down
        while (countDown < time)
        {
            countDown += Time.fixedDeltaTime;
            missionProgress.fillAmount = countDown / time;
            yield return new WaitForFixedUpdate();
        }

        // once finished
        popUp.MissionCompletePopUp(missionName, missionReward);




        // TODO: Grant the Reward
        // TODO:
        // 






        missionList.Remove(gameObject);
        Destroy(gameObject);
    }
}
