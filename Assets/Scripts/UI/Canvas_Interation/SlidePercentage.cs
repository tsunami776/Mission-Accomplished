using UnityEngine;
using UnityEngine.UI;

public class SlidePercentage : MonoBehaviour
{
    [SerializeField] private int whichProject;
    [SerializeField] private Text percentage;
    [SerializeField] private Text rewardText;

    public void FixedUpdate()
    {
        switch (whichProject)
        {
            case 1:
                {
                    if (!GameController.GC.isConstruction1Completed)
                    {
                        GetComponent<Image>().fillAmount = GameController.GC.constructionProgress1 / 100f;
                    }
                    else 
                    {
                        GetComponent<Image>().fillAmount = 1f;
                        GetComponent<Image>().color = Config.MISSION_COLOR_COMPLETED;
                        rewardText.color = Config.MISSION_COLOR_COMPLETED;
                    }
                    UpdatePercentage();
                    break;
                }

            case 2:
                {
                    if (!GameController.GC.isConstruction2Completed)
                    {
                        GetComponent<Image>().fillAmount = GameController.GC.constructionProgress2 / 100f;
                    }
                    else
                    {
                        GetComponent<Image>().fillAmount = 1f;
                        GetComponent<Image>().color = Config.MISSION_COLOR_COMPLETED;
                        rewardText.color = Config.MISSION_COLOR_COMPLETED;
                    }
                    UpdatePercentage();
                    break;
                }

            case 3:
                {
                    if (!GameController.GC.isConstruction3Completed)
                    {
                        GetComponent<Image>().fillAmount = GameController.GC.constructionProgress3 / 100f;
                    }
                    else
                    {
                        GetComponent<Image>().fillAmount = 1f;
                        GetComponent<Image>().color = Config.MISSION_COLOR_COMPLETED;
                        rewardText.color = Config.MISSION_COLOR_COMPLETED;
                    }
                    UpdatePercentage();
                    break;
                }
        }
    }

    public void UpdatePercentage()
    {
        switch (whichProject)
        {
            case 1:
                {
                    percentage.text = GameController.GC.constructionProgress1.ToString("F2") + "%";
                    break;
                }

            case 2:
                {
                    percentage.text = GameController.GC.constructionProgress2.ToString("F2") + "%";
                    break;
                }

            case 3:
                {
                    percentage.text = GameController.GC.constructionProgress3.ToString("F2") + "%";
                    break;
                }
        }
    }
}
