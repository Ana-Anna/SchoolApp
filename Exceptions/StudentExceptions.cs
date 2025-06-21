using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Exceptions
{
    public class StudentExceptions:Exception
    {
        public StudentExceptions() : base("An error occurred with the student operation.") { }
        public StudentExceptions(string message) : base(message) { }
        public StudentExceptions(string message, Exception innerException) : base(message, innerException) { }
        public static StudentExceptions StudentNotFound(Guid studentId)
        {
            return new StudentExceptions($"Student with ID {studentId} not found.");
        }
        public static StudentExceptions InvalidStudentData(string message)
        {
            return new StudentExceptions(message);
        }
    }
}
