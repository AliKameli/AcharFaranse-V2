using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;

namespace App.AppServices;

public class JobWorkerProposalAppService:IJobWorkerProposalAppService
{
    private readonly IJobWorkerProposalService _jobWorkerProposalService;

    public JobWorkerProposalAppService(IJobWorkerProposalService jobWorkerProposalService)
    {
        _jobWorkerProposalService = jobWorkerProposalService;
    }

    public async Task EnsureExistsByIdAsync(int jobWorkerProposalId)
    {
        throw new NotImplementedException();
    }

    public async Task EnsureDoesNotExistAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int jobWorkerProposalId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<JobWorkerProposalDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId)
    {
        throw new NotImplementedException();
    }
}