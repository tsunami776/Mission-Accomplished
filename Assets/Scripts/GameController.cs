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
    public int currentQuest;
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
        DisableCursor();
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
        currentQuest = -1;

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

    // tell the player the changes of the mission states
    public void UpdateMissionState_Player()
    {
        if (currentQuest != -1)
        {
            // tell player to update the mission view and dynamic mission tracker
            Debug.Log("NPC Mission States Updated");
            allQuest[currentQuest].gameObject.SetActive(true);
        }
    }

    //
    public void UpdateSomeItemsOrResources(/* you interact with a NPC to purchase/sell some items */)
    {
        
    }

    //
    public void UseItem(/* which item */)
    {
        
    }

    // disable mouse cursor
    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // enable mouse cursor
    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}

// 1=true, 0=false, for mission2: [0][0][0][0]
