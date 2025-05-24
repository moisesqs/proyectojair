using System;
using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Core.Dto;
public class CommentsDto : DtoBase
{
        public string Description { get; set; }
        public string Content { get; set; }
        public int Task_Id { get; set; }
        public int Author_Id { get; set; }

        public CommentsDto() { }

        public CommentsDto(Comments comment)
        {
            Id = comment.Id;
            Description = comment.Description;
            Content = comment.Content;
            Task_Id = comment.Task_Id;
            Author_Id = comment.Author_Id;

        }
}
