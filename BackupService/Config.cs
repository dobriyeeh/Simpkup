using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public enum BackupCondition
    {
        AlwaysSchedulePeriod,
        AlwaysAtTheCertainTime,
        SchedulePeriodIfChanged,
        AtTheCertainTimeIfChanged
    }

    public enum BackupMethod
    {
        Archive,
        Copy
    }

    public struct ReactionTime
    {
        public int hour;
        public int minute;

        public TimeSpan TimeLeftBeforeEvent(DateTime eventTime)
        {
            int needHour   = hour   != -1 ? hour   : eventTime.Hour;
            int needMinute = minute != -1 ? minute : eventTime.Minute;

            DateTime reaction;

            if ((eventTime.Hour < needHour) || 
                ((eventTime.Hour == needHour) && (eventTime.Minute < needMinute)))
            {
                reaction = new DateTime(eventTime.Year, eventTime.Month, eventTime.Day, needHour, needMinute, 0);
            }
            else
            {
                var oneDay = TimeSpan.FromDays(1);
                var nextDate = eventTime + oneDay;

                reaction = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, needHour, needMinute, 0);
            }

            var result = reaction - eventTime;

            return result;
        }
    }

    public struct Config
    {
        public string           backupFromPath;
        public string           backupToPath;

        public BackupCondition  backupCondition;
        public BackupMethod     backupMethod;

        public TimeSpan         schedulePeriod;
        public ReactionTime     reactionTime;

        public bool             usePassword;
        public string           password;
    }
}
