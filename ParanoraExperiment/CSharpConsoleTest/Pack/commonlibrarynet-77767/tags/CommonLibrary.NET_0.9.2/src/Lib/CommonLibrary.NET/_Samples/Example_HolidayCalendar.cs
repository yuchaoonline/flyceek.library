using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using CommonLibrary;
using CommonLibrary.DomainModel;
using CommonLibrary.Membership;


namespace CommonLibrary.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_HolidayCalendar : ApplicationTemplate
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_HolidayCalendar()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // 1. Use the default holiday calendar loaded with U.S. holidays.
            // What is next business date after 1/1/<current_year>
            DateTime nextBusDay = HolidayCalendar.GetNextBusinessDate(new DateTime(DateTime.Today.Year, 1, 1));
            DateTime previousBusinessDay = HolidayCalendar.GetPreviousBusinessDate(new DateTime(DateTime.Today.Year, 1, 1));
            
            // 2. Use other holiday calendar.
            // NOTE:( Current implementation only uses hard dates ( no-relative dates 3rd thursday of november )
            // can be supplied. This limitation will be fixed.
            HolidayCalanderDao calDao = new HolidayCalanderDao("kishore's_holiday_calendar", GetSampleHolidays());
            IHolidayCalendar calService = new HolidayCalendarService("kishore's_holiday_calendar", calDao, 5);

            nextBusDay = calService.GetNextBusinessDate(DateTime.Today);
            Console.WriteLine("Next business date using \"kishore's_holiday_calendar\" : " + nextBusDay.ToString());
            return BoolMessageItem.True;
        }



        /// <summary>
        /// Get sample united states holidays.
        /// 
        /// This can be loaded from an XML file.
        /// </summary>
        /// <returns></returns>
        private static List<HolidayItem> GetSampleHolidays()
        {
            // For testing, New Years, July 4th, Christmas.
            // This can be loaded from the database.
            var holidays = new List<HolidayItem>()
            {
                new HolidayItem(1, 1, true, DayOfWeek.Monday, -1,   "Start the year fresh day"),
                new HolidayItem(1, 19, true, DayOfWeek.Monday, -1,  "King Midas"),
                new HolidayItem(2, 14, true, DayOfWeek.Monday, -1,  "Get it on with your girlfriend day"),
                new HolidayItem(5, 25, true, DayOfWeek.Monday, -1,  "World War 1 Day"),
                new HolidayItem(7, 4, true, DayOfWeek.Monday, -1,   "Free at last day"),
                new HolidayItem(9, 7, true, DayOfWeek.Monday, -1,   "Get your ass to work Day"),
                new HolidayItem(10, 12, true, DayOfWeek.Monday, -1, "I found a piece of land day."),
                new HolidayItem(11, 11, true, DayOfWeek.Monday, -1, "War - What is it good for day."),
                new HolidayItem(11, 26, true, DayOfWeek.Monday, -1, "Thank you god for everything i have day."),
                new HolidayItem(12, 25, true, DayOfWeek.Monday, -1, "I want my xbox 360 gift day.")
            };
            return holidays;
        }
    }
}
