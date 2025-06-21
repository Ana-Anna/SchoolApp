using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Exceptions
{
    public class ScheduleExceptions:Exception
    {
        public ScheduleExceptions() : base("An error occurred with the schedule operation.") { }
        public ScheduleExceptions(string message) : base(message) { }
        public ScheduleExceptions(string message, Exception innerException) : base(message, innerException) { }
        public static ScheduleExceptions EntryNotFound(Guid entryId)
        {
            return new ScheduleExceptions($"Schedule entry with ID {entryId} not found.");
        }
        public static ScheduleExceptions InvalidEntryData(string message)
        {
            return new ScheduleExceptions(message);
        }
    }
}
