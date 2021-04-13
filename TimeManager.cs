using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //Fields
    private ResourceManager resourceManager;

    private DateTime _startDate = new DateTime(2005, 1, 1);
    private DateTime currentDate;

    private Text currentDateText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize fields
        currentDate = _startDate;
        currentDateText = GameObject.FindGameObjectWithTag("CurrentDateText").GetComponent<Text>();

        resourceManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDateText.text = currentDate.ToShortDateString();
    }

    public void GoToNextDay()
    {
        currentDate = currentDate.AddDays(1);

        resourceManager.UpdateResourceCounts();
    }

    public DateTime GetCurrentDate()
    {
        return currentDate;
    }
}