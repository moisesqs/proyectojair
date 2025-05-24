using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace TecPurisima.Ecommerce.Api.Services;

public class CommentsService :ICommentsService
{
     private readonly ICommentsRepository _commentsRepository;

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<bool> CommentExists(int id)
    {
        var comment = await _commentsRepository.GetByIdAsync(id);
        return comment != null;
    }

    public async Task<CommentsDto> SaveAsync(CommentsDto commentsDto)
    {
        var comment = new Comments
        {
            Description = commentsDto.Description,
            Content = commentsDto.Content,
            Task_Id = commentsDto.Task_Id,
            Author_Id = commentsDto.Author_Id,
            Created_at = DateTime.Now,
            IsDeleted = false
        };

        comment = await _commentsRepository.SaveAsync(comment);
        commentsDto.Id = comment.Id;

        return commentsDto;
    }

    public async Task<CommentsDto> UpdateAsync(CommentsDto commentsDto)
    {
        var comment = await _commentsRepository.GetByIdAsync(commentsDto.Id);
        if (comment == null)
            throw new Exception("Comment not found");

        comment.Description = commentsDto.Description;
        comment.Content = commentsDto.Content;
        comment.Task_Id = commentsDto.Task_Id;
        comment.Author_Id = commentsDto.Author_Id;
        comment.IsDeleted = false;

        await _commentsRepository.UpdateAsync(comment);
        return commentsDto;
    }

    public async Task<List<CommentsDto>> GetAllAsync()
    {
        var comments = await _commentsRepository.GetAllAsync();
        var commentsDto = comments.Select(c => new CommentsDto(c)).ToList();
        return commentsDto;
    }

    public async Task<CommentsDto> GetByIdAsync(int id)
    {
        var comment = await _commentsRepository.GetByIdAsync(id);
        if (comment == null)
            throw new Exception("Comment not found");
        var commentDto = new CommentsDto(comment);
        return commentDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _commentsRepository.DeleteAsync(id);
    }
}