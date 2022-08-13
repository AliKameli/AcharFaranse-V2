using App.Domain.Core.Job._0_Enums;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobCommandRepository : IJobCommandRepository
{
    private readonly AppDbContext _context;

    public JobCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(JobDto jobDto)
    {
        Domain.Core.Job._1_Entitys.Job record = new Domain.Core.Job._1_Entitys.Job
        {
            Description = jobDto.Description,
            IsOnlinePayment = jobDto.IsOnlinePayment,
            JobStatus = JobStatusEnum.RequestedByCostumer,
            CostumerEstimatedFinalCost = jobDto.CostumerEstimatedFinalCost,
            CostumerId = jobDto.CostumerId,
            CostumerAddressId = jobDto.CostumerAddressId,
        };
        await _context.Jobs.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobDto jobDto)
    {
        var record = await _context.Jobs.SingleAsync(x => x.Id == jobDto.Id);
        record.LastUpdateDateTime = DateTimeOffset.Now;
        record.Description = jobDto.Description;
        record.IsOnlinePayment = jobDto.IsOnlinePayment;
        record.JobStatus = jobDto.JobStatus;
        record.CostumerAddressId = jobDto.CostumerAddressId;
        record.CostumerEndNote = jobDto.CostumerEndNote;
        record.WorkerEndNote = jobDto.WorkerEndNote;
        record.CostumerRatingForWorker = jobDto.CostumerRatingForWorker;
        record.WorkerRatingForCostumer = jobDto.WorkerRatingForCostumer;
        record.IsClosed = jobDto.IsClosed;
        record.JobAcceptedByWorkerDateTime = jobDto.JobAcceptedByWorkerDateTime;
        record.JobStartedByWorkerDateTime = jobDto.JobStartedByWorkerDateTime;
        record.JobClosedDateTime = jobDto.JobClosedDateTime;
        record.WageCost = jobDto.WageCost;
        record.MaterialCost = jobDto.MaterialCost;
        record.FinalCost = jobDto.FinalCost;
        record.OnlinePaymentReceiptInfo = jobDto.OnlinePaymentReceiptInfo;
        record.WorkerId = jobDto.WorkerId;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobId)
    {
        var record = await _context.Jobs.SingleAsync(x => x.Id == jobId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}