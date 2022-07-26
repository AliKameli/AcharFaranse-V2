using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class JobWorkerProposalService : IJobWorkerProposalService
{
    private readonly AppDbContext _dbContext;

    public JobWorkerProposalService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int jobWorkerProposalId)
    {
        var result = await _dbContext.JobWorkerProposals.AnyAsync(x => x.Id == jobWorkerProposalId);
        if (!result) throw new Exception($"پیشنهاد قیمت با شناسه {jobWorkerProposalId} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        var result = await _dbContext.JobWorkerProposals.AnyAsync(x =>
            x.JobId == jobWorkerProposalDto.JobId &&
            x.WorkerId == jobWorkerProposalDto.WorkerId);
        if (result) throw new Exception("کارمند قبلا به این کار پیشنهاد قیمت داده است");
    }

    public async Task<int> AddAsync(JobWorkerProposalDto jobWorkerProposalDto)
    {
        await EnsureDoesNotExistAsync(jobWorkerProposalDto);

        var record = new JobWorkerProposal
        {
            ProposedPrice = jobWorkerProposalDto.ProposedPrice,
            WorkerComment = jobWorkerProposalDto.WorkerComment,
            JobId = jobWorkerProposalDto.JobId,
            WorkerId = jobWorkerProposalDto.WorkerId
        };

        var result = await _dbContext.JobWorkerProposals.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    //public async Task UpdateAsync(JobWorkerProposalDto jobWorkerProposalDto)
    //{
    //    throw new Exception("نمیتوان پیشنهاد را تغییر داد")ک
    //}

    public async Task DeleteAsync(int jobWorkerProposalId)
    {
        await EnsureExistsByIdAsync(jobWorkerProposalId);

        var record = await _dbContext.JobWorkerProposals.FirstAsync(x => x.Id == jobWorkerProposalId);

        _dbContext.JobWorkerProposals.Remove(record);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<JobWorkerProposalDto>> GetAllAsync()
    {
        var records = await _dbContext.JobWorkerProposals.Select(x => new JobWorkerProposalDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                ProposedPrice = x.ProposedPrice,
                WorkerComment = x.WorkerComment,
                JobId = x.JobId,
                WorkerId = x.WorkerId,
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName
            })
            .ToListAsync();

        return records;
    }

    public async Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId)
    {
        await EnsureExistsByIdAsync(jobWorkerProposalId);

        var record = await _dbContext.JobWorkerProposals.AsNoTracking().FirstAsync(x => x.Id == jobWorkerProposalId);

        var result = new JobWorkerProposalDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            ProposedPrice = record.ProposedPrice,
            WorkerComment = record.WorkerComment,
            JobId = record.JobId,
            WorkerId = record.WorkerId,
            WorkerName = record.Worker!.FirstName + ' ' + record.Worker.LastName
        };

        return result;
    }

    public async Task<List<JobWorkerProposalDto>> GetByJobIdAsync(int jobId)
    {
        var records = await _dbContext.JobWorkerProposals
            .Where(x => x.JobId == jobId)
            .Select(x => new JobWorkerProposalDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                ProposedPrice = x.ProposedPrice,
                WorkerComment = x.WorkerComment,
                JobId = x.JobId,
                WorkerId = x.WorkerId,
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName
            })
            .ToListAsync();

        return records;
    }
}