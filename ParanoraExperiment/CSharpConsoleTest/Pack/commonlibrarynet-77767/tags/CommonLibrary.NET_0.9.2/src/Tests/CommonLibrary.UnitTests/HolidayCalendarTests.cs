using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using CommonLibrary;


namespace CommonLibrary.Tests
{
    [TestFixture]
    public class HolidayCalendarTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            HolidayCalanderDao dao = new HolidayCalanderDao();

            // For testing, jan1, july4, christmas.
            List<HolidayItem> holidays = new List<HolidayItem>()
            {
                new HolidayItem(1, 1, true, DayOfWeek.Monday, -1, "New Years"),
                new HolidayItem(7, 4, true, DayOfWeek.Monday, -1, "Independence Day"),
                new HolidayItem(12, 25, true, DayOfWeek.Monday, -1, "Christmas Day")
            };
            dao.Load("usa-bronx-holidays", holidays);            
            HolidayCalendar.Init("usa-bronx-holidays", dao, DateTime.Today.Year, DateTime.Today.Year + 1);
        }


        [Test]
        public void CanGetNextBusinessDate()
        {
            DateTime busDay = HolidayCalendar.GetNextBusinessDate(new DateTime(DateTime.Today.Year, 1, 1));

            Assert.AreEqual(HolidayCalendar.CalendarCode, "usa-bronx-holidays");
            Assert.AreEqual(busDay, new DateTime(DateTime.Today.Year, 1, 2));
        }


        [Test]
        public void CanGetFirstBusinessDateOfYear()
        {
            DateTime busDay = HolidayCalendar.GetFirstBusinessDateOfYear(DateTime.Today.Year);

            Assert.AreEqual(busDay, new DateTime(DateTime.Today.Year, 1, 2));
        }


        [Test]
        public void CanGetFirsttBusinessDateOfMonth()
        {
            DateTime busDay = HolidayCalendar.GetFirstBusinessDateOfMonth(1, DateTime.Today.Year);

            Assert.AreEqual(busDay, new DateTime(DateTime.Today.Year, 1, 2));
        }
    }
}