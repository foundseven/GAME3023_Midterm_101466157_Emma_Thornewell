using Calender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "CalendarEvents/NewEvent")]
public class NewEventSO : CalenderEventSO
{
    #region Variables
    public string eventName;
    [Header("Date and Time Settings")]
    //hours
    [Range(0, 24)]
    public int hour;
    //minutes
    [Range(0, 59)]
    public int minute;
    //season
    //making it 1 - 4 for readability
    [Range(1, 4)]
    public int season;
    //date
    [Range(1, 28)]
    public int date;
    //year
    [Range(1, 50)] //50 years ig will be fine
    public int year;

    [Header("Prefab Settings")]
    public GameObject prefab;

    #endregion

    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        eventDate = new Calender.DateTime(minute, hour, date, season - 1, year);
        Debug.Log($"Created new event: {eventName} on {eventDate.Date}, {eventDate.Season}");
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        calenderManager.dayColor = color;
        Debug.Log($"{calenderManager.currentDate}");
        if(calenderManager.UpdatedCurrentDate().Date == eventDate.Date)
        {
            Debug.Log("It is the day of your event!");

            //if(prefab != null) 
            //{
            //    prefab.SetActive(true);
            //}
            //else
            //{
            //    Debug.Log("No prefab added!");
            //}
        }
        //else
        //{
        //    prefab.SetActive(false);
        //}
    }
}
