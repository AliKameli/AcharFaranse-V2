using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Enums;
using App.Domain.Options;

namespace App.AppServices;

public class JobPictureAppService : IJobPictureAppService
{
    private readonly ICostumerRepo _costumerService;
    private readonly IJobPictureRepo _jobPictureService;
    private readonly IJobRepo _jobService;
    private readonly FilePath _hostPath;
    private readonly IWorkerRepo _workerService;

    public JobPictureAppService(IJobPictureRepo jobPictureService,
        IJobRepo jobService,
        IWorkerRepo workerService,
        ICostumerRepo costumerService,
        FilePath hostPath)
    {
        _jobPictureService = jobPictureService;
        _jobService = jobService;
        _workerService = workerService;
        _costumerService = costumerService;
        _hostPath = hostPath;
    }


    public async Task<int> AddAsync(JobPictureDto jobPictureDto)
    {
        await (jobPictureDto.UserType == UserTypeEnum.Customer
            ? _costumerService.EnsureExistsByIdAsync((int) jobPictureDto.CostumerId!)
            : _workerService.EnsureExistsByIdAsync((int) jobPictureDto.WorkerId!));

        await _jobService.EnsureExistsByIdAsync(jobPictureDto.JobId);


        if (jobPictureDto.PictureFile != null)
        {
            var uploadsFolder = Path.Combine(_hostPath.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid() + "_" + jobPictureDto.PictureFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await jobPictureDto.PictureFile.CopyToAsync(fileStream);
            }

            jobPictureDto.FileSavePath = @"/Images/" + uniqueFileName;

            return await _jobPictureService.AddAsync(jobPictureDto);
        }

        throw new Exception("نشد");
    }

    public async Task UpdateAsync(JobPictureDto jobPictureDto)
    {
        await _jobPictureService.UpdateAsync(jobPictureDto);
    }

    public async Task DeleteAsync(int jobPictureId)
    {
        var record = await _jobPictureService.GetByIdAsync(jobPictureId);
        var savePath = Path.Join(_hostPath.WebRootPath, record.FileSavePath);
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