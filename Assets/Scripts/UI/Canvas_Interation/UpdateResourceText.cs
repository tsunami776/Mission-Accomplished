using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateResourceText : MonoBehaviour
{
    enum resourceType { funds, troop }

    [SerializeField] private CommandTerminal cmd;
    [SerializeField] private Slider whichSlider;
    [SerializeField] private resourceType resource;

    private void Start()
    {
        if (resource == resourceType.funds)
        {
            GetComponent<Text>().text = "0" + "/" + Config.DEFAULT_INTI_FUND;
        }
        if (resource == resourceType.troop)
        {
            GetComponent<Text>().text = "0" + "/" + Config.DEFAULT_INTI_TROOP;
        }
    }

    // on value changed
    public void FixedUpdate()
    {
        if (resource == resourceType.funds)
        {
            whichSlider.maxValue = GameController.GC.fundCount;
            GetComponent<Text>().text = whichSlider.value.ToString() + "/" + GameController.GC.fundCount;
            cmd.UpdateAvalibleFunds();
        }
        if (resource == resourceType.troop)
        {
            whichSlider.maxValue = GameController.GC.troopCount;
            GetComponent<Text>().text = whichSlider.value.ToString() + "/" + GameController.GC.troopCount;
            cmd.UpdateAvaliblePersonnel();
        }
    }
}
