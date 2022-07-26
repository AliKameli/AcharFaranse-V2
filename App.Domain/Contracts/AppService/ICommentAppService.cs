using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface ICommentAppService
{
    Task<int> AddAsync(CommentDto commentDto);
    Task UpdateAsync(CommentDto commentDto);
    Task DeleteAsync(int commentId);
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto> GetByIdAsync(int commentId);
    Task<List<CommentDto>> GetByJobIdAsync(int jobId);
    Task<List<CommentDto>> GetByCostumerIdAsync(int costumerId);
    Task<List<CommentDto>> GetByWorkerIdAsync(int workerId);
    Task ConfirmAsync(int commentId);
}