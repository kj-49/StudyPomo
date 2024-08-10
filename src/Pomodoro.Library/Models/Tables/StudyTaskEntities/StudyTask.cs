﻿using Pomodoro.Library.Models.Identity;
using Pomodoro.Library.Models.Tables.CourseEntities;
using Pomodoro.Library.Models.Tables.LabelEntities;
using Pomodoro.Library.Models.Tables.TaskPriorityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Library.Models.Tables.StudyTaskEntities;

public class StudyTask
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Name { get; set; }
    public bool Completed { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateCompleted { get; set; }
    public int? TaskPriorityId { get; set; }
    public TaskPriority? TaskPriority { get; set; }
    public bool Archived { get; set; }
    public DateTime? Deadline { get; set; }
    public int? CourseId { get; set; }
    public Course? Course { get; set; }

    public List<TaskLabel> TaskLabels { get; } = [];
}