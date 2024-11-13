using Calender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenHauntEvent", menuName = "CalendarEvents/HalloweenHaunt")]
public class HalloweenHauntSO : CalenderEventSO
{
    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        eventDate = new Calender.DateTime(0, 6, 28, (int)Season.Fall, 1);
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        calenderManager.dayColor = Color.yellow;
    }
}
