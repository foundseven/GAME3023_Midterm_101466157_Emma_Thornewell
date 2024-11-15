using UnityEngine;

public abstract class CalenderEventSO : ScriptableObject
{
    //public string eventName;
    public Calender.DateTime eventDate;

    public string eventName;

    //create the color in the editor
    public Color color;

    public abstract void TriggerEvent(CalenderManager calenderManager);
}
