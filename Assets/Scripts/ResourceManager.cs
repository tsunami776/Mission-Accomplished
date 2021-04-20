using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // Fields
    private float[] rates;
    private float[] resourcesCounts;

    [SerializeField] private Text[] rateTexts;
    [SerializeField] private Text[] resourceCountTexts;

    [SerializeField] private DateTime lastDate;
    public DateTime currentDate;

    // Start is called before the first frame update
    void Start()
    {
        //{water, food}
        rates = new float[] {5, 10};
        resourcesCounts = new float[] {0, 0};
        lastDate = currentDate;

        // Update texts
        for (int i = 0; i < rates.Length; i++)
        {
            rateTexts[i].text = rates[i].ToString();
            resourceCountTexts[i].text = resourcesCounts[i].ToString();
        }
    }

    public void UpdateResourceCounts()
    {
        // update resources
        for (int i = 0; i < rates.Length; i++)
        {
            resourcesCounts[i] += rates[i];
        }

        // Update texts
        for (int i = 0; i < rates.Length; i++)
        {
            rateTexts[i].text = rates[i].ToString();
            resourceCountTexts[i].text = resourcesCounts[i].ToString();
        }
    }
}