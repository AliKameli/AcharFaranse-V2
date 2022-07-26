using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface IJobPictureAppService
{
    Task<int> AddAsync(JobPictureDto jobPictureDto);
    Task UpdateAsync(JobPictureDto jobPictureDto);
    Task DeleteAsync(int jobPictureId);
    Task<List<JobPictureDto>> GetAllAsync();
    Task<JobPictureDto> GetByIdAsync(int jobPictureId);
    Task<List<JobPictureDto>> GetByJobIdAsync(int jobId);
    Task<List<JobPictureDto>> GetByCostumerIdAsync(int costumerId);
    Task<List<JobPictureDto>> GetByWorkerIdAsync(int workerId);
    Task ConfirmAsync(int jobPictureId);
}