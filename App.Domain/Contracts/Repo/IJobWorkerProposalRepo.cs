using App.Domain.Dtos;

namespace App.Domain.Contracts.Repo;

public interface IJobWorkerProposalRepo
{
    Task EnsureExistsByIdAsync(int jobWorkerProposalId);
    Task EnsureDoesNotExistAsync(JobWorkerProposalDto jobWorkerProposalDto);
    Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto);
    Task DeleteAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetAllAsync();
    Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId);
    Task<List<JobWorkerProposalDto>> GetByWorkerIdAsync(int workerId);
    Task AcceptAsync(int jobWorkerProposalId);
}