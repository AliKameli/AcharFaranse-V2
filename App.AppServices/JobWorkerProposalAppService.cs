using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;

namespace App.AppServices;

public class JobWorkerProposalAppService : IJobWorkerProposalAppService
{
    private readonly IJobWorkerProposalService _jobWorkerProposalService;

    public JobWorkerProposalAppService(IJobWorkerProposalService jobWorkerProposalService)
    {
        _jobWorkerProposalService = jobWorkerProposalService;
    }

    public async Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        return await _jobWorkerProposalService.AddAsync(jobWorkerProposalDto);
    }

    public async Task DeleteAsync(int jobWorkerProposalId)
    {
        await _jobWorkerProposalService.DeleteAsync(jobWorkerProposalId);
    }

    public async Task<List<JobWorkerProposalDto>> GetAllAsync()
    {
        return await _jobWorkerProposalService.GetAllAsync();
    }

    public async Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId)
    {
        return await _jobWorkerProposalService.GetByIdAsync(jobWorkerProposalId);
    }

    public async Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId)
    {
        return await _jobWorkerProposalService.GetByJobIdAsync(jobId);
    }

    public async Task<List<JobWorkerProposalDto>> GetByWorkerIdAsync(int workerId)
    {
        return await _jobWorkerProposalService.GetByWorkerIdAsync(workerId);
    }
}