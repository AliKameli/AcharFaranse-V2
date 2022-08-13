using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._0_Enums;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.Job._3_Contracts._3_Repositories;
using App.Domain.Core.User.Worker._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.Job;

public class JobQueryRepository : IJobQueryRepository
{
    private readonly AppDbContext _context;

    public JobQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobDto>> GetAllAsync()
    {
        return await _context.Jobs.Select(x => new JobDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            IsDeleted = x.IsDeleted,
            DeleteDateTime = x.DeleteDateTime,
            Description = x.Description,
            CostumerEndNote = x.CostumerEndNote,
            WorkerEndNote = x.WorkerEndNote,
            OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
            IsClosed = x.IsClosed,
            IsOnlinePayment = x.IsOnlinePayment,
            JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
            JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
            JobClosedDateTime = x.JobClosedDateTime,
            JobStatus = x.JobStatus,
            CostumerRatingForWorker = x.CostumerRatingForWorker,
            WorkerRatingForCostumer = x.WorkerRatingForCostumer,
            CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
            WageCost = x.WageCost,
            MaterialCost = x.MaterialCost,
            FinalCost = x.FinalCost,
            CostumerId = x.CostumerId,
            CostumerAddressId = x.CostumerAddressId,
            WorkerId = x.WorkerId
        }).ToListAsync();
    }

    public async Task<List<JobDto>> GetAllByWorkerIdAsync(int workerId)
    {
        return await _context.Jobs
            .Where(x => x.WorkerId == workerId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Description = x.Description,
                CostumerEndNote = x.CostumerEndNote,
                WorkerEndNote = x.WorkerEndNote,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                FinalCost = x.FinalCost,
                CostumerId = x.CostumerId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId
            }).ToListAsync();
    }

    public async Task<List<JobDto>> GetAllByCostumerIdAsync(int costumerId)
    {
        return await _context.Jobs
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Description = x.Description,
                CostumerEndNote = x.CostumerEndNote,
                WorkerEndNote = x.WorkerEndNote,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                FinalCost = x.FinalCost,
                CostumerId = x.CostumerId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId
            }).ToListAsync();
    }

    public async Task<JobDto?> GetByIdAsync(int jobId)
    {
        var record = await _context.Jobs
            .Where(x => x.Id == jobId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Description = x.Description,
                CostumerEndNote = x.CostumerEndNote,
                WorkerEndNote = x.WorkerEndNote,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                FinalCost = x.FinalCost,
                CostumerId = x.CostumerId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId
            })
            .FirstOrDefaultAsync();

        return record;
    }
}