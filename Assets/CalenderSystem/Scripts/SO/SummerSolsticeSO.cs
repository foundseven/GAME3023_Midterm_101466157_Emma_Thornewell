using Calender;
using UnityEngine;

[CreateAssetMenu(fileName = "SummerSolsticeEvent", menuName = "CalendarEvents/SummerSolstice")]
public class SummerSolsticeSO : CalenderEventSO
{
    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        eventDate = new Calender.DateTime(0, 6, 28, (int)Season.Spring, 1);
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        calenderManager.dayColor = Color.magenta;
    }
}
