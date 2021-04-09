using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // all Missions
    public int missionTotalNumber;
    private bool[][] missionStates; /* state index{ 0=isUnlocked, 1=isOngoing, 2=isComplete, 3=isSuccessful }*/

    // start
    private void Start()
    {
        // initializing
        missionStates = new bool[missionTotalNumber][];
        for (int i = 0; i < missionTotalNumber; i++)
        {
            missionStates[i] = new bool[4];
        }
    }

    // lock a mission
    public void LockMission(int whichMission)
    {
        missionStates[whichMission][0] = false;
        UpdateMissionState();
    }

    // unlock a mission
    public void UnlockMission(int whichMission)
    {
        missionStates[whichMission][0] = true;
        UpdateMissionState();
    }

    // set a mission ongoing
    public void OngoingMission(int whichMission)
    {
        missionStates[whichMission][1] = true;
        missionStates[whichMission][2] = false;
        UpdateMissionState();
    }


    // complete a mission
    public void CompleteMission(int whichMission)
    {
        missionStates[whichMission][1] = false;
        missionStates[whichMission][2] = true;
        UpdateMissionState();
    }

    // succeed a mission
    public void SucceedMission(int whichMission)
    {
        missionStates[whichMission][3] = true;
        UpdateMissionState();
    }

    // fail a mission
    public void FailMission(int whichMission)
    {
        missionStates[whichMission][3] = false;
        UpdateMissionState();
    }


    // tell the corresponding NPCs
    public void UpdateMissionState(/* parameter: whichNPCs */)
    {
        // tell NPCs which yarn shall be loaded according to their corresponding missions states
    }
}
