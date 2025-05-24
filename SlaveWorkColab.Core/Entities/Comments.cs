using System;
using SlaveWorkColab.Core.Entities;

//namespace SlaveWorkColab.Core.Entitites;

public class Comments : EntityBase
{
        public string Description { get; set; }
        public string Content { get; set; }
        public int Task_Id { get; set; }
        public int Author_Id { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
}