using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using ComLib;
using ComLib.Types;


namespace CommonLibrary.Tests
{
    

    [TestFixture]
    public class TimeParserTests
    {
        [Test]
        public void CanNotParseWithOutAmPm()
        {
            IList<string> errors = new List<string>();
            TimeSpan time = TimeHelper.Parse("1", errors);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(time.Hours, TimeSpan.MinValue.Hours);
            Assert.AreEqual(time.Minutes, TimeSpan.MinValue.Minutes);           
        }


        [Test]
        public void CanParseWithoutMinutesInPm()
        {
            IList<string> errors = new List<string>();
            TimeSpan time = TimeHelper.Parse("1pm", errors);
            Assert.IsTrue(errors.Count == 0);
            Assert.AreEqual(time.Hours, 13);
            Assert.AreEqual(time.Minutes, 0);       
        }


        [Test]
        public void CanParseWithoutMinutesInAm()
        {
            IList<string> errors = new List<string>();
            TimeSpan time = TimeHelper.Parse("8am", errors);
            Assert.IsTrue(errors.Count == 0);
            Assert.AreEqual(time.Hours, 8);
            Assert.AreEqual(time.Minutes, 0);       
        }


        [Test]
        public void CanParseWithMinutesInPm()
        {
            IList<string> errors = new List<string>();
            TimeSpan time = TimeHelper.Parse("10:45pm", errors);
            Assert.IsTrue(errors.Count == 0);
            Assert.AreEqual(time.Hours, 22);
            Assert.AreEqual(time.Minutes, 45);       
        }


        [Test]
        public void CanParseWithMinutesInAm()
        {
            IList<string> errors = new List<string>();
            TimeSpan time = TimeHelper.Parse("6:30am", errors);
            Assert.IsTrue(errors.Count == 0);
            Assert.AreEqual(time.Hours, 6);
            Assert.AreEqual(time.Minutes, 30);       
        }


        [Test]
        public void CanParseTimeRange()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30am to 1pm", errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);           
        }


        [Test]
        public void CanParseTimeRangeWithPeriodsInAmPm()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30 a.m. to 1 p.m.", errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);
        }


        [Test]
        public void CanParseTimeRangeWithSpacesBetweenTimeAndAm()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30 am to 1 pm", errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);
        }


        [Test]
        public void CanParseTimeRangeWithOutEndTime()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30am", errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
            Assert.AreEqual(TimeParserConstants.MaxDate, result.EndTimeAsDate);           
        }


        [Test]
        public void CanParseIndividualStartEndTimes()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30am", "1pm", true, errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(end, result.End);   
        }


        [Test]
        public void CanNotParseIndividualStartEndTimesWithMissingEndTime()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30am", "", true, errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(TimeSpan.MinValue, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
        }


        [Test]
        public void CanParseIndividualStartEndTimesWithMissingEndTime()
        {
            IList<string> errors = new List<string>();
            TimeParseResult result = TimeHelper.ParseStartEndTimes("11:30am", "", false, errors);

            TimeSpan start = new TimeSpan(11, 30, 0);
            TimeSpan end = new TimeSpan(13, 0, 0);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(start, result.Start);
            Assert.AreEqual(TimeSpan.MaxValue, result.End);
        }
    }
}
