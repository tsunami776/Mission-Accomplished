using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * When using functions from this singleton, please call the static obj "GameController.GC.FunctionName" in your scripts directly, 
 * instead of calling it from the gameObject.
*/

public class GameController : MonoBehaviour
{
    // singleton Game Controller
    public static GameController GC;

    // player character
    public GameObject player;
    public Transform spawn;

    // date and time
    private DateTime _startDate = new DateTime(2005, 1, 1);
    public DateTime currentDate;
    public Timer timer;

    // all missions
    public int missionTotalNumber;
    public List<GameObject> allMissions;
    public MissionViewController MVC;

    // mission states
    private List<int> missionUnlocked;
    private List<int> missionOngoing;
    private List<int> missionComplete;
    private List<int> missionSuccessful;
    private List<int> missionFailed;

    // date hold all items/resources
    private ResourceManager resourceManager;

    // intialize singleton
    private void Awake()
    {
        if (GC == null)
        {
            GC = this;
        }
    }

    // start
    private void Start()
    {
        // initializing player intial variables
        spawn.position = player.transform.position;
        spawn.rotation = player.transform.rotation;

        // initializing date and time
        currentDate = _startDate;
        resourceManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();

        // initializing state lists
        allMissions = new List<GameObject>();
        missionUnlocked = new List<int>();
        missionOngoing = new List<int>();
        missionComplete = new List<int>();
        missionSuccessful = new List<int>();
        missionFailed = new List<int>();
    }

    // go to the next day
    public void GoToNextDay()
    {
        currentDate = currentDate.AddDays(1);
        resourceManager.currentDate = currentDate;
        resourceManager.UpdateResourceCounts();
    }

    // lock a mission
    public void LockMission(int whichMission)
    {
        missionUnlocked.Remove(whichMission);
        UpdateMissionState_NPC();
        UpdateMissionState_Player();
    }

    // unlock a mission
    public void UnlockMission(int whichMission)
    {
        missionUnlocked.Add(whichMission);
        UpdateMissionState_NPC();
        UpdateMissionState_Player();
    }

    // set a mission ongoing
    public void OngoingMission(int whichMission)
    {
        missionOngoing.Add(whichMission);
        missionComplete.Remove(whichMission);
        UpdateMissionState_NPC();
        UpdateMissionState_Player();
    }


    // complete a mission
    public void CompleteMission(int whichMission)
    {
        missionOngoing.Remove(whichMission);
        missionComplete.Add(whichMission);
        UpdateMissionState_NPC();
        UpdateMissionState_Player();
    }

    // succeed a mission
    public void SucceedMission(int whichMission)
    {
        missionSuccessful.Add(whichMission);
        CompleteMission(whichMission);
    }

    // fail a mission
    public void FailMission(int whichMission)
    {
        missionFailed.Add(whichMission);
        CompleteMission(whichMission);
    }


    // tell the player the changes of the mission states
    public void UpdateMissionState_Player(/* parameter: whichNPCs */)
    {
        // tell player to update the mission view and dynamic mission tracker
        MVC.UpdateMissionView();
        Debug.Log("NPC Mission States Updated");
    }

    // tell the corresponding NPCs the changes of the mission states
    public void UpdateMissionState_NPC(/* parameter: whichNPCs */)
    {
        // tell NPCs which yarn shall be loaded according to their corresponding missions states\
        Debug.Log("NPC Mission States Updated");

        // send mission2: [0][0][0][0] => NPCs(1,3,5)
    }

    //
    public void UpdateSomeItemsOrResources(/* you interact with a NPC to purchase/sell some items */)
    {
        
    }

    //
    public void UseItem(/* which item */)
    {
        
    }
}

// 1=true, 0=false, for mission2: [0][0][0][0]
