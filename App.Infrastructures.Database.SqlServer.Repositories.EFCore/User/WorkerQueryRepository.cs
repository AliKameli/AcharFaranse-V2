using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Worker._1_Entitys;
using App.Domain.Core.User.Worker._2_Dtos;
using App.Domain.Core.User.Worker._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class WorkerQueryRepository : IWorkerQueryRepository
{
    private readonly AppDbContext _context;

    public WorkerQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        return await _context.Workers.Select(x => new WorkerDto
        {
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            IsDeleted = x.IsDeleted,
            DeleteDateTime = x.DeleteDateTime,
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            NationalSecurityId = x.NationalSecurityId,
            HomeAddress = x.HomeAddress,
            IsConfirmed = x.IsConfirmed,
            ConfirmDateTime = x.ConfirmDateTime,
            UserCityId = x.UserCityId,
            Description = x.Description,
            JobsProposedCount = x.JobsProposedCount,
            JobsAcceptedCount = x.JobsAcceptedCount,
            JobsFailedByWorkerCount = x.JobsFailedByWorkerCount,
            JobsCanceledByCostumersCount = x.JobsCanceledByCostumersCount,
            JobsDoingCount = x.JobsDoingCount,
            JobsDoneSuccessfullyCount = x.JobsDoneSuccessfullyCount,
            TotalWageEarned = x.TotalWageEarned,
            TotalMaterialCostEarned = x.TotalMaterialCostEarned,
            TotalMoneyEarned = x.TotalMoneyEarned,
            TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
            MoneyOwedToCompany = x.MoneyOwedToCompany,
            CompanySharePercentage = x.CompanySharePercentage,
            RatingByCostumers = x.RatingByCostumers,
            RatingCount = x.RatingCount
        }).ToListAsync();
    }

    public async Task<WorkerDto?> GetByIdAsync(int workerId)
    {
        var record = await _context.Workers
            .Where(x => x.Id == workerId)
            .Select(x => new WorkerDto
            {
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalSecurityId = x.NationalSecurityId,
                HomeAddress = x.HomeAddress,
                IsConfirmed = x.IsConfirmed,
                ConfirmDateTime = x.ConfirmDateTime,
                UserCityId = x.UserCityId,
                Description = x.Description,
                JobsProposedCount = x.JobsProposedCount,
                JobsAcceptedCount = x.JobsAcceptedCount,
                JobsFailedByWorkerCount = x.JobsFailedByWorkerCount,
                JobsCanceledByCostumersCount = x.JobsCanceledByCostumersCount,
                JobsDoingCount = x.JobsDoingCount,
                JobsDoneSuccessfullyCount = x.JobsDoneSuccessfullyCount,
                TotalWageEarned = x.TotalWageEarned,
                TotalMaterialCostEarned = x.TotalMaterialCostEarned,
                TotalMoneyEarned = x.TotalMoneyEarned,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                CompanySharePercentage = x.CompanySharePercentage,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            }).FirstOrDefaultAsync();

        return record;
    }

    public async Task<WorkerDto?> GetByNationalSecurityIdAsync(string workerNationalSecurityId)
    {
        var record = await _context.Workers
            .Where(x => x.NationalSecurityId == workerNationalSecurityId)
            .Select(x => new WorkerDto
            {
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalSecurityId = x.NationalSecurityId,
                HomeAddress = x.HomeAddress,
                IsConfirmed = x.IsConfirmed,
                ConfirmDateTime = x.ConfirmDateTime,
                UserCityId = x.UserCityId,
                Description = x.Description,
                JobsProposedCount = x.JobsProposedCount,
                JobsAcceptedCount = x.JobsAcceptedCount,
                JobsFailedByWorkerCount = x.JobsFailedByWorkerCount,
                JobsCanceledByCostumersCount = x.JobsCanceledByCostumersCount,
                JobsDoingCount = x.JobsDoingCount,
                JobsDoneSuccessfullyCount = x.JobsDoneSuccessfullyCount,
                TotalWageEarned = x.TotalWageEarned,
                TotalMaterialCostEarned = x.TotalMaterialCostEarned,
                TotalMoneyEarned = x.TotalMoneyEarned,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                CompanySharePercentage = x.CompanySharePercentage,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            }).FirstOrDefaultAsync();

        return record;
    }
}