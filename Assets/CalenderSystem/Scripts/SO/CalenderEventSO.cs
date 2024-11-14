using System.Collections.Generic;
using UnityEngine;

public abstract class CalenderEventSO : ScriptableObject
{
    //public string eventName;
    public Calender.DateTime eventDate;

    public abstract void TriggerEvent(CalenderManager calenderManager);
}
