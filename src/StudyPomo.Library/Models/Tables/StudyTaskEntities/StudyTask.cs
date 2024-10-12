using StudyPomo.Library.Models.Identity;
using StudyPomo.Library.Models.Tables.CourseEntities;
using StudyPomo.Library.Models.Tables.LabelEntities;
using StudyPomo.Library.Models.Tables.TaskPriorityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPomo.Library.Models.Tables.StudyTaskEntities;

public class StudyTask
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateCompleted { get; set; }
    public int? TaskPriorityId { get; set; }
    public TaskPriority? TaskPriority { get; set; }
    public bool Archived { get; set; }
    public DateTime? Deadline { get; set; }
    public int? CourseId { get; set; }
    public Course? Course { get; set; }
    /// <summary>
    /// If not null, implies this is a subtask to another parent task.
    /// </summary>
    public int? ParentId { get; set; }
    public List<TaskLabel> TaskLabels { get; } = [];
}
