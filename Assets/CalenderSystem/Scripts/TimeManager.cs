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

        //
        private void AdvanceTime()
        {
            //so here we want to run through everything
            //staring with seconds
            seconds++;
            if(seconds >= 60)
            {
                //put seconds back at 0
                seconds = 0;
                minute++;
                if(minute >= 60) 
                {
                    //put minute back at 0
                    minute = 0;
                    hour++;
                    if(hour >= 24)
                    {
                        //pu hour back at 0
                        hour = 0;
                        date++;
                        if(date > 28)
                        {
                            //rotate the day back to the first
                            date = 1;
                            season++;
                            if(season > 4)
                            {
                                //rotate the season back to the first
                                season = 1;
                                year++;
                            }
                        }
                    }
                }
            }
            //notify listeners about the updated time
            OnDateTimeChanged?.Invoke(DateTime);
        }
    }

    [Serializable]
    public struct DateTime
    {
        #region Fields

        private int seconds;
        private int minutes;
        private int hour;

        private Days day;
        private int date;
        private int year;

        //make something for the season - enum
        private Season season;

        //add num of days and weeks to rot through
        private int totalNumDays;
        private int totalNumWeeks;

        #endregion

        #region Properties
        
        public int Seconds => seconds;
        public int Minutes => minutes;
        public int Hours => hour;
        private Days Days => day;
        public int Date => date;
        public int Year => year;
        public Season Season => season;
        public int TotalNumDays => totalNumDays;
        //todo
        //a little math will be needed to get the current week
        public int CurrentWeek => totalNumWeeks;  


        #endregion

        #region Constructor
        public DateTime(int seconds, int minutes, int hours, int date, int season, int year)
        {
            //going to have the this constructer added
            //so this will be for setting up the date season years and what not
            //timing
            this.seconds = seconds;
            this.minutes = minutes; 
            this.hour = hours;

            //this sets the day of the week with the knowledge of what the date itself is
            //example = if date = 10 then 1- % 7 = 3 meaning its the third day of the week
            this.day = (Days)(date % 7);
            if(day == 0)
            {
                day = (Days)7;
            }

            this.date = date;
            this.season = (Season)season;
            this.year = year;

            //set the total num days
            //cast our season enum to an int, check if it is greater than 0, it will calc the number of days based on date and season
            //it then multiplies the current season number by 28 (assumed date i put in)
            //lastly it adds the date to the total days in previous seaons repping the culmulative day count for the entire year up to the day
            totalNumDays = (int)this.season > 0  ? date + (28 * (int)this.season) : 0;
            //adjust the total numdays to include the days from previous years
            totalNumDays = year > 1 ? totalNumDays + (112 * (year - 1)) : totalNumDays;
            //set total weeks based on the total num days
            totalNumWeeks = 1 + totalNumDays / 7;
        }
        #endregion

        //todo
        //set date and time advancement in here
        #region Times Arrow Marches forward (Time advancement)

        //maybe i break it down by seconds, minutes, hour, days, season
        #endregion
    }

    #region Enums
    [Serializable]
    public enum Days
    {
        NULL = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    [Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3
    }
    #endregion
}
