using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : MonoBehaviour
{
    // value sliders
    [SerializeField] private Slider fundsSlider1;
    [SerializeField] private Slider personnelSlider1;
    [SerializeField] private Slider fundsSlider2;
    [SerializeField] private Slider personnelSlider2;
    [SerializeField] private Slider fundsSlider3;
    [SerializeField] private Slider personnelSlider3;

    // available texts
    [SerializeField] private Text fundAvailabeText;
    [SerializeField] private Text personnelAvailabeText;

    // total shared values
    public float totalAvailableFunds;
    public float totalAvailablePersonnel;

    // values for each individual task
    public float funds1;
    public float personnel1;
    public float funds2;
    public float personnel2;
    public float funds3;
    public float personnel3;

    private void Start()
    {
        totalAvailableFunds = Config.DEFAULT_INTI_FUND;
        totalAvailablePersonnel = Config.DEFAULT_INTI_TROOP;
        fundsSlider1.maxValue = Config.DEFAULT_INTI_FUND;
        personnelSlider1.maxValue = Config.DEFAULT_INTI_TROOP;
        fundsSlider2.maxValue = Config.DEFAULT_INTI_FUND;
        personnelSlider2.maxValue = Config.DEFAULT_INTI_TROOP;
        fundsSlider3.maxValue = Config.DEFAULT_INTI_FUND;
        personnelSlider3.maxValue = Config.DEFAULT_INTI_TROOP;
        fundAvailabeText.text = "Fund: " + Config.DEFAULT_INTI_FUND.ToString();
        personnelAvailabeText.text = "Troop: " + Config.DEFAULT_INTI_TROOP.ToString();
    }


    // update total
    public void UpdateAvalibleFunds()
    {
        totalAvailableFunds = GameController.GC.fundCount - funds1 - funds2 - funds3;
        fundAvailabeText.text = "Fund: " + totalAvailableFunds.ToString();
    }
    public void UpdateAvaliblePersonnel()
    {
        totalAvailablePersonnel = GameController.GC.troopCount - personnel1 - personnel2 - personnel3;
        personnelAvailabeText.text = "Troop: " + totalAvailablePersonnel.ToString();
    }


    // task 1
    public void ChangeAvalibleFunds1(float input)
    {
        funds1 = input;
        UpdateAvalibleFunds();

        // if surpass the total value
        if (totalAvailableFunds < 0)
        {
            funds1 += totalAvailableFunds;
            totalAvailableFunds = 0;
            fundsSlider1.value = funds1;
            UpdateAvalibleFunds();
        }
    }
    public void ChangeAvaliblePersonnel1(float input)
    {
        personnel1 = input;
        UpdateAvaliblePersonnel();

        // if surpass the total value
        if (totalAvailablePersonnel < 0)
        {
            personnel1 += totalAvailablePersonnel;
            totalAvailablePersonnel = 0;
            personnelSlider1.value = personnel1;
            UpdateAvaliblePersonnel();
        }
    }
    public void ChangeConstructionGrowth1()
    {
        GameController.GC.constructionGrowth1 = (Config.MODIFIER_AMPLIFIER_FUND_1 * funds1) + (Config.MODIFIER_AMPLIFIER_PERSONNEL_1 * personnel1);
    }


    // task 2
    public void ChangeAvalibleFunds2(float input)
    {
        funds2 = input;
        UpdateAvalibleFunds();

        // if surpass the total value
        if (totalAvailableFunds < 0)
        {
            funds2 += totalAvailableFunds;
            totalAvailableFunds = 0;
            fundsSlider2.value = funds2;
            UpdateAvalibleFunds();
        }
    }
    public void ChangeAvaliblePersonnel2(float input)
    {
        personnel2 = input;
        UpdateAvaliblePersonnel();

        // if surpass the total value
        if (totalAvailablePersonnel < 0)
        {
            personnel2 += totalAvailablePersonnel;
            totalAvailablePersonnel = 0;
            personnelSlider2.value = personnel2;
            UpdateAvaliblePersonnel();
        }
    }
    public void ChangeConstructionGrowth2()
    {
        GameController.GC.constructionGrowth2 = (Config.MODIFIER_AMPLIFIER_FUND_2 * funds2) + (Config.MODIFIER_AMPLIFIER_PERSONNEL_2 * personnel2);
    }


    // task 3
    public void ChangeAvalibleFunds3(float input)
    {
        funds3 = input;
        UpdateAvalibleFunds();

        // if surpass the total value
        if (totalAvailableFunds < 0)
        {
            funds3 += totalAvailableFunds;
            totalAvailableFunds = 0;
            fundsSlider3.value = funds3;
            UpdateAvalibleFunds();
        }
    }
    public void ChangeAvaliblePersonnel3(float input)
    {
        personnel3 = input;
        UpdateAvaliblePersonnel();

        // if surpass the total value
        if (totalAvailablePersonnel < 0)
        {
            personnel3 += totalAvailablePersonnel;
            totalAvailablePersonnel = 0;
            personnelSlider3.value = personnel3;
            UpdateAvaliblePersonnel();
        }
    }
    public void ChangeConstructionGrowth3()
    {
        GameController.GC.constructionGrowth3 = (Config.MODIFIER_AMPLIFIER_FUND_3 * funds3) + (Config.MODIFIER_AMPLIFIER_PERSONNEL_3 * personnel3);
    }
}
