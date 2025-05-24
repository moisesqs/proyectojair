using SlaveWorkColab.Core.Dto;

namespace TecPurisima.Ecommerce.Api.Services.Interfaces;

public interface ICommentsService
{
    Task<bool>CommentExists(int id);
    Task<CommentsDto> SaveAsync(CommentsDto commentsDto);
    Task<CommentsDto> UpdateAsync(CommentsDto commentsDto);
    Task<List<CommentsDto>> GetAllAsync();
    Task<CommentsDto> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}