using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class JobCategoryService : IJobCategoryService
{
    private readonly AppDbContext _dbContext;

    public JobCategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int jobCategoryId)
    {
        var result = await _dbContext.JobCategories.AnyAsync(x => x.Id == jobCategoryId);

        if (!result) throw new Exception($"دسته‌بندی با شناسه {jobCategoryId} وجود ندارد !");
    }

    public async Task EnsureExistsByNameAsync(string jobCategoryName)
    {
        var result = await _dbContext.JobCategories.AnyAsync(x => x.Name == jobCategoryName);

        if (!result) throw new Exception($"دسته‌بندی {jobCategoryName} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(JobCategoryDto jobCategoryDto)
    {
        var result = await _dbContext.JobCategories.AnyAsync(x =>
            x.Name == jobCategoryDto.Name &&
            x.ParentJobCategoryId == jobCategoryDto.ParentJobCategoryId);

        if (result) throw new Exception($"دسته‌بندی {jobCategoryDto.Name} در این گروه وجود دارد !");
    }

    public async Task<int> AddAsync(JobCategoryDto jobCategoryDto)
    {
        await EnsureDoesNotExistAsync(jobCategoryDto);

        var record = new JobCategory
        {
            Name = jobCategoryDto.Name,
            Description = jobCategoryDto.Description,
            PictureFilePath = jobCategoryDto.PictureFilePath,
            EstimatedWageCost = jobCategoryDto.EstimatedWageCost,
            ParentJobCategoryId = jobCategoryDto.ParentJobCategoryId
        };

        var result = await _dbContext.JobCategories.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(JobCategoryDto jobCategoryDto)
    {
        await EnsureExistsByIdAsync(jobCategoryDto.Id);

        if (await _dbContext.JobCategories.AnyAsync(x =>
                x.ParentJobCategoryId == jobCategoryDto.ParentJobCategoryId &&
                x.Name == jobCategoryDto.Name &&
                x.Id != jobCategoryDto.Id))
            throw new Exception($"دسته‌بندی {jobCategoryDto.Name} در این گروه وجود دارد !");

        var record = await _dbContext.JobCategories.FirstAsync(x => x.Id == jobCategoryDto.Id);

        record.Name = jobCategoryDto.Name;
        record.Description = jobCategoryDto.Description;
        record.PictureFilePath = jobCategoryDto.PictureFilePath;
        record.EstimatedWageCost = jobCategoryDto.EstimatedWageCost;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobCategoryId)
    {
        await EnsureExistsByIdAsync(jobCategoryId);

        var record = await _dbContext.JobCategories.FirstAsync(x => x.Id == jobCategoryId);

        try
        {
            _dbContext.JobCategories.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e,
                message: $"دسته بندی {record.Name} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<JobCategoryDto>> GetAllAsync()
    {
        var records = await _dbContext.JobCategories.Select(x => new JobCategoryDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                Description = x.Description,
                PictureFilePath = x.PictureFilePath,
                EstimatedWageCost = x.EstimatedWageCost,
                ParentJobCategoryId = x.ParentJobCategoryId
            })
            .ToListAsync();
        records.ForEach(x =>
            x.GroupPath = GetGroupPath(x.Id, x.ParentJobCategoryId));

        return records;
    }

    public async Task<JobCategoryDto> GetByIdAsync(int jobCategoryId)
    {
        await EnsureExistsByIdAsync(jobCategoryId);

        var record = await _dbContext.JobCategories.AsNoTracking().FirstAsync(x => x.Id == jobCategoryId);

        var result = new JobCategoryDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Name = record.Name,
            Description = record.Description,
            PictureFilePath = record.PictureFilePath,
            EstimatedWageCost = record.EstimatedWageCost,
            ParentJobCategoryId = record.ParentJobCategoryId,
            GroupPath = GetGroupPath(record.Id, record.ParentJobCategoryId)
        };

        return result;
    }

    public async Task<List<JobCategoryDto>> GetByParentIdAsync(int? parentId)
    {
        var records = await _dbContext.JobCategories
            .Where(x => x.ParentJobCategoryId == parentId)
            .Select(x => new JobCategoryDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                Description = x.Description,
                PictureFilePath = x.PictureFilePath,
                EstimatedWageCost = x.EstimatedWageCost,
                ParentJobCategoryId = x.ParentJobCategoryId
            })
            .ToListAsync();
        records.ForEach(x =>
            x.GroupPath = GetGroupPath(x.Id, x.ParentJobCategoryId));

        return records;
    }

    public async Task<List<JobCategoryDto>> GetByWorkerIdAsync(int workerId)
    {
        var records = await _dbContext.JobCategories
            .Where(x => x.Workers.Any(y => y.Id == workerId))
            .Select(x => new JobCategoryDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                Description = x.Description,
                PictureFilePath = x.PictureFilePath,
                EstimatedWageCost = x.EstimatedWageCost,
                ParentJobCategoryId = x.ParentJobCategoryId
            })
            .ToListAsync();

        records.ForEach(x =>
            x.GroupPath = GetGroupPath(x.Id, x.ParentJobCategoryId));

        return records;
    }

    private string GetGroupPath(int jobCategoryId, int? parentId)
    {
        var result = string.Empty;

        if (parentId == null) return "پایه";

        var parent = _dbContext.JobCategories.AsNoTracking().First(x => x.Id == parentId);

        result = new string(GetGroupPath(parent.Id, parent.ParentJobCategoryId) + @" / " + parent.Name);

        return result;
    }
}