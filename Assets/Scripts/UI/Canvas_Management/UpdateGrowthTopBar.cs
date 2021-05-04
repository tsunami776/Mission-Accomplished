using UnityEngine;
using UnityEngine.UI;

public class UpdateGrowthTopBar : MonoBehaviour
{
    enum allResourcesType { food, water, fund, troop}

    [SerializeField] private allResourcesType whichResource;
    private Text growthText;

    private void Start()
    {
        growthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (whichResource)
        {
            case allResourcesType.food:
                {
                    if (GameController.GC.foodGrowth > 0)
                    {
                        growthText.color = Color.green;
                        growthText.text = "(+" + GameController.GC.foodGrowth.ToString() + ")";
                    }
                    else if (GameController.GC.foodGrowth == 0)
                    {
                        growthText.color = Color.red;
                        growthText.text = "(" + GameController.GC.foodGrowth.ToString() + ")";
                    }
                    else
                    {
                        growthText.color = Color.red;
                        growthText.text = "(-" + GameController.GC.foodGrowth.ToString() + ")";
                    }
                    break;
                }

            case allResourcesType.water:
                {
                    if (GameController.GC.waterGrowth > 0)
                    {
                        growthText.color = Color.green;
                        growthText.text = "(+" + GameController.GC.waterGrowth.ToString() + ")";
                    }
                    else if (GameController.GC.waterGrowth == 0)
                    {
                        growthText.color = Color.red;
                        growthText.text = "(" + GameController.GC.waterGrowth.ToString() + ")";
                    }
                    else
                    {
                        growthText.color = Color.red;
                        growthText.text = "(-" + GameController.GC.waterGrowth.ToString() + ")";
                    }
                    break;
                }

            case allResourcesType.fund:
                {
                    if (GameController.GC.fundGrowth > 0)
                    {
                        growthText.color = Color.green;
                        growthText.text = "(+" + GameController.GC.fundGrowth.ToString() + ")";
                    }
                    else if (GameController.GC.fundGrowth == 0)
                    {
                        growthText.color = Color.red;
                        growthText.text = "(" + GameController.GC.fundGrowth.ToString() + ")";
                    }
                    else
                    {
                        growthText.color = Color.red;
                        growthText.text = "(-" + GameController.GC.fundGrowth.ToString() + ")";
                    }
                    break;
                }

            case allResourcesType.troop:
                {
                    if (GameController.GC.troopGrowth > 0)
                    {
                        growthText.color = Color.green;
                        growthText.text = "(+" + GameController.GC.troopGrowth.ToString() + ")";
                    }
                    else if (GameController.GC.troopGrowth == 0)
                    {
                        growthText.color = Color.red;
                        growthText.text = "(" + GameController.GC.troopGrowth.ToString() + ")";
                    }
                    else
                    {
                        growthText.color = Color.red;
                        growthText.text = "(-" + GameController.GC.troopGrowth.ToString() + ")";
                    }
                    break;
                }
        }
    }
}
