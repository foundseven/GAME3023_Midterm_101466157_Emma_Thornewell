using Calender;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Date, Time, Season, Year/*, Week*/;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTimeUI;
    }
    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTimeUI;
    }
    //make a function that will update all of the Text
    public void UpdateDateTimeUI(DateTime dateTime)
    {
        Date.text = dateTime.DateString();
        Time.text = dateTime.TimeString();
        Season.text = dateTime.Season.ToString();
        Year.text = dateTime.Year.ToString();
    }
}
