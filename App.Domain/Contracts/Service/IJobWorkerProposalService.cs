using App.Domain.Dtos;

namespace App.Domain.Contracts.Service;

public interface IJobWorkerProposalService
{
    Task EnsureExistsByIdAsync(int jobWorkerProposalId);
    Task EnsureDoesNotExistAsync(JobWorkerProposalDto jobWorkerProposalDto);
    Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto);
    Task DeleteAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetAllAsync();
    Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId);
    Task<List<JobWorkerProposalDto>> GetByWorkerIdAsync(int workerId);
}