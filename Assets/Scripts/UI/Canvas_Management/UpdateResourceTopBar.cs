using UnityEngine;
using UnityEngine.UI;

public class UpdateResourceTopBar : MonoBehaviour
{
    enum allResourcesType { food, water, fund, troop}

    [SerializeField] private allResourcesType whichResource;
    private Text resourceText;

    private void Start()
    {
        resourceText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (whichResource)
        {
            case allResourcesType.food:
                {
                    resourceText.text = GameController.GC.foodCount.ToString();
                    break;
                }

            case allResourcesType.water:
                {
                    resourceText.text = GameController.GC.waterCount.ToString();
                    break;
                }

            case allResourcesType.fund:
                {
                    resourceText.text = GameController.GC.fundCount.ToString();
                    break;
                }

            case allResourcesType.troop:
                {
                    resourceText.text = GameController.GC.troopCount.ToString();
                    break;
                }
        }
    }
}
