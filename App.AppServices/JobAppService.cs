﻿using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class JobAppService : IJobAppService
{
    private readonly ICityRepo _cityService;
    private readonly ICostumerAddressRepo _costumerAddressService;
    private readonly ICostumerRepo _costumerService;
    private readonly IJobPictureAppService _jobPictureAppService;
    private readonly IJobRepo _jobService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerRepo _workerService;

    public JobAppService(IJobRepo jobService,
        ICostumerRepo costumerService,
        ICostumerAddressRepo costumerAddressService,
        ICityRepo cityService,
        IWorkerRepo workerService,
        UserManager<IdentityUser<int>> userManager,
        IJobPictureAppService jobPictureAppService)
    {
        _jobService = jobService;
        _costumerService = costumerService;
        _costumerAddressService = costumerAddressService;
        _cityService = cityService;
        _workerService = workerService;
        _userManager = userManager;
        _jobPictureAppService = jobPictureAppService;
    }


    public async Task<int> AddAsync(JobDto jobDto)
    {
        await _costumerService.EnsureExistsByIdAsync(jobDto.CostumerId);
        await _costumerAddressService.EnsureExistsByIdAsync(jobDto.CostumerAddressId);
        await _cityService.EnsureExistsByIdAsync(jobDto.JobCityId);

        return await _jobService.AddAsync(jobDto);
    }

    public async Task UpdateAsync(JobDto jobDto)
    {
        await _jobService.UpdateAsync(jobDto);
    }

    public async Task DeleteAsync(int jobId, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var job = await _jobService.GetByIdAsync(jobId);
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            await _jobService.DeleteAsync(jobId);
        }
        else
        {
            if (job.JobStatus is JobStatusEnum.RequestedByCostumer or JobStatusEnum.WorkerChosenByCostumer)
            {
                var pictures = await _jobPictureAppService.GetByJobIdAsync(jobId);
                foreach (var picture in pictures) await _jobPictureAppService.DeleteAsync(picture.Id);
                await _jobService.DeleteAsync(jobId);
            }
            else
            {
                throw new Exception("این کار در مرحله قابل حذف نیست");
            }
        }
    }

    public async Task ChangePaymentMethod(int jobId)
    {
        var job = await _jobService.GetByIdAsync(jobId);


        if (job.IsClosed)
        {
            throw new Exception("این کار بسته شده است");
        }

        job.IsOnlinePayment = !job.IsOnlinePayment;

        await _jobService.UpdateAsync(job);
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

    public async Task<List<JobDto>> GetByUserNameAsync(string userName)
    {
        return await _jobService.GetByUserNameAsync(userName);
    }

    public async Task<List<JobDto>> GetAvailableJobsForWorkerAsync(int workerId)
    {
        await _workerService.EnsureExistsByIdAsync(workerId);

        return await _jobService.GetAvailableJobsForWorkerAsync(workerId);
    }
}