﻿using PomodoroLibrary.Models.Tables.StudyTaskEntities;

namespace PomodoroLibrary.Services.Interfaces
{
    public interface IStudyTaskService
    {
        Task CreateAsync(StudyTaskCreate studyTaskCreate);
        Task RemoveAsync(int id);
        Task UpdateAsync(StudyTaskUpdate studyTaskUpdate);
        Task CompleteAsync(int id);
        Task UncompleteAsync(int id);
        Task<ICollection<StudyTask>> GetAllAsync(int userId);
    }
}