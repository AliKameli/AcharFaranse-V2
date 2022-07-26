using App.Domain.Dtos;

namespace App.Domain.Contracts.Service;

public interface ICommentService
{
    Task EnsureExistsByIdAsync(int commentId);
    Task EnsureDoesNotExistAsync(CommentDto commentDto);
    Task<int> AddAsync(CommentDto commentDto);
    Task UpdateAsync(CommentDto commentDto);
    Task DeleteAsync(int commentId);
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto> GetByIdAsync(int commentId);
    Task<List<CommentDto>> GetByJobIdAsync(int jobId);
    Task<List<CommentDto>> GetByCostumerIdAsync(int costumerId);
    Task<List<CommentDto>> GetByWorkerIdAsync(int workerId);
}