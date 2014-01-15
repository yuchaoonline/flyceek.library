/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;



namespace ComLib.Types
{
    /// <summary>
    /// Enum to represent the time as a part of the day.
    /// </summary>
    public enum StartTimeOfDay { All = 0, Morning, Afternoon, Evening };


    /// <summary>
    /// Time parse result.
    /// </summary>
    public class TimeParseResult
    {
        public readonly bool IsValid;
        public readonly string Error;
        public readonly TimeSpan Start;
        public readonly TimeSpan End;


        /// <summary>
        /// Constructor to initialize the results
        /// </summary>
        /// <param name="valid"></param>
        /// <param name="error"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public TimeParseResult(bool valid, string error, TimeSpan start, TimeSpan end)
        {
            IsValid = valid;
            Error = error;
            Start = start;
            End = end;
        }


        /// <summary>
        /// Get the start time as a datetime
        /// </summary>
        public DateTime StartTimeAsDate
        {
            get 
            {
                if (Start == TimeSpan.MinValue) 
                    return TimeParserConstants.MinDate;

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Start.Hours, Start.Minutes, Start.Seconds); 
            }
        }


        /// <summary>
        /// Get the end time as a datetime
        /// </summary>
        public DateTime EndTimeAsDate
        {
            get 
            {
                if (End == TimeSpan.MaxValue) 
                    return TimeParserConstants.MaxDate;

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, End.Hours, End.Minutes, End.Seconds); 
            }
        }
    }



    /// <summary>
    /// constants used by the time parser.
    /// </summary>
    public class TimeParserConstants
    {
        /// <summary>
        /// Am string.
        /// </summary>
        public const string Am = "am";

        /// <summary>
        /// Am string with periods a.m.
        /// </summary>
        public const string AmWithPeriods = "a.m.";
        
        /// <summary>
        /// Pm string.
        /// </summary>
        public const string Pm = "pm";

        /// <summary>
        /// Pm string with periods p.m.
        /// </summary>
        public const string PmWithPeriods = "p.m.";

        /// <summary>
        /// Min Time to represent All times for a post.
        /// </summary>
        public static readonly DateTime MinDate = new DateTime(2000, 1, 1);

        /// <summary>
        /// Max Time to represent all times for a post.
        /// </summary>
        public static readonly DateTime MaxDate = new DateTime(2050, 1, 1);      

        public const string ErrorEndTimeLessThanStart = "End time must be greater than or equal to start time.";
        public const string ErrorStartEndTimeSepartorNotPresent = "Start and end time separator not present, use '-' or 'to'";
        public const string ErrorStartTimeNotProvided = "Start time not provided";
        public const string ErrorEndTimeNotProvided = "End time not provided";
    }



    /// <summary>
    /// Class to parse time in following formats.
    /// 
    /// 1. 1
    /// 2. 1am
    /// 3. 1pm
    /// 4. 1:30
    /// 5. 1:30am
    /// 6. 12pm
    /// </summary>
    public class TimeHelper
    {
        #region Parse Methods
        /// <summary>
        /// Parses the start and (optional) end time supplied as a single string.
        /// 
        /// e.g.
        ///     11:30am
        ///     11am    -  1pm
        ///     11am    to 1pm
        /// </summary>
        /// <remarks>If only 1 time is provided, it's assumed to be the starttime,
        /// and the end time is set to TimeSpan.MaxValue</remarks>
        /// <param name="startAndEndTimeRange"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static TimeParseResult ParseStartEndTimes(string startAndEndTimeRange, IList<string> errors)
        {
            string dateSeparator = "-";
            int ndxOfSeparator = startAndEndTimeRange.IndexOf(dateSeparator);
            
            if (ndxOfSeparator < 0 )
            {
                dateSeparator = "to";
                ndxOfSeparator = startAndEndTimeRange.IndexOf(dateSeparator);

                // No end time specified.
                if (ndxOfSeparator < 0)
                {
                    int initialErrorCount = errors.Count;
                    TimeSpan startOnlyTime = Parse(startAndEndTimeRange, errors);
                    
                    // Error parsing?
                    if (errors.Count > initialErrorCount)
                        return new TimeParseResult(false, errors[0], startOnlyTime, TimeSpan.MaxValue);

                    return new TimeParseResult(true, string.Empty, startOnlyTime, TimeSpan.MaxValue);
                }
            }

            string starts = startAndEndTimeRange.Substring(0, ndxOfSeparator);
            string ends = startAndEndTimeRange.Substring(ndxOfSeparator + dateSeparator.Length);
            return ParseStartEndTimes(starts, ends, true, errors);
        }


        /// <summary>
        /// Parses the start and end time and confirms if the end time is greater than
        /// the start time.
        /// e.g. 11am, 1:30pm
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static TimeParseResult ParseStartEndTimes(string starts, string ends, bool checkEndTime, IList<string> errors)
        {
            // Validate start and end times were provided.
            if (string.IsNullOrEmpty(starts))
            {
                errors.Add(TimeParserConstants.ErrorStartTimeNotProvided);
                return new TimeParseResult(false, TimeParserConstants.ErrorStartTimeNotProvided, TimeSpan.MinValue, TimeSpan.MaxValue);
            }

            if ( checkEndTime && string.IsNullOrEmpty(ends))
            {
                errors.Add(TimeParserConstants.ErrorEndTimeNotProvided);                
                return new TimeParseResult(false, TimeParserConstants.ErrorEndTimeNotProvided, TimeSpan.MinValue, TimeSpan.MaxValue);
            }

            int initialErrorCount = errors.Count;
            TimeSpan start = Parse(starts, errors);
            TimeSpan end = TimeSpan.MaxValue;

            // Validate start time is ok.
            if (errors.Count > initialErrorCount) return new TimeParseResult(false, errors[0], TimeSpan.MinValue, TimeSpan.MaxValue);

            // Validate end time is ok.
            if (checkEndTime)
            {
                end = Parse(ends, errors);
                if (errors.Count > initialErrorCount) return new TimeParseResult(false, errors[0], TimeSpan.MinValue, TimeSpan.MaxValue);

                if (end < start)
                {
                    errors.Add(TimeParserConstants.ErrorEndTimeLessThanStart);
                    return new TimeParseResult(false, TimeParserConstants.ErrorEndTimeLessThanStart, start, end);
                }
            }
            return new TimeParseResult(true, string.Empty, start, end);
        }


        /// <summary>
        /// Parse the time. Can pass as 9am, 12pm, 3:30pm
        /// </summary>
        /// <param name="time">Non-military time. e.g. 9am, 12pm, 3:30pm</param>
        /// <returns></returns>
        public static TimeSpan Parse(string time, IList<string> errors)
        {
            // Validate.
            if (string.IsNullOrEmpty(time))
            {
                errors.Add("No time specified.");
                return TimeSpan.MinValue;
            }

            // Convert to lowercase for am pm.
            time = time.Trim().ToLower();

            string amFormat = TimeParserConstants.Am;
            string pmFormat = TimeParserConstants.Pm;

            // Check for am.
            bool hasAm = time.IndexOf(amFormat) > 0;
            bool hasPm = time.IndexOf(pmFormat) > 0;

            // Require am/pm.
            if (!hasAm && !hasPm)
            {
                amFormat = TimeParserConstants.AmWithPeriods;
                pmFormat = TimeParserConstants.PmWithPeriods;

                hasAm = time.IndexOf(amFormat) > 0;
                hasPm = time.IndexOf(pmFormat) > 0;

                if (!hasAm && !hasPm )
                {
                    errors.Add("Am / Pm is not specified in : " + time);
                    return TimeSpan.MinValue;
                }
            }

            int indexOfMinutesSeparator = time.IndexOf(":");
            int indexOfAmPm = -1;
            bool hasMinutesSeparator = indexOfMinutesSeparator > 0;
            string hourType = string.Empty;
            Int16 minutes = 0;
            Int16 hours = 0;

            // Get am or pm and the index of it.
            hourType = hasAm ? amFormat : pmFormat;
            indexOfAmPm = time.IndexOf(hourType);
            
            // Only the hours.
            if (!hasMinutesSeparator)
            {
                string hoursPart = time.Substring(0, indexOfAmPm);
                // Invalid.
                if (!Int16.TryParse(hoursPart, out hours))
                {
                    errors.Add("Hours are invalid.");
                    return TimeSpan.MinValue;
                }
            }
            else
            {
                // Get the hours and minutes.
                string hoursPart = time.Substring(0, indexOfMinutesSeparator);
                string minutesPart = time.Substring(indexOfMinutesSeparator + 1, indexOfAmPm - (indexOfMinutesSeparator + 1));
                if (!Int16.TryParse(hoursPart, out hours))
                {
                    errors.Add("Hours are invalid.");
                    return TimeSpan.MinValue;
                }
                if (!Int16.TryParse(minutesPart, out minutes))
                {
                    errors.Add("Minutes are invalid.");
                    return TimeSpan.MinValue;
                }
            }
            // If pm, add 12 hours as timespan is military time.
            if (hasPm && hours != 12) { hours += 12; }
            return new TimeSpan(hours, minutes, 0);
        }
        #endregion

        
        #region Time Conversion methods
        /// <summary>
        /// Convert military time ( 1530 = 3:30 pm ) to a TimeSpan.
        /// </summary>
        /// <param name="military"></param>
        /// <returns></returns>
        public static TimeSpan ConvertFromMilitaryTime(int military)
        {
            TimeSpan time = TimeSpan.MinValue;
            int hours = military / 100;
            int hour = hours;
            int minutes = military % 100;

            time = new TimeSpan(hours, minutes, 0);
            return time;
        }


        /// <summary>
        /// Converts to military time.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static int ConvertToMilitary(TimeSpan timeSpan)
        {
            return (timeSpan.Hours * 100) + timeSpan.Minutes;
        }
        #endregion


        #region Formatting methods
        /// <summary>
        /// Get the time formatted correctly to exclude the minutes if
        /// there aren't any. Also includes am - pm.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string Format(TimeSpan time)
        {            
            int hours = time.Hours;
            string amPm = hours < 12 ? TimeParserConstants.Am : TimeParserConstants.Pm;

            // Convert military time 13 hours to standard time 1pm
            if (hours > 12)
                hours = hours - 12;

            if (time.Minutes == 0)
                return hours + amPm;

            // Handles 11:10 - 11:59
            if (time.Minutes > 10)
                return hours + ":" + time.Minutes + amPm;

            // Handles 11:01 - 11:09
            return hours + ":0" + time.Minutes + amPm;
        }
        #endregion


        #region Miscellaneous Methods
        /// <summary>
        /// Gets the time as a part of the day.( morning, afternoon, evening ).
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static StartTimeOfDay GetTimeOfDay(TimeSpan time)
        {           
            if (time.Hours < 12) return StartTimeOfDay.Morning;
            if (time.Hours >= 12 && time.Hours <= 16) return StartTimeOfDay.Afternoon;
            return StartTimeOfDay.Evening;
        }


        /// <summary>
        /// Get the time of day ( morning, afternoon, etc. ) from military time.
        /// </summary>
        /// <param name="militaryTime"></param>
        /// <returns></returns>
        public static StartTimeOfDay GetTimeOfDay(int militaryTime)
        {
            return GetTimeOfDay(ConvertFromMilitaryTime(militaryTime));
        }
        #endregion        
    }
}
