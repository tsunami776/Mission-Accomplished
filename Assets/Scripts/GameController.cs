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
    [SerializeField] private Text TopBar_Date;
    public Timer timer;

    // all missions & NPCs
    public int missionTotalNumber;
    public GameObject[] allNPCs;

    // mission states
    public List<GameObject> allMissions;

    // mission quests list
    public QuestManager[] allQuest;

    // construction projects
    [HideInInspector] public float constructionProgress1;
    [HideInInspector] public float constructionProgress2;
    [HideInInspector] public float constructionProgress3;
    [HideInInspector] public float constructionGrowth1;
    [HideInInspector] public float constructionGrowth2;
    [HideInInspector] public float constructionGrowth3;
    [HideInInspector] public bool isConstruction1Completed;
    [HideInInspector] public bool isConstruction2Completed;
    [HideInInspector] public bool isConstruction3Completed;

    // all items/resources
    [HideInInspector] public float foodCount;
    [HideInInspector] public float waterCount;
    [HideInInspector] public float fundCount;
    [HideInInspector] public float troopCount;
    [HideInInspector] public float foodGrowth;
    [HideInInspector] public float waterGrowth;
    [HideInInspector] public float fundGrowth;
    [HideInInspector] public float troopGrowth;


    // intialize singleton
    private void Awake()
    {
        if (GC == null)
        {
            GC = this;
        }
        allMissions = new List<GameObject>();
    }

    // start
    private void Start()
    {
        // initializing player intial variables
        spawn.position = player.transform.position;
        spawn.rotation = player.transform.rotation;

        // initializing date
        currentDate = _startDate;

        // initializing all resources
        foodCount = Config.DEFAULT_INTI_FOOD;
        waterCount = Config.DEFAULT_INTI_WATER;
        fundCount = Config.DEFAULT_INTI_FUND;
        troopCount = Config.DEFAULT_INTI_TROOP;
        foodGrowth = Config.DEFAULT_GROWTH_FOOD;
        waterGrowth = Config.DEFAULT_GROWTH_WATER;
        fundGrowth = Config.DEFAULT_GROWTH_FUND;
        troopGrowth = Config.DEFAULT_GROWTH_TROOP;

        // initializing state lists
        constructionProgress1 = 0f;
        constructionProgress2 = 0f;
        constructionProgress3 = 0f;
        allNPCs = GameObject.FindGameObjectsWithTag("NPC");

        // update topbar info UI
        UpdateTopBarDate();
    }

    // go to the next day and calculate new resources
    public void GoToNextDay()
    {
        currentDate = currentDate.AddDays(1);
        foodCount += foodGrowth;
        waterCount += waterGrowth;
        fundCount += fundGrowth;
        troopCount += troopGrowth;
    }

    // update top bar info
    public void UpdateTopBarDate()
    {
        // update date
        TopBar_Date.text = "Date: " + currentDate.ToShortDateString();
    }

    /*    // lock a mission
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
        }*/


    // tell the player the changes of the mission states
    public void UpdateMissionState_Player(/* parameter: whichNPCs */)
    {
        // tell player to update the mission view and dynamic mission tracker
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
