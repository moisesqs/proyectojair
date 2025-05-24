using System;
using Dapper.Contrib.Extensions;

namespace SlaveWorkColab.Core.Entities;

[Table("Tasks")]
public class Tasks : EntityBase
{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public int Project_Id { get; set; }
        public int? Assigned_to { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now; 
}
