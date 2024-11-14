using Calender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "CalendarEvents/NewEvent")]
public class NewEventSO : CalenderEventSO
{
    public string eventName;
 
    //create the color in the editor
    public Color color;

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

    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        eventDate = new Calender.DateTime(minute, hour, date, season - 1, year);
        Debug.Log($"Created new event: {eventName} on {eventDate.Date}, {eventDate.Season}");
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        calenderManager.dayColor = color;
    }
}
