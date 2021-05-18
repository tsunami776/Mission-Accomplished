using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // references
    [SerializeField] private GameObject newQuestWindow;
    [SerializeField] private GameObject questNotAvailableWindow;
    [SerializeField] private Text costText;
    [SerializeField] private Text rewardText;

    // values
    public int whichMission;
    public bool isQuestAvailable;

    public void Activate()
    {
        isQuestAvailable = true;
    }

    public void Deactivate()
    {
        isQuestAvailable = false;
    }

    private void OnEnable()
    {
        GameController.GC.player.GetComponent<PlayerMovementController>().Lock();
        GameController.GC.player.GetComponent<PlayerLookController>().Lock();
        if (isQuestAvailable)
        {
            newQuestWindow.SetActive(false);
            questNotAvailableWindow.SetActive(true);
        }
        else
        {
            newQuestWindow.SetActive(true);
            questNotAvailableWindow.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GameController.GC.player.GetComponent<PlayerMovementController>().Unlock();
        GameController.GC.player.GetComponent<PlayerLookController>().Unlock();
    }

    private void OnValidate()
    {
        switch (whichMission)
        {
            case 1:
                {
                    costText.text = Config.STRING_MISSION_COST_1;
                    rewardText.text = Config.STRING_MISSION_REWARD_1;
                    break;
                }
            case 2:
                {
                    costText.text = Config.STRING_MISSION_COST_2;
                    rewardText.text = Config.STRING_MISSION_REWARD_2;
                    break;
                }
            case 3:
                {
                    costText.text = Config.STRING_MISSION_COST_3;
                    rewardText.text = Config.STRING_MISSION_REWARD_3;
                    break;
                }
            case 4:
                {
                    costText.text = Config.STRING_MISSION_COST_4;
                    rewardText.text = Config.STRING_MISSION_REWARD_4;
                    break;
                }
        }
    }
}
