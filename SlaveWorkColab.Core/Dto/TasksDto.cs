using Task = SlaveWorkColab.Core.Entities.Tasks;
using TaskStatus = SlaveWorkColab.Core.Entities.TaskStatus;

namespace SlaveWorkColab.Core.Dto;
public class TasksDto : DtoBase 
{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Project_Id { get; set; }
        public int? Assigned_to { get; set; }


        public TasksDto() { }

        public TasksDto(Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
            Project_Id = task.Project_Id;
            Assigned_to = task.Assigned_to;

        }
}