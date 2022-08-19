using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Enums;

namespace App.AppServices;

public class JobWorkerProposalAppService : IJobWorkerProposalAppService
{
    private readonly IJobRepo _jobService;
    private readonly IJobWorkerProposalRepo _jobWorkerProposalService;

    public JobWorkerProposalAppService(IJobWorkerProposalRepo jobWorkerProposalService, IJobRepo jobService)
    {
        _jobWorkerProposalService = jobWorkerProposalService;
        _jobService = jobService;
    }

    public async Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        return await _jobWorkerProposalService.AddAsync(jobWorkerProposalDto);
    }

    public async Task DeleteAsync(int jobWorkerProposalId)
    {
        var record = await _jobWorkerProposalService.GetByIdAsync(jobWorkerProposalId);

        if ((await _jobService.GetByIdAsync(record.JobId)).JobStatus == JobStatusEnum.RequestedByCostumer)
            await _jobWorkerProposalService.DeleteAsync(jobWorkerProposalId);
        else
            throw new Exception("این پیشنهاد قیمت پذیرفته شده است و قابل حذف نیست");
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

    public async Task AcceptAsync(int jobWorkerProposalId)
    {
        await _jobWorkerProposalService.AcceptAsync(jobWorkerProposalId);
    }
}