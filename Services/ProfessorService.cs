using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Exceptions;
using SchoolApp.Models;
using SchoolApp.Repositories;

namespace SchoolApp.Services
{
    public class ProfessorService(InMemoryRepository<Professor> professorRepository)
    {
        private readonly InMemoryRepository<Professor> _professorRepository = professorRepository;
        public void AddProfessor(Professor professor)
        {
            ArgumentNullException.ThrowIfNull(professor);
            _professorRepository.Add(professor);
        }

        public void RemoveProfessor(Guid professorId)
        {
            _professorRepository.Remove(professorId);
        }

        public Professor GetProfessorById(Guid professorId)
        {
            var professor = _professorRepository.GetById(professorId);
            return professor ?? throw ProfessorExceptions.ProfessorNotFound(professorId);
        }


        public IEnumerable<Professor> GetAllProfessors()
        {
            return _professorRepository.GetAll();
        }

        public void UpdateProfessor(Guid professorId, Professor updateProfessor)
        {
            ArgumentNullException.ThrowIfNull(updateProfessor);
            if (professorId != updateProfessor.ProfessorId) throw ProfessorExceptions.InvalidProfessorData("Professor ID mismatch.");
            _ = _professorRepository.GetById(professorId) ?? throw ProfessorExceptions.ProfessorNotFound(professorId);
            _professorRepository.Update(professorId, updateProfessor);
        }
    }
}
