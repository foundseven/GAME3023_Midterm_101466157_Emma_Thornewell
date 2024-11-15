using Calender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "CalendarEvents/NewEvent")]
public class NewEventSO : CalenderEventSO
{
    #region Variables
    //public string eventName;
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
    [Min(0)]
    public int year;

    [Header("Prefab Settings")]
    public Sprite image;
    private GameObject instantiatedPrefab;
    public bool hasPrefab;
    public GameObject prefab;
    public Vector3 spawnPosition;
    public bool useCustomPosition;

    #endregion

    private void OnEnable()
    {
        if(hasPrefab)
        {
            prefab.SetActive(true);
        }
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
            Debug.Log($"It is the day of your event! {eventName}");

            //if (hasPrefab && prefab != null)
            //{
            //   // GameObject instantiatedPrefab = Instantiate(prefab);
            //   // instantiatedPrefab.transform.position = useCustomPosition ? spawnPosition : Vector3.zero;
            //  //  Debug.Log("Prefab instantiated!");
            //}
            if (hasPrefab && prefab != null && instantiatedPrefab == null)
            {
                instantiatedPrefab = Instantiate(prefab);
                instantiatedPrefab.transform.position = useCustomPosition ? spawnPosition : Vector3.zero;
                instantiatedPrefab.SetActive(true);
                Debug.Log("Prefab instantiated!");
            }
        }
        else
        {
            // Optionally deactivate the prefab if the event is not happening today
            if (instantiatedPrefab != null)
            {
                instantiatedPrefab.SetActive(false);
            }
        }

    }
}
