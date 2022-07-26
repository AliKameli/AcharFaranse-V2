using System.Net;
using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Enums;
using Microsoft.AspNetCore.Hosting;

namespace App.AppServices;

public class JobPictureAppService : IJobPictureAppService
{
    private readonly IJobPictureService _jobPictureService;
    private readonly IJobService _jobService;
    private readonly IWorkerService _workerService;
    private readonly ICostumerService _costumerService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public JobPictureAppService(IJobPictureService jobPictureService, IJobService jobService, IWorkerService workerService, ICostumerService costumerService, IWebHostEnvironment webHostEnvironment)
    {
        _jobPictureService = jobPictureService;
        _jobService = jobService;
        _workerService = workerService;
        _costumerService = costumerService;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<int> AddAsync(JobPictureDto jobPictureDto)
    {
        await (jobPictureDto.UserType == UserTypeEnum.Customer
            ? _costumerService.EnsureExistsByIdAsync((int)jobPictureDto.CostumerId!)
            : _workerService.EnsureExistsByIdAsync((int)jobPictureDto.WorkerId!));

        await _jobService.EnsureExistsByIdAsync(jobPictureDto.JobId);

        string uniqueFileName = string.Empty;

        if (jobPictureDto.PictureFile != null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + jobPictureDto.PictureFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await jobPictureDto.PictureFile.CopyToAsync(fileStream);
            }

            jobPictureDto.FileSavePath = @"/Images/" + uniqueFileName;
        return await _jobPictureService.AddAsync(jobPictureDto);
        }
        else
        {
            throw new Exception("نشد");
        }
    }

    public async Task UpdateAsync(JobPictureDto jobPictureDto)
    {
        await _jobPictureService.UpdateAsync(jobPictureDto);
    }

    public async Task DeleteAsync(int jobPictureId)
    {
        var record = await _jobPictureService.GetByIdAsync(jobPictureId);
        var savePath =Path.Join(_webHostEnvironment.WebRootPath, record.FileSavePath);
        File.Delete(savePath);
        await _jobPictureService.DeleteAsync(jobPictureId);
    }

    public async Task<List<JobPictureDto>> GetAllAsync()
    {
        return await _jobPictureService.GetAllAsync();
    }

    public async Task<JobPictureDto> GetByIdAsync(int jobPictureId)
    {
        return await _jobPictureService.GetByIdAsync(jobPictureId);
    }

    public async Task<List<JobPictureDto>> GetByJobIdAsync(int jobId)
    {
        await _jobService.EnsureExistsByIdAsync(jobId);

        return await _jobPictureService.GetByJobIdAsync(jobId);
    }

    public async Task<List<JobPictureDto>> GetByCostumerIdAsync(int costumerId)
    {
        await _costumerService.EnsureExistsByIdAsync(costumerId);

        return await _jobPictureService.GetByCostumerIdAsync(costumerId);
    }

    public async Task<List<JobPictureDto>> GetByWorkerIdAsync(int workerId)
    {
        await _workerService.EnsureExistsByIdAsync(workerId);

        return await _jobPictureService.GetByWorkerIdAsync(workerId);
    }

    public async Task ConfirmAsync(int jobPictureId)
    {
        var record = await _jobPictureService.GetByIdAsync(jobPictureId);
        record.IsConfirmed = true;
        await _jobPictureService.UpdateAsync(record);
    }
}