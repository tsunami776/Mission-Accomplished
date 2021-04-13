using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // Fields
    private TimeManager timeManager;

    private float[] rates;
    private float[] resourcesCounts;

    private Text[] rateTexts;
    private Text[] resourceCountTexts;

    [SerializeField] private DateTime lastDate;
    [SerializeField] private DateTime currentDate;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();

        //{water, food}
        rates = new float[] {5, 10};
        resourcesCounts = new float[] {0, 0};

        rateTexts = new Text[]
        {
            GameObject.FindGameObjectWithTag("WaterRateText").GetComponent<Text>(),
            GameObject.FindGameObjectWithTag("FoodRateText").GetComponent<Text>()
        };
        resourceCountTexts = new Text[]
        {
            GameObject.FindGameObjectWithTag("WaterCountText").GetComponent<Text>(),
            GameObject.FindGameObjectWithTag("FoodCountText").GetComponent<Text>()
        };

        currentDate = timeManager.GetCurrentDate();
        lastDate = currentDate;
    }

    // Update is called once per frame
    void Update()
    {
        // Update texts
        for (int i = 0; i < rates.Length; i++)
        {
            rateTexts[i].text = rates[i].ToString();
            resourceCountTexts[i].text = resourcesCounts[i].ToString();
        }
    }

    public void UpdateResourceCounts()
    {
        for (int i = 0; i < rates.Length; i++)
        {
            resourcesCounts[i] += rates[i];
        }
    }
}