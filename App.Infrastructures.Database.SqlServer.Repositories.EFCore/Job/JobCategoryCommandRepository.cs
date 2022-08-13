using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobCategoryCommandRepository : IJobCategoryCommandRepository
{
    private readonly AppDbContext _context;

    public JobCategoryCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(JobCategoryDto jobCategoryDto)
    {
        JobCategory record = new JobCategory
        {
            Name = jobCategoryDto.Name,
            Description = jobCategoryDto.Description,
            PictureFilePath = jobCategoryDto.PictureFilePath,
            EstimatedWageCost = jobCategoryDto.EstimatedWageCost,
            ParentJobCategoryId = jobCategoryDto.ParentJobCategoryId,
        };
        await _context.JobCategories.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobCategoryDto jobCategoryDto)
    {
        var record = await _context.JobCategories.SingleAsync(x => x.Id == jobCategoryDto.Id);
        record.Name = jobCategoryDto.Name;
        record.Description = jobCategoryDto.Description;
        record.PictureFilePath = jobCategoryDto.PictureFilePath;
        record.EstimatedWageCost = jobCategoryDto.EstimatedWageCost;
        record.ParentJobCategoryId = jobCategoryDto.ParentJobCategoryId;
        record.LastUpdateDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobCategoryId)
    {
        var record = await _context.JobCategories.SingleAsync(x => x.Id == jobCategoryId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}