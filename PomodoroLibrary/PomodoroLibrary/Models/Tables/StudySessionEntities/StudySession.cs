﻿using PomodoroLibrary.Models.Identity;
using PomodoroLibrary.Models.Tables.StudyTypeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroLibrary.Models.Tables.StudySessionEntities;

public class StudySession
{
    public int Id { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime Started { get; set; }
    public DateTime Ended { get; set; }
    public StudyType StudyType { get; set; }
}
