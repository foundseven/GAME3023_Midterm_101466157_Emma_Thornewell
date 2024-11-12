using Calender;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Date, Time, Season, Week;

    //make a function that will update all of the Text
    public void UpdateDateTimeUI(DateTime dateTime)
    {
        Date.text = dateTime.ToString();
        Time.text = dateTime.ToString();
        Season.text = Season.ToString();
        Week.text = Week.ToString();
    }
}
