using App.Domain.Common;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class JobPictureService : IJobPictureService
{
    private readonly AppDbContext _dbContext;

    public JobPictureService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int jobPictureId)
    {
        var result = await _dbContext.JobPictures.AnyAsync(x => x.Id == jobPictureId);
        if (!result) throw new Exception($"عکس با شناسه {jobPictureId} وجود ندارد !");
    }


    public async Task<int> AddAsync(JobPictureDto jobPictureDto)
    {
        var record = new JobPicture
        {
            FileSavePath = jobPictureDto.FileSavePath,
            Description = jobPictureDto.Description,
            JobId = jobPictureDto.JobId,
            UserType = jobPictureDto.UserType,
            CostumerId = jobPictureDto.CostumerId,
            WorkerId = jobPictureDto.WorkerId
        };

        var result = await _dbContext.JobPictures.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(JobPictureDto jobPictureDto)
    {
        await EnsureExistsByIdAsync(jobPictureDto.Id);

        var record = await _dbContext.JobPictures.FirstAsync(x => x.Id == jobPictureDto.Id);

        record.Description = jobPictureDto.Description;
        record.IsConfirmed = jobPictureDto.IsConfirmed;

        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobPictureId)
    {
        await EnsureExistsByIdAsync(jobPictureId);

        var record = await _dbContext.JobPictures.FirstAsync(x => x.Id == jobPictureId);

        try
        {
            _dbContext.JobPictures.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e, message: $"عکس {record.Id} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<JobPictureDto>> GetAllAsync()
    {
        var records = await _dbContext.JobPictures.Select(x => new JobPictureDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                FileSavePath = x.FileSavePath,
                Description = x.Description,
                JobId = x.JobId,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker :x.Costumer!).ToString() ,
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<JobPictureDto> GetByIdAsync(int jobPictureId)
    {
        await EnsureExistsByIdAsync(jobPictureId);

        var record = await _dbContext.JobPictures
            .Select(x=> new JobPictureDto()
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                FileSavePath = x.FileSavePath,
                Description = x.Description,
                JobId = x.JobId,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser)x.Worker : x.Costumer!).ToString(),
                IsConfirmed = x.IsConfirmed
            })
            .FirstAsync(x => x.Id == jobPictureId);

        return record;
    }

    public async Task<List<JobPictureDto>> GetByJobIdAsync(int jobId)
    {
        var records = await _dbContext.JobPictures
            .Where(x => x.JobId == jobId)
            .Select(x => new JobPictureDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                FileSavePath = x.FileSavePath,
                Description = x.Description,
                JobId = x.JobId,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser)x.Worker : x.Costumer!).ToString(),
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobPictureDto>> GetByCostumerIdAsync(int costumerId)
    {
        var records = await _dbContext.JobPictures
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new JobPictureDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                FileSavePath = x.FileSavePath,
                Description = x.Description,
                JobId = x.JobId,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser)x.Worker : x.Costumer!).ToString(),
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobPictureDto>> GetByWorkerIdAsync(int workerId)
    {
        var records = await _dbContext.JobPictures
            .Where(x => x.WorkerId == workerId)
            .Select(x => new JobPictureDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                FileSavePath = x.FileSavePath,
                Description = x.Description,
                JobId = x.JobId,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser)x.Worker : x.Costumer!).ToString(),
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

}