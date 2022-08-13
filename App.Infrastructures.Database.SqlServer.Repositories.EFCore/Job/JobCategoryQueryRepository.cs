using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobCategoryQueryRepository : IJobCategoryQueryRepository
{
    private readonly AppDbContext _context;

    public JobCategoryQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobCategoryDto>> GetAllAsync()
    {
        return await _context.JobCategories.Select(x => new JobCategoryDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            IsDeleted = x.IsDeleted,
            DeleteDateTime = x.DeleteDateTime,
            Name = x.Name,
            Description = x.Description,
            PictureFilePath = x.PictureFilePath,
            EstimatedWageCost = x.EstimatedWageCost,
            ParentJobCategoryId = x.ParentJobCategoryId
        }).ToListAsync();
    }

    public async Task<JobCategoryDto?> GetByIdAsync(int jobCategoryId)
    {
        var record = await _context.JobCategories
            .Where(x => x.Id == jobCategoryId)
            .Select(x => new JobCategoryDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Name = x.Name,
                Description = x.Description,
                PictureFilePath = x.PictureFilePath,
                EstimatedWageCost = x.EstimatedWageCost,
                ParentJobCategoryId = x.ParentJobCategoryId
            })
            .FirstOrDefaultAsync();

        return record;
    }

    public async Task<JobCategoryDto?> GetByNameAsync(string jobCategoryName)
    {
        var record = await _context.JobCategories
            .Where(x => x.Name == jobCategoryName)
            .Select(x => new JobCategoryDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Name = x.Name,
                Description = x.Description,
                PictureFilePath = x.PictureFilePath,
                EstimatedWageCost = x.EstimatedWageCost,
                ParentJobCategoryId = x.ParentJobCategoryId
            })
            .FirstOrDefaultAsync();

        return record;
    }
}