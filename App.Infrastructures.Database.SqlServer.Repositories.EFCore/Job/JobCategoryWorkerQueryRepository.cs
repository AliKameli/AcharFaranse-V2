using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using App.Domain.Core.User.Worker._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobCategoryWorkerQueryRepository : IJobCategoryWorkerQueryRepository
{
    private readonly AppDbContext _context;

    public JobCategoryWorkerQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobCategoryWorkerDto>> GetAllAsync()
    {
        return await _context.JobCategoryWorkers.Select(x => new JobCategoryWorkerDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            IsDeleted = x.IsDeleted,
            DeleteDateTime = x.DeleteDateTime,
            JobCategoryId = x.JobCategoryId,
            WorkerId = x.WorkerId
        }).ToListAsync();
    }

    public async Task<List<JobCategoryWorkerDto>> GetAllByWorkerIdAsync(int workerId)
    {
        return await _context.JobCategoryWorkers
            .Where(x => x.WorkerId == workerId)
            .Select(x => new JobCategoryWorkerDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                JobCategoryId = x.JobCategoryId,
                WorkerId = x.WorkerId
            }).ToListAsync();
    }

    public async Task<List<JobCategoryWorkerDto>> GetAllByJobCategoryIdAsync(int jobCategoryId)
    {
        return await _context.JobCategoryWorkers
            .Where(x => x.JobCategoryId == jobCategoryId)
            .Select(x => new JobCategoryWorkerDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                JobCategoryId = x.JobCategoryId,
                WorkerId = x.WorkerId
            }).ToListAsync();
    }

    public async Task<JobCategoryWorkerDto?> GetByIdAsync(int jobCategoryWorkerId)
    {
        var record = await _context.JobCategoryWorkers
            .Where(x => x.Id == jobCategoryWorkerId)
            .Select(x => new JobCategoryWorkerDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                JobCategoryId = x.JobCategoryId,
                WorkerId = x.WorkerId
            })
            .FirstOrDefaultAsync();

        return record;
    }
}