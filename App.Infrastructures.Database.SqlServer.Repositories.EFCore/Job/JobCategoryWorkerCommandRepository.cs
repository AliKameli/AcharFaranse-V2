using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobCategoryWorkerCommandRepository : IJobCategoryWorkerCommandRepository
{
    private readonly AppDbContext _context;

    public JobCategoryWorkerCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(JobCategoryWorkerDto jobCategoryWorkerDto)
    {
        JobCategoryWorker record = new JobCategoryWorker
        {
            JobCategoryId = jobCategoryWorkerDto.JobCategoryId,
            WorkerId = jobCategoryWorkerDto.WorkerId,
        };
        await _context.JobCategoryWorkers.AddAsync(record);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int jobCategoryWorkerId)
    {
        var record = await _context.JobCategoryWorkers.SingleAsync(x => x.Id == jobCategoryWorkerId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}