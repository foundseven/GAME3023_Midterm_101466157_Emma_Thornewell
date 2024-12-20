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
    private GameObject instantiatedPrefab;
    private Transform playerTransform;

    public bool hasPrefab;
    public GameObject prefab;
    public Vector3 spawnPosition;
    public bool useCustomPosition;

    [Header("Audio Settings")]
    public bool hasAudio;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;


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
        Debug.Log($"{color}");

        // Get the player's transform
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;  // Assumes the player has the tag "Player"
        }

        if (calenderManager.UpdatedCurrentDate().Date == eventDate.Date)
        {
            Debug.Log($"It is the day of your event! {eventName}");

            if (hasAudio && backgroundMusic != null)
            {
                if (audioSource == null)
                {
                    audioSource = calenderManager.GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = calenderManager.gameObject.AddComponent<AudioSource>();
                    }
                }

                if (!audioSource.isPlaying || audioSource.clip != backgroundMusic)
                {
                    audioSource.clip = backgroundMusic;
                    audioSource.Play();
                    Debug.Log($"Playing background music for {eventName}");
                }
            }

            if (hasPrefab && prefab != null && instantiatedPrefab == null)
            {
                // Instantiate the prefab and set its parent to the player
                instantiatedPrefab = Instantiate(prefab);
                instantiatedPrefab.transform.SetParent(playerTransform);

                // Position it relative to the player's head
                instantiatedPrefab.transform.localPosition = new Vector3(0, 1.5f, 0);
                instantiatedPrefab.transform.localRotation = Quaternion.identity; 

                instantiatedPrefab.SetActive(true);
                Debug.Log("Prefab attached to player!");
            }
        }
        else
        {
            //deactivate
            if (instantiatedPrefab != null)
            {
                instantiatedPrefab.SetActive(false);
            }

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Stopped background music as the event is no longer active.");
            }
        }

    }
}
