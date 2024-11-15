using Calender;
using UnityEngine;

[CreateAssetMenu(fileName = "SummerSolsticeEvent", menuName = "CalendarEvents/SummerSolstice")]
public class SummerSolsticeSO : CalenderEventSO
{
    private void OnEnable()
    {
        // Set the specific date for the rainy season event, for example.
        eventDate = new Calender.DateTime(0, 9, 28, (int)Season.Spring, 1);
    }

    public override void TriggerEvent(CalenderManager calenderManager)
    {
        calenderManager.dayColor = Color.magenta;

        if (calenderManager.UpdatedCurrentDate().Date == eventDate.Date)
        {
             if (calenderManager.light2D != null)
             {
                //increase the brightness 
                float extraBrightness = 4.0f; 
                calenderManager.light2D.intensity = extraBrightness;
             }
        }
        else
        {
            if (calenderManager.light2D != null)
            {
                float defaultBrightness = 1.0f;
                calenderManager.light2D.intensity = defaultBrightness;
            }
        }
    }
}
