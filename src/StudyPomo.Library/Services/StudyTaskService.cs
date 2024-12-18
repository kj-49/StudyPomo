﻿using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StudyPomo.Library.Data.Interfaces;
using StudyPomo.Library.Models.Identity;
using StudyPomo.Library.Models.Tables.LabelEntities;
using StudyPomo.Library.Models.Tables.StudyTaskEntities;
using StudyPomo.Library.Models.Tables.StudyTaskLabelEntities;
using StudyPomo.Library.Models.Tables.TaskPriorityEntities;
using StudyPomo.Library.Models.Utility;
using StudyPomo.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPomo.Library.Services;

public class StudyTaskService : IStudyTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public StudyTaskService(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task CreateAsync(StudyTaskCreate studyTaskCreate)
    {
        ApplicationUser? user = await _userService.GetCurrentUserAsync();
        if (user == null) throw new Exception("User not found");

        TaskPriority? taskPriority = await _unitOfWork.TaskPriority.GetAsync(u => u.Id == studyTaskCreate.TaskPriorityId);

        StudyTask studyTask = studyTaskCreate.ToEntity(user.Id, TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId ?? SD.UTC));

        // Now add labels to task
        if (studyTaskCreate.TaskLabelIds != null && studyTaskCreate.TaskLabelIds.Any())
        {
            // Fetch all labels at once
            var labels = await _unitOfWork.TaskLabel.GetAllAsync(u => studyTaskCreate.TaskLabelIds.Contains(u.Id));

            // Create the StudyTaskLabel entities
            var studyTaskLabels = labels.Select(label => new StudyTaskLabel
            {
                StudyTask = studyTask,
                TaskLabel = label
            });

            // Add all StudyTaskLabels in one go
            await _unitOfWork.StudyTaskLabel.AddRangeAsync(studyTaskLabels);
        }

        await _unitOfWork.StudyTask.AddAsync(studyTask);

        _unitOfWork.Complete();
    }

    public async Task RemoveAsync(int id)
    {
        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(u => u.Id == id);
        if (studyTask == null) throw new Exception("Study Task not found");

        _unitOfWork.StudyTask.Remove(studyTask);
        _unitOfWork.Complete();
    }

    public async Task ArchiveAsync(int id)
    {
        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(u => u.Id == id);
        if (studyTask == null) throw new Exception("Study Task not found");
        studyTask.Archived = true;

        _unitOfWork.StudyTask.Update(studyTask);
        _unitOfWork.Complete();
    }

    public async Task UpdateAsync(StudyTaskUpdate studyTaskUpdate)
    {
        ApplicationUser user = await _userService.GetCurrentUserAsync();

        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(u => u.Id == studyTaskUpdate.Id);
        if (studyTask == null) throw new Exception("Study Task not found");

        StudyTask updatedStudyTask = studyTaskUpdate.ToEntity(TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId ?? SD.UTC), studyTask);

        await RemoveAllLabels(studyTask.Id);

        // Now add labels to task
        if (studyTaskUpdate.TaskLabelIds != null && studyTaskUpdate.TaskLabelIds.Any())
        {
            var labels = await _unitOfWork.TaskLabel.GetAllAsync(u => studyTaskUpdate.TaskLabelIds.Contains(u.Id));

            var studyTaskLabels = labels.Select(label => new StudyTaskLabel
            {
                StudyTaskId = studyTask.Id,
                TaskLabelId = label.Id
            });

            await _unitOfWork.StudyTaskLabel.AddRangeAsync(studyTaskLabels);
        }

        _unitOfWork.StudyTask.Update(updatedStudyTask);
        _unitOfWork.Complete();
    }


    public async Task RemoveAllLabels(int studyTaskId)
    
    {
        IEnumerable<StudyTaskLabel> studyTaskLabels = await _unitOfWork.StudyTaskLabel.GetAllAsync(u => u.StudyTask.Id == studyTaskId);

        _unitOfWork.StudyTaskLabel.RemoveRange(studyTaskLabels);

        _unitOfWork.Complete();
    }

    public async Task CompleteAsync(int id)
    {
        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(u => u.Id == id);

        if (studyTask == null) throw new Exception("Study Task not found");

        studyTask.Completed = true;
        studyTask.DateCompleted = DateTime.UtcNow;

        _unitOfWork.StudyTask.Update(studyTask);
        _unitOfWork.Complete();
    }

    public async Task UncompleteAsync(int id)
    {
        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(u => u.Id == id);

        if (studyTask == null) throw new Exception("Study Task not found");

        studyTask.Completed = false;
        studyTask.DateCompleted = null;

        _unitOfWork.StudyTask.Update(studyTask);
        _unitOfWork.Complete();
    }

    public async Task<ICollection<StudyTask>> GetAllAsync(int userId, bool includeArchived = false)
    {
        if (includeArchived)
        {
            IEnumerable<StudyTask> studyTasks = await _unitOfWork.StudyTask.GetAllAsync(
                u => u.User.Id == userId,
                t => t.TaskPriority,
                t => t.User,
                t => t.TaskLabels,
                t => t.Course
            );
            return studyTasks.ToList();
        } else
        {
            IEnumerable<StudyTask> studyTasks = await _unitOfWork.StudyTask.GetAllAsync(
                u => u.User.Id == userId && !u.Archived,
                t => t.TaskPriority,
                t => t.User,
                t => t.TaskLabels,
                t => t.Course
            );
            return studyTasks.ToList();
        }
    }

    public async Task<StudyTask> GetAsync(int id)
    {
        StudyTask? studyTask = await _unitOfWork.StudyTask.GetAsync(
            u => u.Id == id,
            t => t.TaskLabels
        );

        if (studyTask == null)
        {
            throw new Exception("Study Task not found");
        }

        return studyTask;
    }
}
