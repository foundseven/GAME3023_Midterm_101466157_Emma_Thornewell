using Calender;
using System;
using System.Collections.Generic;
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
    public TextMeshProUGUI eventListText;

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
    public int displayDaysAhead = 30;

    public int currentDate;
    public Season currentSeason;

    private Calender.DateTime currentDateTime;
    private Calender.DateTime currentSeasonDT;

    private Season displayedSeason;
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

        CreateCalender(dateTime, displayedSeason);
        DisplayUpcomingEvents();
        GetCurrentSeason();

        //set target brightness
        float targetBrightness = dateTime.IsNight() ? nightBrightness : dayBrightness;

        //transition to the target brightness
        light2D.intensity = Mathf.Lerp(light2D.intensity, targetBrightness, 0.5f * 1);
    }

    public Calender.DateTime UpdatedCurrentDate()
    {
        return currentDateTime;
    }
    public Calender.Season UpdatedCurrentSeason(Season season)
    {
        return season;
    }
    public void CreateCalender(Calender.DateTime dateTime, Season season)
    {
        currentDateTime = dateTime;

        int daysInMonth = 28;
        int firstDayOfMonth = (int)dateTime.Days - ((dateTime.Date - 1) % 7);

        // Loop to create day boxes
        for (int week = 0; week < 4; week++)
        {
            for (int weekDay = 1; weekDay <= 7; weekDay++)
            {
                currentDate = week * 7 + weekDay;

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

                    //check to see if there is an event that needs to be added
                    foreach (var calenderEvents in events)
                    {
                        if (currentDate == calenderEvents.eventDate.Date && currentDateTime.Season == calenderEvents.eventDate.Season)
                        {
                            dayColor = calenderEvents.color;
                            calenderEvents.TriggerEvent(this);
                        }
                    }

                    //Debug.Log($"Current Day: {dateTime.Date}, Current Season: {dateTime.Season}");
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
    public void GetCurrentSeason()
    {
        displayedSeason = currentDateTime.Season;
        Debug.Log($"Current season: {displayedSeason}");
    }
    //i could expland on this to make this a little nicer
    public void DisplayUpcomingEvents()
    {
        eventListText.text = "";
        foreach (var eventSO in events)
        {
            // Calculate how far ahead the event is
            if(eventSO.eventDate.Season == currentDateTime.Season)
            {
                string eventInfo = $"{eventSO.eventName} on {eventSO.eventDate.Season} {eventSO.eventDate.Date}\n";
                eventListText.text += eventInfo;
            }
        }
    }

    public void CreateEventsInCalender()
    {
        foreach (var calenderEvents in events)
        {
            if (currentDate == calenderEvents.eventDate.Date && currentDateTime.Season == calenderEvents.eventDate.Season)
            {
                dayColor = calenderEvents.color;
                calenderEvents.TriggerEvent(this);
            }
        }
    }
#endregion

    #region Buttons
    public void ToggleOffandOn()
    {
        calenderPrefab.SetActive(!calenderPrefab.activeSelf);
    }

    public void NextSeason()
    {
        TimeManager.Instance.season = TimeManager.Instance.season + 1;

        TimeManager.Instance.UpdateDateTime();
    }

    public void PreviousSeason()
    {
        TimeManager.Instance.season = TimeManager.Instance.season - 1;

        TimeManager.Instance.UpdateDateTime();
    }
    #endregion
}
