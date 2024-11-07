using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//can now usee this so it is Calender.TimeManager
namespace Calender
{
    public class TimeManager : MonoBehaviour
    {
        #region Variables
        //so what do i need in order to make a calender
        [Header("Date and Time Settings")]
        //hours
        [Range(0, 24)]
        public int hour;
        //minutes
        [Range(0, 59)]
        public int minute;
        //seconds
        [Range(1, 59)]
        public int seconds;
        //season
        //making it 1 - 4 for readability
        [Range(1, 4)]
        public int season;
        //date
        [Range(1, 28)]
        public int date;
        //year
        [Range(1, 50)] //50 years ig will be fine
        public int year;
        //using something called DateTime,
        //"DateTime helps developer to find out more information about Date and Time like Get month, day, year, week day.
        //It also helps to find date difference, add number of days to a date, etc."
        //So we can store the current date and time this way
        private DateTime DateTime;

        //create a unity action with date time so we can alert anything that needs to know
        //so everytime the date changes it can recieve this unity action and will update the clock accordingly
        public static UnityAction<DateTime> OnDateTimeChanged;

        #endregion

        private void Awake()
        {
            //setting a new datetime with my variables
            //our own struct if yew will
            DateTime = new DateTime(year, season - 1, date, hour, minute, seconds);
        }

        void Start()
        {
            OnDateTimeChanged?.Invoke(DateTime);
        }

        //we will want to run through ticking here
        void Update()
        {
        
        }
    }

}
