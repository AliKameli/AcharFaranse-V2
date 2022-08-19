using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Repo;

public class JobWorkerProposalRepo : IJobWorkerProposalRepo
{
    private readonly AppDbContext _dbContext;

    public JobWorkerProposalRepo(AppDbContext dbContext)
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

    public async Task AcceptAsync(int jobWorkerProposalId)
    {
        await EnsureExistsByIdAsync(jobWorkerProposalId);
        var record = await _dbContext.JobWorkerProposals
            .FirstAsync(x => x.Id == jobWorkerProposalId);
        var job = await _dbContext.Jobs.FirstAsync(x => x.Id == record.JobId);
        _dbContext.JobWorkerProposals.RemoveRange(
            _dbContext.JobWorkerProposals.Where(x =>
                x.JobId == record.JobId &&
                x.Id != record.Id)
        );
        record.ProposalStatus = ProposalStatusEnum.AcceptedByCostumer;
        job.JobStatus = JobStatusEnum.WorkerChosenByCostumer;
        job.WorkerId = record.WorkerId;

        await _dbContext.SaveChangesAsync();
    }

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
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName,
                ProposalStatus = x.ProposalStatus
            })
            .ToListAsync();

        return records;
    }

    public async Task<JobWorkerProposalDto> GetByIdAsync(int jobWorkerProposalId)
    {
        await EnsureExistsByIdAsync(jobWorkerProposalId);

        var result = await _dbContext.JobWorkerProposals
            .Select(x => new JobWorkerProposalDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                ProposedPrice = x.ProposedPrice,
                WorkerComment = x.WorkerComment,
                JobId = x.JobId,
                WorkerId = x.WorkerId,
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName,
                ProposalStatus = x.ProposalStatus
            })
            .FirstAsync(x => x.Id == jobWorkerProposalId);

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
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName,
                ProposalStatus = x.ProposalStatus
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobWorkerProposalDto>> GetByWorkerIdAsync(int workerId)
    {
        var records = await _dbContext.JobWorkerProposals
            .Where(x => x.WorkerId == workerId)
            .Select(x => new JobWorkerProposalDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                ProposedPrice = x.ProposedPrice,
                WorkerComment = x.WorkerComment,
                JobId = x.JobId,
                WorkerId = x.WorkerId,
                WorkerName = x.Worker!.FirstName + ' ' + x.Worker.LastName,
                ProposalStatus = x.ProposalStatus
            })
            .ToListAsync();

        return records;
    }
}