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

        //todo: add my own ticking to make time move at a different speed?
        [Header("Personal Tick Settings")]
        [SerializeField]
        public int TickIncrease = 1;

        [SerializeField]
        public float TimeBetweenTicks = .5f;

        [SerializeField]
        public float CurrentTickTime = 0;


        #endregion

        private void Awake()
        {
            //setting a new datetime with my variables
            //our own struct if yew will
            DateTime = new DateTime(minute, hour, date, season - 1, year);
        }

        void Start()
        {
            OnDateTimeChanged?.Invoke(DateTime);
        }

        //we will want to run through ticking here
        void Update()
        {
            //so ill run through my own ticking here
            CurrentTickTime += Time.deltaTime;

            //so when it gets to 2, reset and do it again
            if(CurrentTickTime >= TimeBetweenTicks)
            {
                CurrentTickTime = 0;
                AdvanceTime();
            }
        }

        //changing this so it maybe only works through the ticking, and the date and time struct will hold the actual movement
        private void AdvanceTime()
        {
            //ill call on my functions i made here
            DateTime.AdvanceMinutes(TickIncrease);
            OnDateTimeChanged?.Invoke(DateTime);
        }
    }

    [Serializable]
    public struct DateTime
    {
        #region Fields

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
        
        public int Minutes => minutes;
        public int Hours => hour;
        public Days Days => day;
        public int Date => date;
        public int Year => year;
        public Season Season => season;
        public int TotalNumDays => totalNumDays;
        //todo
        //a little math will be needed to get the current week
        public int CurrentWeek => totalNumWeeks;  


        #endregion

        #region Constructor
        public DateTime(int minutes, int hours, int date, int season, int year)
        {
            //going to have the this constructer added
            //so this will be for setting up the date season years and what not
            //timing
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

        #region Times Arrow Marches forward (Time advancement) - Bojack Horseman ref....

        //maybe i break it down by minutes, hour, days, season
        //minutes
        public void AdvanceMinutes(int timeMovement)
        {
            Debug.Log("Minutes: " + minutes);
            //so make the minutes go up
            minutes += timeMovement;

            //if the seconds reach 60
            if(minutes >= 60)
            {
                //make the minute 1
                minutes = (minutes) % 60;
                //advance the hour
                AdvanceHours();
            }
        }
        //hours
        private void AdvanceHours()
        {
            //add the hour
            hour++;
            //if the hour is 24 (bc duh you cant have that)
            if((hour) == 24)
            {
                //hour equals 0
                hour = 0;
                //add the day
                AdvanceDays();
            }
        }
        //days
        private void AdvanceDays()
        {
            //totaly num of days goes up
            totalNumDays++;
           
            //move the day up
            day++;
            //so we want to advance the day (of the week) AND the date
            //so if the DOTW is greater than 7
            if (day + 1 > (Days)8)
            {
                //put the day of the week back to 1
                day = (Days)1;
                totalNumWeeks++;
            }

            //move the date up
            date++;
            //so this way we know if the "month" is up
            if(date % 29  == 0)
            {
                //bring it back to the 1st bc you cant have day 0
                date = 1;
                //advance the season
                AdvanceSeason();
            }
        }
        //season
        private void AdvanceSeason()
        {
            //so run through all of them, starting at spring
            //when it hits the last one (winter), go back to spring

            season++;

            if(Season == Season.Winter)
            {
                season = Season.Spring;
                AdvanceYear();
            }
        }

        //year
        private void AdvanceYear()
        {
            //should reset the date back to 1
            date = 1;
            year++;
        }
        #endregion

        //todo
        #region Event Definition + Checks

        public bool IsNight()
        {
            //if it is after 8 pm or before 6 am
            return hour > 20 || hour < 6;
        }

        public bool IsMorning()
        {
            //if it between 6 and just before 12
            return hour >= 6 && hour < 12;
        }

        public DateTime AddCustomEvent(int minutes, int hours, int date, int season, int year)
        {
            return new DateTime(minutes, hours, date, season, year);
        }

        #endregion

        #region Printing stringggggs

        //todo: format this so it runs through the days like a calender would, rather than through the classic format
        //maybe do two so we can have a mini player and a big calender?
        public string DateString()
        {
            //making a string that prints the current date
            //so day date and year
            return $"{day} / {Date} / {Year}";
        }

        public string TimeString()
        {
            //making a string that prints the current time
            //this will be for minutes and hours
            //remember it is a 24 hour clock
            int fixedHour = 0;
            string period = "AM";

            if(hour == 0)
            {
                //if it is midnight
                fixedHour = 12;
            }
            else if(hour == 12)
            {
                //if it is noon
                fixedHour = 12;
                period = "PM";
            }
            else if(hour >= 13)
            {
                //any hour above 13 so making it a pm case
                fixedHour = hour - 12;
                period = "PM";
            }
            else
            {
                //if it is morning
                fixedHour = hour;
            }

            return $"{fixedHour} : {minutes} {period}";
        }

        public string WeekString()
        {
            return $"Week: {CurrentWeek}";
        }

        public string YearString()
        {
            return $"Year: {year}";
        }

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
