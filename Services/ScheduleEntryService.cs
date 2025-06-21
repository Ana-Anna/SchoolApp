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
    public class ScheduleEntryService(IRepository<ScheduleEntry> scheduleEntryRepository)
    {
        private readonly IRepository<ScheduleEntry> _scheduleEntryRepository = scheduleEntryRepository;
        public void AddScheduleEntry(ScheduleEntry entry)
        {
            ArgumentNullException.ThrowIfNull(entry);
            _scheduleEntryRepository.Add(entry);
        }
        public void RemoveScheduleEntry(Guid entryId)
        {
            _scheduleEntryRepository.Remove(entryId);
        }
        public ScheduleEntry GetScheduleEntryById(Guid entryId)
        {
            var entry = _scheduleEntryRepository.GetById(entryId);
            return entry ?? throw ScheduleExceptions.EntryNotFound(entryId);
        }
        public IEnumerable<ScheduleEntry> GetAllScheduleEntries()
        {
            return _scheduleEntryRepository.GetAll();
        }
        public void UpdateScheduleEntry(Guid entryId, ScheduleEntry updatedEntry)
        {
            ArgumentNullException.ThrowIfNull(updatedEntry);
            if (entryId != updatedEntry.EntryId) throw ScheduleExceptions.InvalidEntryData("Schedule entry ID mismatch.");
            _ = _scheduleEntryRepository.GetById(entryId) ?? throw ScheduleExceptions.EntryNotFound(entryId);
            _scheduleEntryRepository.Update(entryId, updatedEntry);
        }

        public void AddStudentToScheduleEntry(Guid entryId, Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            var entry = _scheduleEntryRepository.GetById(entryId) ?? throw ScheduleExceptions.EntryNotFound(entryId);
            if (entry.Students.Contains(student)) return; 
            entry.Students.Add(student);
            _scheduleEntryRepository.Update(entryId, entry);
        }

        public void RemoveStudentFromScheduleEntry(Guid entryId, Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            var entry = _scheduleEntryRepository.GetById(entryId) ?? throw ScheduleExceptions.EntryNotFound(entryId);
            if (!entry.Students.Contains(student)) return;
            entry.Students.Remove(student);
            _scheduleEntryRepository.Update(entryId, entry);
        }

        public IEnumerable<ScheduleEntry> GetSchedulesForDay(DateTime day)
        {
            var entries = _scheduleEntryRepository.GetAll()
                .Where(e => e.StartTime.Date == day.Date)
                .OrderBy(e => e.StartTime)
                .ToList();

            if (entries.Count == 0)
            {
                throw ScheduleExceptions.InvalidEntryData ("No schedule entries found for the specified date.");
            }

            return entries;
        }
    }
 }
