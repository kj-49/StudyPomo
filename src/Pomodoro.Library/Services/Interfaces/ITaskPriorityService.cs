﻿using Pomodoro.Library.Models.Tables.TaskPriorityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Library.Services.Interfaces;

public interface ITaskPriorityService
{
    Task<ICollection<TaskPriority>> GetAllAsync();
}