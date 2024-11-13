using Calender;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
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
        Date.text = dateTime.DateString();
        Clock.text = dateTime.TimeString();
        Season.text = dateTime.Season.ToString();
        Year.text = dateTime.YearString();
        Week.text = dateTime.WeekString();


        //TODO: make this more readable
        // Clear existing calendar entries
        foreach (Transform child in calenderGrid.transform)
        {
            Destroy(child.gameObject);
        }

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
                    //get the current season
                    Season currentSeason = dateTime.Season;


                    //if (currentDate == dateTime.SummerSolstice(dateTime.Year).Date && currentSeason == dateTime.SummerSolstice(dateTime.Year).Season)
                    //{
                    //    dayColor = Color.magenta;
                    //}
                    //else if (currentDate == dateTime.HalloweenHaunt(dateTime.Year).Date && currentSeason == dateTime.HalloweenHaunt(dateTime.Year).Season)
                    //{
                    //    dayColor = Color.yellow;
                    //}

                    //else if (currentDate == dateTime.RainySeason(dateTime.Year).Date && currentSeason == dateTime.RainySeason(dateTime.Year).Season)
                    //{
                    //    dayColor = Color.cyan;

                    //    if(dateTime.Date == dateTime.RainySeason(dateTime.Year).Date)
                    //    {
                    //        rainPrefab.SetActive(true);
                    //    }
                    //    else
                    //    {
                    //        rainPrefab.SetActive(false);
                    //    }
                    //}

                    //highlight the current day in green
                    if (currentDate == dateTime.Date)
                    {
                        dayColor = Color.green;
                    }

                    dayText.color = dayColor;

                }
            }
        }
        //set target brightness
        float targetBrightness = dateTime.IsNight() ? nightBrightness : dayBrightness;

        //transition to the target brightness
        light2D.intensity = Mathf.Lerp(light2D.intensity, targetBrightness, 0.5f * 1);
    }

    public void ToggleOffandOn()
    {
        calenderPrefab.SetActive(!calenderPrefab.activeSelf);
    }

    public void TriggerEvent(Calender.DateTime dateTime)
    {
        foreach (var calenderEvents in events)
        {
            if (calenderEvents.eventDate.Equals(dateTime))
            {
                calenderEvents.TriggerEvent(this);
            }
        }
    }

    public void CheckAndTriggerEvents(Calender.DateTime dateTime)
    {
        foreach (var calenderEvent in events)
        {
            if (calenderEvent.eventDate.Equals(dateTime))
            {
                calenderEvent.TriggerEvent(this);
            }
        }
    }

    //public void AddEventToCalender(Calender.DateTime dateTime, int currentDate, Color eventColor)
    //{
    //    //get the current season
    //    Season currentSeason = dateTime.Season;

    //    if (currentDate == dateTime.AddCustomEvent(0, 6, 7, 0, 1).Date && currentSeason == dateTime.AddCustomEvent(0, 6, 7, 0, 1).Season)
    //    {
    //        eventColor = Color.cyan;

    //        if (dateTime.Date == dateTime.AddCustomEvent(0, 6, 7, 0, 1).Date)
    //        {
    //            rainPrefab.SetActive(true);
    //        }
    //        else
    //        {
    //            rainPrefab.SetActive(false);
    //        }
    //    }
    //}

    #endregion
}
