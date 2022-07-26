using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface IJobAppService
{
    Task<int> AddAsync(JobDto jobDto);
    Task DeleteAsync(int jobId);
    Task<List<JobDto>> GetAllAsync();
    Task<JobDto> GetByIdAsync(int jobId);
    Task<List<JobDto>> GetByJobCategoryIdAsync(int jobCategoryId);
    Task<List<JobDto>> GetByCostumerIdAsync(int costumerId);
    Task<List<JobDto>> GetByWorkerIdAsync(int workerId);
    Task<List<JobDto>> GetByCityIdAsync(int cityId);
}