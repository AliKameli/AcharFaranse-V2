using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface IJobWorkerProposalAppService
{
    Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto);
    Task DeleteAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetAllAsync();
    Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId);
    Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId);
    Task<List<JobWorkerProposalDto>> GetByWorkerIdAsync(int workerId);
    Task AcceptAsync(int jobWorkerProposalId);
}