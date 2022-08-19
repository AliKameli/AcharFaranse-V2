using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;

namespace App.AppServices;

public class JobCategoryAppService : IJobCategoryAppService
{
    private readonly IJobCategoryRepo _jobCategoryService;

    public JobCategoryAppService(IJobCategoryRepo jobCategoryService)
    {
        _jobCategoryService = jobCategoryService;
    }

    public async Task<int> AddAsync(JobCategoryDto jobCategoryDto)
    {
        return await _jobCategoryService.AddAsync(jobCategoryDto);
    }

    public async Task UpdateAsync(JobCategoryDto jobCategoryDto)
    {
        var recrod = await _jobCategoryService.GetByIdAsync(jobCategoryDto.Id);
        jobCategoryDto.ParentJobCategoryId = recrod.ParentJobCategoryId;
        await _jobCategoryService.UpdateAsync(jobCategoryDto);
    }

    public async Task DeleteAsync(int jobCategoryId)
    {
        await _jobCategoryService.DeleteAsync(jobCategoryId);
    }

    public async Task<List<JobCategoryDto>> GetAllAsync()
    {
        return await _jobCategoryService.GetAllAsync();
    }

    public async Task<JobCategoryDto> GetByIdAsync(int jobCategoryId)
    {
        return await _jobCategoryService.GetByIdAsync(jobCategoryId);
    }

    public async Task<List<JobCategoryDto>> GetByParentIdAsync(int? parentId)
    {
        return await _jobCategoryService.GetByParentIdAsync(parentId);
    }

    public async Task<List<JobCategoryDto>> GetByWorkerIdAsync(int workerId)
    {
        return await _jobCategoryService.GetByWorkerIdAsync(workerId);
    }
}