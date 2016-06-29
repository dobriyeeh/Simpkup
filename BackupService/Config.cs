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
        public int second;

        public static ReactionTime Defalt => new ReactionTime() { hour = -1, minute = -1, second = -1 };

        public TimeSpan TimeLeftBeforeEvent(DateTime fromTime)
        {
            int needHour   = hour   != -1 ? hour   : fromTime.Hour;
            int needMinute = minute != -1 ? minute : fromTime.Minute;
            int needSecond = second != -1 ? second : fromTime.Second; 

            DateTime reaction;

            if ((fromTime.Hour < needHour) || 
                ((fromTime.Hour == needHour) && (fromTime.Minute < needMinute)) ||
                ((fromTime.Hour == needHour) && (fromTime.Minute == needMinute) && (fromTime.Second < needSecond)))
            {
                reaction = new DateTime(fromTime.Year, fromTime.Month, fromTime.Day, needHour, needMinute, needSecond);
            }
            else
            {
                var oneDay = TimeSpan.FromDays(1);
                var nextDate = fromTime + oneDay;

                reaction = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, needHour, needMinute, needSecond);
            }

            var result = reaction - fromTime;

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
