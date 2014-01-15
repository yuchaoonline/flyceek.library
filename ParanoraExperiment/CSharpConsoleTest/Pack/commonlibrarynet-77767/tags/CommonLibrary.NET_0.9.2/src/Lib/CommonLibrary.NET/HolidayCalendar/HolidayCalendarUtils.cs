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

namespace CommonLibrary
{
    /// <summary>
    /// Utility class for holidays.
    /// </summary>
    public class HolidayCalendarUtils
    {
        /// <summary>
        /// Converts the holiday items into DateTime objects for the specified year.
        /// </summary>
        /// <param name="holidays"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<DateTime> InterpretHolidays(List<HolidayItem> holidays, int year)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (HolidayItem holiday in holidays)
            {
                if (holiday.IsHardDay)
                    dates.Add(new DateTime(year, holiday.Month, holiday.Day));
                else
                {
                    // Now calculate.
                    // TO_DO: Interpret the 3rd Thursday of November for ThanskGiving.
                }

            }
            return dates;
        }


        /// <summary>
        /// Get United States holidays.
        /// </summary>
        /// <returns></returns>
        public static List<HolidayItem> GetUnitedStatesHolidays()
        {
            // This can be loaded from the database.
            List<HolidayItem> holidays = new List<HolidayItem>()
            {
                new HolidayItem(1, 1, true, DayOfWeek.Monday, -1, "New Years"),
                new HolidayItem(1, 19, true, DayOfWeek.Monday, -1, "Martin Luther King"),
                new HolidayItem(2, 16, true, DayOfWeek.Monday, -1, "Washingtons Birthday"),
                new HolidayItem(5, 25, true, DayOfWeek.Monday, -1, "Memorial Day"),
                new HolidayItem(7, 4, true, DayOfWeek.Monday, -1, "Independence Day"),
                new HolidayItem(9, 7, true, DayOfWeek.Monday, -1, "Labor Day"),
                new HolidayItem(10, 12, true, DayOfWeek.Monday, -1, "Columbus Day"),
                new HolidayItem(11, 11, true, DayOfWeek.Monday, -1, "Veterans Day"),
                new HolidayItem(11, 26, true, DayOfWeek.Thursday, 4, "Thanks Giving"),
                new HolidayItem(12, 25, true, DayOfWeek.Monday, -1, "Christmas Day")
            };
            return holidays;
        }
    }
}
