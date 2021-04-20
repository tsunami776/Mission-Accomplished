using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionViewController : MonoBehaviour
{
    // all missions
    public List<GameObject> allMissions = new List<GameObject>();
    public int missionCount;
    
    // template mission and text
    [SerializeField] private GameObject missionTemp;
    [SerializeField] private GameObject missionField;
    [SerializeField] private Text missionText;

    // mission tracker
    [SerializeField] private MissionTrackerController MTC;
    

    // update all mission info from the Game Controller
    public void UpdateMissionView()
    {
        
    }

    // generate an unlocked(avaliable) mission 
    public void NewUnlockedMission(int whichMission)
    {
        // modify the template
        missionCount++;
        missionText.text = "Mission" + whichMission.ToString() + " (Avaliable)";

        // instantiate
        var newMission = Instantiate(missionTemp, missionField.transform);
        newMission.SetActive(true);
        allMissions.Add(newMission);

        // change its transform by index
        Vector3 tempPos = missionTemp.GetComponent<RectTransform>().localPosition;
        Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (missionCount - 1) * (-60f), tempPos.z);
        newMission.GetComponent<RectTransform>().localPosition = newPos;
        newMission.GetComponent<Image>().color = Config.MISSION_COLOR_UNLOCKED;
    }

    public void NewOngoingMission(int whichMission)
    {
        // modify the template
        missionCount++;
        missionText.text = "Mission" + whichMission.ToString() + " (Ongoing)";

        // instantiate
        var newMission = Instantiate(missionTemp, missionField.transform);
        newMission.SetActive(true);
        allMissions.Add(newMission);

        // change its transform by index
        Vector3 tempPos = missionTemp.GetComponent<RectTransform>().localPosition;
        Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (missionCount - 1) * (-60f), tempPos.z);
        newMission.GetComponent<RectTransform>().localPosition = newPos;
        newMission.GetComponent<Image>().color = Config.MISSION_COLOR_ONGOING;

        // update the mission tracker
        MTC.TrackerStartNewMission(whichMission.ToString(), 55f);
    }

    public void NewCompleteMission(int whichMission)
    {
        // modify the template
        missionCount++;
        missionText.text = "Mission" + whichMission.ToString() + " (Complete)";

        // instantiate
        var newMission = Instantiate(missionTemp, missionField.transform);
        newMission.SetActive(true);
        allMissions.Add(newMission);

        // change its transform by index
        Vector3 tempPos = missionTemp.GetComponent<RectTransform>().localPosition;
        Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (missionCount - 1) * (-60f), tempPos.z);
        newMission.GetComponent<RectTransform>().localPosition = newPos;
        newMission.GetComponent<Image>().color = Config.MISSION_COLOR_COMPLETED;
    }

    public void NewFailedMission(int whichMission)
    {
        // modify the template
        missionCount++;
        missionText.text = "Mission" + whichMission.ToString() + " (Failed)";

        // instantiate
        var newMission = Instantiate(missionTemp, missionField.transform);
        newMission.SetActive(true);
        allMissions.Add(newMission);

        // change its transform by index
        Vector3 tempPos = missionTemp.GetComponent<RectTransform>().localPosition;
        Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (missionCount - 1) * (-60f), tempPos.z);
        newMission.GetComponent<RectTransform>().localPosition = newPos;
        newMission.GetComponent<Image>().color = Config.MISSION_COLOR_FAILED;
    }
}
