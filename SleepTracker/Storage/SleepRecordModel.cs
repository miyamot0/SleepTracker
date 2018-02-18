using System;
using SQLite;

namespace SleepTracker.Storage
{
    public class SleepRecordModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // yyyyMMdd
        public string Date { get; set; }

        public int Type { get; set; }
        public int Order { get; set; }

        public int Lower { get; set; }
        public int Upper { get; set; }

        // yyyyMMddHHmmss
        public string DateSaved { get; set; }
    }
}
