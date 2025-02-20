﻿
using StudyPomo.Library.Models.Tables.StudyTaskEntities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudyPomo.Library.Data.Interfaces;

public interface IStudyTaskRepository : IRepository<StudyTask>
{
    void Update(StudyTask model);
}
