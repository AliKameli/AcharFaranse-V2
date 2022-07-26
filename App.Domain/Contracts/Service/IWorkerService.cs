using App.Domain.Dtos;

namespace App.Domain.Contracts.Service;

public interface IWorkerService
{
    Task EnsureExistsByIdAsync(int workerId);
    Task EnsureExistsByNationalIdAsync(string nationalId);
    Task EnsureDoesNotExistAsync(string nationalId);
    Task<int> AddAsync(WorkerDto workerDto);
    Task UpdateAsync(WorkerDto workerDto);
    Task DeleteAsync(int workerId);
    Task<List<WorkerDto>> GetAllAsync();
    Task<WorkerDto> GetByIdAsync(int workerId);
    Task<WorkerDto> GetByNationalIdAsync(string nationalId);
    Task<List<WorkerDto>> GetByJobCategoryIdAsync(int jobCategoryId);
    Task<List<WorkerDto>> GetByCityIdAsync(int cityId);
    Task<List<WorkerDto>> SearchAsync(string? name = null, string? nationalId = null);
}