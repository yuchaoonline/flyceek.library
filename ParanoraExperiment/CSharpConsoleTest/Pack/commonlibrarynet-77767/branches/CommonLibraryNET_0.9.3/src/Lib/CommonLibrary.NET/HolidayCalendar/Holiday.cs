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

namespace ComLib.Calendars
{    
    /// <summary>
    /// Initialize.
    /// </summary>
    public class Holiday
    {
        public int Month;
        public int Day;
        public bool IsHardDay;
        public DayOfWeek DayOfTheWeek;
        public int WeekOfMonth;
        public string Description;


        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="isHardDay"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="weekOfMonth"></param>
        public Holiday(int month, int day, bool isHardDay, DayOfWeek dayOfWeek, int weekOfMonth, string holidayDescription)
        {
            Month = month;
            Day = day;
            IsHardDay = isHardDay;
            DayOfTheWeek = dayOfWeek;
            WeekOfMonth = weekOfMonth;
            Description = holidayDescription;
        }
    }
}
