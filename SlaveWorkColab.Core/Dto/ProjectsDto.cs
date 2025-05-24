using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Core.Dto;
public class ProjectsDto : DtoBase 
{
        public string Name { get; set; }
        public string Description { get; set; }

        public ProjectsDto()
        {
            
        }

        public ProjectsDto(Projects project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
        }
}
    