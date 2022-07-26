using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;

namespace App.AppServices;

public class JobAppService : IJobAppService
{
    private readonly ICityService _cityService;
    private readonly ICostumerAddressService _costumerAddressService;
    private readonly ICostumerService _costumerService;
    private readonly IJobService _jobService;
    private readonly IWorkerService _workerService;

    public JobAppService(IJobService jobService,
        ICostumerService costumerService,
        ICostumerAddressService costumerAddressService,
        ICityService cityService,
        IWorkerService workerService)
    {
        _jobService = jobService;
        _costumerService = costumerService;
        _costumerAddressService = costumerAddressService;
        _cityService = cityService;
        _workerService = workerService;
    }


    public async Task<int> AddAsync(JobDto jobDto)
    {
        await _costumerService.EnsureExistsByIdAsync(jobDto.CostumerId);
        await _costumerAddressService.EnsureExistsByIdAsync(jobDto.CostumerAddressId);
        await _cityService.EnsureExistsByIdAsync(jobDto.JobCityId);
        return await _jobService.AddAsync(jobDto);
    }

    public async Task DeleteAsync(int jobId)
    {
        await _jobService.DeleteAsync(jobId);
    }

    public async Task<List<JobDto>> GetAllAsync()
    {
        return await _jobService.GetAllAsync();
    }

    public async Task<JobDto> GetByIdAsync(int jobId)
    {
        return await _jobService.GetByIdAsync(jobId);
    }

    public async Task<List<JobDto>> GetByJobCategoryIdAsync(int jobCategoryId)
    {
        return await _jobService.GetByJobCategoryIdAsync(jobCategoryId);
    }

    public async Task<List<JobDto>> GetByCostumerIdAsync(int costumerId)
    {
        return await _jobService.GetByCostumerIdAsync(costumerId);
    }

    public async Task<List<JobDto>> GetByWorkerIdAsync(int workerId)
    {
        return await _jobService.GetByWorkerIdAsync(workerId);
    }

    public async Task<List<JobDto>> GetByCityIdAsync(int cityId)
{
    return await _jobService.GetByCityIdAsync(cityId);
    }
}