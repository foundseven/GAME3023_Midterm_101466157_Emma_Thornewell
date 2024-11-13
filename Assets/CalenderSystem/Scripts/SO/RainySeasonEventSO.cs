using Calender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "RainySeasonEvent", menuName = "CalendarEvents/RainySeason")]
public class RainySeasonEventSO : CalenderEventSO
{
    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        //eventDate = new Calender.DateTime(0, 6, 7, (int)Season.Spring, 1);
    }
    public override void TriggerEvent(CalenderManager calendarManager)
    {
        calendarManager.rainPrefab.SetActive(true);
        //set a new color
        calendarManager.dayColor = Color.cyan;
        Debug.Log($"{eventName} triggered!");
    }
}
