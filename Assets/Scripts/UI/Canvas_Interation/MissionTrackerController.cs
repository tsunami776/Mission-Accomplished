﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrackerController : MonoBehaviour
{
    // tracker info
    [SerializeField] private GameObject template;
    [SerializeField] private MissionPopUpOnCompletion popUp;
    [SerializeField] private Text missionTrackerText;
    [SerializeField] private Image gridImg;
    [SerializeField] private Image missionProgress;
    [SerializeField] private GameObject insuffientResourceWindow;

    // values
    [HideInInspector] public int missionIndex;
    private float countDown;
    private bool insufficientResource;
    //private 

    /*private void Start()
    {
        tempPos 
    }*/

    // start a new mission
    public void NewMission(int whichMission)
    {
        // modify the template
        string missionName = "";
        switch (whichMission)
        {
            case 1:
                {
                    if (GameController.GC.foodCount >= Config.DEFAULT_MISSION_COST_1[0] && GameController.GC.waterCount >= Config.DEFAULT_MISSION_COST_1[1] &&
                        GameController.GC.fundCount >= Config.DEFAULT_MISSION_COST_1[2] && GameController.GC.troopCount >= Config.DEFAULT_MISSION_COST_1[3])
                    {
                        missionIndex = 1;
                        missionName = Config.STRING_MISSION_NAME_1;
                    }
                    else
                    {
                        insufficientResource = true;
                    }
                    break;
                }
            case 2:
                {
                    if (GameController.GC.foodCount >= Config.DEFAULT_MISSION_COST_2[0] && GameController.GC.waterCount >= Config.DEFAULT_MISSION_COST_2[1] &&
                        GameController.GC.fundCount >= Config.DEFAULT_MISSION_COST_2[2] && GameController.GC.troopCount >= Config.DEFAULT_MISSION_COST_2[3])
                    {
                        missionIndex = 2;
                        missionName = Config.STRING_MISSION_NAME_2;
                    }
                    else
                    {
                        insufficientResource = true;
                    }
                    break;
                }
            case 3:
                {
                    if (GameController.GC.foodCount >= Config.DEFAULT_MISSION_COST_3[0] && GameController.GC.waterCount >= Config.DEFAULT_MISSION_COST_3[1] &&
                        GameController.GC.fundCount >= Config.DEFAULT_MISSION_COST_3[2] && GameController.GC.troopCount >= Config.DEFAULT_MISSION_COST_3[3])
                    {
                        missionIndex = 3;
                        missionName = Config.STRING_MISSION_NAME_3;
                    }
                    else
                    {
                        insufficientResource = true;
                    }
                    break;
                }
            case 4:
                {
                    if (GameController.GC.foodCount >= Config.DEFAULT_MISSION_COST_4[0] && GameController.GC.waterCount >= Config.DEFAULT_MISSION_COST_4[1] &&
                        GameController.GC.fundCount >= Config.DEFAULT_MISSION_COST_4[2] && GameController.GC.troopCount >= Config.DEFAULT_MISSION_COST_4[3])
                    {
                        missionIndex = 4;
                        missionName = Config.STRING_MISSION_NAME_4;
                    }
                    else
                    {
                        insufficientResource = true;
                    }
                    break;
                }
        }

        // if has enough resources
        if (!insufficientResource)
        {
            missionTrackerText.text = missionName;

            // instantiate
            var newMission = Instantiate(gameObject, transform.parent);
            newMission.SetActive(true);
            GameController.GC.allMissions.Add(newMission);

            // change its transform by index
            Vector3 tempPos = template.GetComponent<RectTransform>().localPosition;
            Vector3 newPos = new Vector3(tempPos.x, tempPos.y + (GameController.GC.allMissions.Count - 1) * (Config.OFFSET_GAP_SPAWN_MISSION_TRACKER), tempPos.z);
            newMission.GetComponent<RectTransform>().localPosition = newPos;

            // start countdown
            newMission.GetComponent<MissionTrackerController>().StartTracking(whichMission, missionName);
        }
        else
        {
            insuffientResourceWindow.SetActive(true);
            insufficientResource = false;
        }
        
    }

    // start tracking
    private void StartTracking(int whichMission, string missionName)
    {
        // start counting
        StartCoroutine(Tracking(whichMission, missionName));
    }

    IEnumerator Tracking(int whichMission, string missionName)
    {
        // initialization
        float time = 0;
        var reward = new float[4];
        string missionRewardText = "";
        switch (whichMission)
        {
            case 1:
                {
                    // subtract the cost
                    GameController.GC.foodCount -= Config.DEFAULT_MISSION_COST_1[0];
                    GameController.GC.waterCount -= Config.DEFAULT_MISSION_COST_1[1];
                    GameController.GC.fundCount -= Config.DEFAULT_MISSION_COST_1[2];
                    GameController.GC.troopCount -= Config.DEFAULT_MISSION_COST_1[3];
                    time = Config.TIME_MISSION_TIME_1;
                    reward = Config.DEFAULT_MISSION_REWARD_1;
                    missionRewardText = Config.STRING_MISSION_REWARD_1;
                    break;
                }
            case 2:
                {
                    // subtract the cost
                    GameController.GC.foodCount -= Config.DEFAULT_MISSION_COST_2[0];
                    GameController.GC.waterCount -= Config.DEFAULT_MISSION_COST_2[1];
                    GameController.GC.fundCount -= Config.DEFAULT_MISSION_COST_2[2];
                    GameController.GC.troopCount -= Config.DEFAULT_MISSION_COST_2[3];
                    time = Config.TIME_MISSION_TIME_2;
                    reward = Config.DEFAULT_MISSION_REWARD_2;
                    missionRewardText = Config.STRING_MISSION_REWARD_2;
                    break;
                }
            case 3:
                {
                    // subtract the cost
                    GameController.GC.foodCount -= Config.DEFAULT_MISSION_COST_3[0];
                    GameController.GC.waterCount -= Config.DEFAULT_MISSION_COST_3[1];
                    GameController.GC.fundCount -= Config.DEFAULT_MISSION_COST_3[2];
                    GameController.GC.troopCount -= Config.DEFAULT_MISSION_COST_3[3];
                    time = Config.TIME_MISSION_TIME_3;
                    reward = Config.DEFAULT_MISSION_REWARD_3;
                    missionRewardText = Config.STRING_MISSION_REWARD_3;
                    break;
                }
            case 4:
                {
                    // subtract the cost
                    GameController.GC.foodCount -= Config.DEFAULT_MISSION_COST_4[0];
                    GameController.GC.waterCount -= Config.DEFAULT_MISSION_COST_4[1];
                    GameController.GC.fundCount -= Config.DEFAULT_MISSION_COST_4[2];
                    GameController.GC.troopCount -= Config.DEFAULT_MISSION_COST_4[3];
                    time = Config.TIME_MISSION_TIME_4;
                    reward = Config.DEFAULT_MISSION_REWARD_4;
                    missionRewardText = Config.STRING_MISSION_REWARD_4;
                    break;
                }
        }

        // lock the quest
        GameController.GC.allQuest[whichMission - 1].Activate();

        // count down
        while (countDown < time)
        {
            if (!GameController.GC.timer.clockLock)
            {
                gridImg.color = new Color(gridImg.color.r, gridImg.color.g, gridImg.color.b, 1f);
                missionProgress.color = new Color(missionProgress.color.r, missionProgress.color.g, missionProgress.color.b, 1f);
                missionTrackerText.color = new Color(missionTrackerText.color.r, missionTrackerText.color.g, missionTrackerText.color.b, 1f);
                countDown += Time.fixedDeltaTime;
                missionProgress.fillAmount = countDown / time;
            }
            else
            {
                gridImg.color = new Color(gridImg.color.r, gridImg.color.g, gridImg.color.b, 0f);
                missionProgress.color = new Color(missionProgress.color.r, missionProgress.color.g, missionProgress.color.b, 0f);
                missionTrackerText.color = new Color(missionTrackerText.color.r, missionTrackerText.color.g, missionTrackerText.color.b, 0f);
            }
            yield return new WaitForFixedUpdate();
        }

        // once finished
        popUp.MissionCompletePopUp(missionName, missionRewardText);

        // unlock the quest
        GameController.GC.allQuest[whichMission - 1].Deactivate();

        // Grant the Reward
        GameController.GC.foodCount += reward[0];
        GameController.GC.waterCount += reward[1];
        GameController.GC.fundCount += reward[2];
        GameController.GC.troopCount += reward[3];

        // Destroy the Tracker
        GameController.GC.allMissions.Remove(gameObject);
        for (int i = 0; i < GameController.GC.allMissions.Count; i++)
        {
            // update all remaining mission trackers
            Vector3 tempPos = template.GetComponent<RectTransform>().localPosition;
            Vector3 newPos = new Vector3(tempPos.x, tempPos.y + i * (Config.OFFSET_GAP_SPAWN_MISSION_TRACKER), tempPos.z);
            GameController.GC.allMissions[i].GetComponent<RectTransform>().localPosition = newPos;
        }
        Destroy(gameObject);
    }
}
