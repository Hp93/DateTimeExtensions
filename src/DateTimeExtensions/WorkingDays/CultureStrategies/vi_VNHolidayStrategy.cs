﻿#region License

// 
// Copyright (c) 2011-2012, João Matos Silva <kappy@acydburne.com.pt>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

#endregion

using DateTimeExtensions.Common;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace DateTimeExtensions.WorkingDays.CultureStrategies
{
    [Locale("vi-VN")]
    public class ViVNHolidayStrategy : HolidayStrategyBase
    {
        private static readonly Calendar LunisolarCalendar = new ChineseLunisolarCalendar();

        public ViVNHolidayStrategy()
        {
            this.InnerHolidays.Add(HungKingsCommemorations);
            this.InnerHolidays.Add(LiberationDay);
            this.InnerHolidays.Add(InternationalWorkersDay);
            this.InnerHolidays.Add(NationalDay);
            this.InnerHolidays.Add(NewYear);
        }

        protected override IDictionary<DateTime, Holiday> BuildObservancesMap(int year)
        {
            IDictionary<DateTime, Holiday> holidayMap = new Dictionary<DateTime, Holiday>();
            foreach (var innerHoliday in InnerHolidays)
            {
                var date = innerHoliday.GetInstance(year);
                if (date.HasValue)
                {
                    //if the holiday is a saturday, the holiday is observed on previous friday
                    switch (date.Value.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            holidayMap.Add(date.Value.AddDays(-1), innerHoliday);
                            break;
                        case DayOfWeek.Sunday:
                            holidayMap.Add(date.Value.AddDays(1), innerHoliday);
                            break;
                        default:
                            holidayMap.Add(date.Value, innerHoliday);
                            break;
                    }
                }
            }
            return holidayMap;
        }

        private static Holiday hungKingsCommemorations;
        public static Holiday HungKingsCommemorations
        {
            get
            {
                if (hungKingsCommemorations == null)
                {
                    hungKingsCommemorations = new FixedHoliday("HungKingsCommemorations", 3, 10, LunisolarCalendar);
                }
                return hungKingsCommemorations;
            }
        }

        private static Holiday liberationDay;
        public static Holiday LiberationDay
        {
            get
            {
                if (liberationDay == null)
                {
                    liberationDay = new FixedHoliday("LiberationDay", 4, 30);
                }
                return liberationDay;
            }
        }

        private static Holiday internationalWorkersDay;
        public static Holiday InternationalWorkersDay
        {
            get
            {
                if (internationalWorkersDay == null)
                {
                    internationalWorkersDay = new FixedHoliday("InternationalWorkersDay", 5, 1);
                }
                return internationalWorkersDay;
            }
        }

        private static Holiday nationalDay;
        public static Holiday NationalDay
        {
            get
            {
                if (nationalDay == null)
                {
                    nationalDay = new FixedHoliday("NationalDay", 9, 2);
                }
                return nationalDay;
            }
        }

        private static Holiday newYear;
        public static Holiday NewYear
        {
            get
            {
                if (newYear == null)
                {
                    newYear = new FixedHoliday("NewYear", 1, 1);
                }
                return newYear;
            }
        }
    }
}