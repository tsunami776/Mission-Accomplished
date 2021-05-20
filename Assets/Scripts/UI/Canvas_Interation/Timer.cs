using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] MissionPopUpOnCompletion popUp;

    private RectTransform RectTF;
    private bool updateLock;
    public bool clockLock;

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
            // rotate clock pointer
            float change = -Time.fixedDeltaTime * Config.MODIFIER_TIMER;
            RectTF.Rotate(0f, 0f, change * 2f); // clock round is 12 hours

            // update the skybox and lightning corresponding to the current time
            LM.UpdateTime(change);

            // update Construction Projects Progresses
            // project 1
            if (!GameController.GC.isConstruction1Completed)
            {
                if (GameController.GC.constructionProgress1 < 100.0f)
                {
                    GameController.GC.constructionProgress1 += Time.fixedDeltaTime * Config.MODIFIER_CONSTRUCTION_1 * GameController.GC.constructionGrowth1;
                }
                else
                {
                    GameController.GC.constructionProgress1 = 100.0f;
                    GameController.GC.isConstruction1Completed = true;
                    // give the corresponding reward
                    GameController.GC.waterGrowth += Config.DEFAULT_REWARD_WATER_PROJECT_1;
                    popUp.MissionCompletePopUp(Config.STRING_CONSTRUCTION_NAME_1, Config.STRING_CONSTRUCTION_REWARD_1);
                }
            }

            // project 2
            if (!GameController.GC.isConstruction2Completed)
            {
                if (GameController.GC.constructionProgress2 < 100.0f)
                {
                    GameController.GC.constructionProgress2 += Time.fixedDeltaTime * Config.MODIFIER_CONSTRUCTION_2 * GameController.GC.constructionGrowth2;
                }
                else
                {
                    GameController.GC.constructionProgress2 = 100.0f;
                    GameController.GC.isConstruction2Completed = true;
                    // give the corresponding reward
                    GameController.GC.foodGrowth += Config.DEFAULT_REWARD_FOOD_PROJECT_2;
                    GameController.GC.fundGrowth += Config.DEFAULT_REWARD_FUND_PROJECT_2;
                    popUp.MissionCompletePopUp(Config.STRING_CONSTRUCTION_NAME_2, Config.STRING_CONSTRUCTION_REWARD_2);
                }
            }
            

            // project 3
            if (!GameController.GC.isConstruction3Completed)
            {
                if (GameController.GC.constructionProgress3 < 100.0f)
                {
                    GameController.GC.constructionProgress3 += Time.fixedDeltaTime * Config.MODIFIER_CONSTRUCTION_3 * GameController.GC.constructionGrowth3;
                }
                else
                {
                    GameController.GC.constructionProgress3 = 100.0f;
                    GameController.GC.isConstruction3Completed = true;
                    // give the corresponding reward
                    GameController.GC.troopGrowth += Config.DEFAULT_REWARD_TROOP_PROJECT_3;
                    popUp.MissionCompletePopUp(Config.STRING_CONSTRUCTION_NAME_3, Config.STRING_CONSTRUCTION_REWARD_3);
                }
            }
        }
        
        // clock goes 1 round
        if (RectTF.rotation.eulerAngles.z % 360f >= -0.001f && RectTF.rotation.eulerAngles.z % 360f <= 0.001f && !updateLock)
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
