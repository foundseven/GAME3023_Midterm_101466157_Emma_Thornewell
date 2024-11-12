using Calender;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CalenderManager : MonoBehaviour
{
    #region Variables
    [Header("Calender Text Settings")]

    [SerializeField]
    public TextMeshProUGUI Date, Time, Season, Year, Week;

    [Header("Grid Settings")]
    [SerializeField]
    public GameObject dayPrefab;

    [SerializeField]
    public GameObject calenderGrid;

    [Header("Visual Settings")]
    public Light2D light;
    public float nightBrightness;
    public float dayBrightness;

    #endregion

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
    public void UpdateDateTimeUI(DateTime dateTime)
    {
        Date.text = dateTime.DateString();
        Time.text = dateTime.TimeString();
        Season.text = dateTime.Season.ToString();
        Year.text = dateTime.YearString();
        Week.text = dateTime.WeekString();

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
                    //dayText.color = (currentDate == dateTime.Date) ? Color.green : Color.white;

                    //set the color
                    Color dayColor = Color.white;

                    //get the current season
                    Season currentSeason = dateTime.Season;

                    //check for the day
                    DateTime summerSolstice = dateTime.SummerSolstice(dateTime.Year);
                    DateTime halloweenHaunt = dateTime.HalloweenHaunt(dateTime.Year);

                    if (currentSeason == Calender.Season.Spring && currentDate == summerSolstice.Date)
                    {
                        dayColor = Color.magenta;
                    }
                    else if (currentSeason == Calender.Season.Fall && currentDate == halloweenHaunt.Date)
                    {
                        dayColor = Color.yellow;
                    }

                    //highlight the current day in green
                    if (currentDate == dateTime.Date)
                    {
                        dayColor = Color.green;
                    }

                    dayText.color = dayColor;

                }
            }
        }

        // Set target brightness based on time of day
        float targetBrightness = dateTime.IsNight() ? nightBrightness : dayBrightness;

        // Smoothly transition to the target brightness
        light.intensity = Mathf.Lerp(light.intensity, targetBrightness, 0.5f * dateTime.Minutes);

    }
    #endregion
}
