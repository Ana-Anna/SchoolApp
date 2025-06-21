using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Exceptions
{
    public class ProfessorExceptions : Exception
    {
        public ProfessorExceptions() : base("An error occurred with the professor operation.") { }
        public ProfessorExceptions(string message) : base(message) { }
        public ProfessorExceptions(string message, Exception innerException) : base(message, innerException) { }
        public static ProfessorExceptions ProfessorNotFound(Guid professorId)
        {
            return new ProfessorExceptions($"Professor with ID {professorId} not found.");
        }
        public static ProfessorExceptions InvalidProfessorData(string message)
        {
            return new ProfessorExceptions(message);
        }
    }
}
