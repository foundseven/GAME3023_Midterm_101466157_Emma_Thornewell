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
        eventDate = new Calender.DateTime(0, 6, 7, 0, 1);
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        if(calenderManager.UpdatedCurrentDate().Date == eventDate.Date)
        {
            calenderManager.rainPrefab.SetActive(true);
        }
        else
        {
            calenderManager.rainPrefab.SetActive(false);
        }
        calenderManager.dayColor = Color.cyan;
    }
}
