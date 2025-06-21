using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Models;
using SchoolApp.Repositories;
using SchoolApp.Exceptions;

namespace SchoolApp.Services
{
    public class StudentService(IRepository<Student> studentRepository)
    {
        private readonly IRepository<Student> _studentRepository = studentRepository;

        public void AddStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            _studentRepository.Add(student);
        }

        public void RemoveStudent(Guid studentId)
        {
            _studentRepository.Remove(studentId);
        }

        public Student GetStudentById(Guid studentId)
        {
            var student = _studentRepository.GetById(studentId);

            return student ?? throw StudentExceptions.StudentNotFound(studentId);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public void UpdateStudent(Guid studentId, Student updatedStudent)
        {
            ArgumentNullException.ThrowIfNull(updatedStudent);
            if (studentId != updatedStudent.StudentId) throw  StudentExceptions.InvalidStudentData("Student ID mismatch.");
            _ = _studentRepository.GetById(studentId) ?? throw StudentExceptions.StudentNotFound(studentId);
            _studentRepository.Update(studentId, updatedStudent);
        }

        public void AddGradeToStudent(Guid studentId, Grade grade)
        {
            ArgumentNullException.ThrowIfNull(grade);
            var student = _studentRepository.GetById(studentId) ?? throw StudentExceptions.StudentNotFound(studentId);
            student.Grades.Add(grade);
            _studentRepository.Update(studentId, student);
        }

        public double GetStudentAverageGrade(Guid studentId)
        {
            var student = _studentRepository.GetById(studentId) ?? throw StudentExceptions.StudentNotFound(studentId);
            return student.AverageGrade;
        }
    }
}
