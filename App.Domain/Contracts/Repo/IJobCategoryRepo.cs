using App.Domain.Dtos;

namespace App.Domain.Contracts.Repo;

public interface IJobCategoryRepo
{
    Task EnsureExistsByIdAsync(int jobCategoryId);
    Task EnsureExistsByNameAsync(string jobCategoryName);
    Task EnsureDoesNotExistAsync(JobCategoryDto jobCategoryDto);
    Task<int> AddAsync(JobCategoryDto jobCategoryDto);
    Task UpdateAsync(JobCategoryDto jobCategoryDto);
    Task DeleteAsync(int jobCategoryId);
    Task<List<JobCategoryDto>> GetAllAsync();
    Task<JobCategoryDto> GetByIdAsync(int jobCategoryId);
    Task<List<JobCategoryDto>> GetByParentIdAsync(int? parentId);
    Task<List<JobCategoryDto>> GetByWorkerIdAsync(int workerId);
}