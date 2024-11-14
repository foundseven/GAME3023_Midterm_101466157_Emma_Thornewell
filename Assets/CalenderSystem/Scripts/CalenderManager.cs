using Calender;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;

public class CalenderEvents
{
    public CalenderEventSO events;
}

public class CalenderManager : MonoBehaviour
{
    #region Variables
    [Header("Calender Text Settings")]
    [SerializeField]
    public TextMeshProUGUI Date, Clock, Season, Year, Week;

    [Header("Grid Settings")]
    [SerializeField]
    public GameObject dayPrefab;

    [SerializeField]
    public GameObject calenderGrid;

    [Header("Visual Settings")]
    public Light2D light2D;
    public float nightBrightness;
    public float dayBrightness;

    public GameObject rainPrefab;
    public GameObject calenderPrefab;

    public Color dayColor;

    [Header("Events List")]
    public List<CalenderEventSO> events;

    private int currentDate;
    private Season currentSeason;
    #endregion

    private void Start()
    {
        calenderPrefab.SetActive(false);
        rainPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTimeUI;
    }
    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTimeUI;
    }

    #region Function Time Baby
    //make a function that will update all of the Text
    public void UpdateDateTimeUI(Calender.DateTime dateTime)
    {
        //updateUI
        Date.text = dateTime.DateString();
        Clock.text = dateTime.TimeString();
        Season.text = dateTime.Season.ToString();
        Year.text = dateTime.YearString();
        Week.text = dateTime.WeekString();

        // Clear existing calendar entries
        foreach (Transform child in calenderGrid.transform)
        {
            Destroy(child.gameObject);
        }

        CreateCalender(dateTime);

        //set target brightness
        float targetBrightness = dateTime.IsNight() ? nightBrightness : dayBrightness;

        //transition to the target brightness
        light2D.intensity = Mathf.Lerp(light2D.intensity, targetBrightness, 0.5f * 1);
    }

    public void CreateCalender(Calender.DateTime dateTime)
    {
        int daysInMonth = 28;
        int firstDayOfMonth = (int)dateTime.Days - ((dateTime.Date - 1) % 7);

        // Loop to create day boxes
        for (int week = 0; week < 4; week++)
        {
            for (int weekDay = 1; weekDay <= 7; weekDay++)
            {
                int currentDate = week * 7 + weekDay;

                if (currentDate < firstDayOfMonth || currentDate > daysInMonth)
                {
                    GameObject emptyBox = Instantiate(dayPrefab, calenderGrid.transform);
                    emptyBox.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
                else
                {
                    GameObject dayBox = Instantiate(dayPrefab, calenderGrid.transform);
                    TextMeshProUGUI dayText = dayBox.GetComponentInChildren<TextMeshProUGUI>();

                    dayText.text = currentDate.ToString("D2");

                    //set the  base color
                    dayColor = Color.white;

                    foreach (var calenderEvents in events)
                    {
                        if (currentDate == calenderEvents.eventDate.Date && currentSeason == calenderEvents.eventDate.Season)
                        {
                            calenderEvents.TriggerEvent(this);
                        }
                    }

                    //CreateEvents();

                    //highlight the current day in green
                    if (currentDate == dateTime.Date)
                    {
                        dayColor = Color.green;
                    }

                    dayText.color = dayColor;

                }
            }
        }
    }

    public void ToggleOffandOn()
    {
        calenderPrefab.SetActive(!calenderPrefab.activeSelf);
    }

    //public void CreateEvents()
    //{
    //    foreach (CalenderEvents calEvent in events)
    //    {
    //        if (currentDate == calenderEvents.eventDate.Date && currentSeason == calenderEvents.eventDate.Season)
    //        {
    //            calenderEvents.TriggerEvent(this);
    //        }
    //    }
    //}

    #endregion
}
