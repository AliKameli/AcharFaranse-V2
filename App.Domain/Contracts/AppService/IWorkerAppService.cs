using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface IWorkerAppService
{
    Task<int> AddAsync(WorkerDto workerDto);
    Task UpdateAsync(WorkerDto workerDto);
    Task DeleteAsync(int workerId);
    Task<List<WorkerDto>> GetAllAsync();
    Task<WorkerDto> GetByIdAsync(int workerId);
    Task<WorkerDto> GetByNationalIdAsync(string nationalId);
    Task<List<WorkerDto>> GetByJobCategoryIdAsync(int jobCategoryId);
    Task<List<WorkerDto>> GetByCityIdAsync(int cityId);
    Task<List<WorkerDto>> SearchAsync(string? name = null, string? nationalId = null);
    Task ConfirmAsync(int workerId);
    Task EditPictureAsync(WorkerDto workerDto);
}