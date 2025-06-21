using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Exceptions
{
    public class GradeExceptions(double value) : Exception($"Grade value {value} is invalid. Must be between 1.0 and 10.0.")
    {
    }
}
