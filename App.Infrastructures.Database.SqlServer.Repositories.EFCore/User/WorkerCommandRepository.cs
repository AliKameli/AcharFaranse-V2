using App.Domain.Core.User.Worker._1_Entitys;
using App.Domain.Core.User.Worker._2_Dtos;
using App.Domain.Core.User.Worker._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class WorkerCommandRepository : IWorkerCommandRepository
{
    private readonly AppDbContext _context;

    public WorkerCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(WorkerDto workerDto)
    {
        Worker record = new Worker
        {
            FirstName = workerDto.FirstName,
            LastName = workerDto.LastName,
            NationalSecurityId = workerDto.NationalSecurityId,
            HomeAddress = workerDto.HomeAddress,
            IsConfirmed = workerDto.IsConfirmed,
            ConfirmDateTime = workerDto.ConfirmDateTime,
            UserCityId = workerDto.UserCityId,
        };
        await _context.Workers.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkerDto workerDto)
    {
        var record = await _context.Workers.SingleAsync(x => x.Id == workerDto.Id);

        record.LastUpdateDateTime = DateTimeOffset.Now;
        record.FirstName = workerDto.FirstName;
        record.LastName = workerDto.LastName;
        record.HomeAddress = workerDto.HomeAddress;
        record.IsConfirmed = workerDto.IsConfirmed;
        record.ConfirmDateTime = workerDto.ConfirmDateTime;
        record.UserCityId = workerDto.UserCityId;
        record.Description = workerDto.Description;
        record.JobsProposedCount = workerDto.JobsProposedCount;
        record.JobsAcceptedCount = workerDto.JobsAcceptedCount;
        record.JobsFailedByWorkerCount = workerDto.JobsFailedByWorkerCount;
        record.JobsCanceledByCostumersCount = workerDto.JobsCanceledByCostumersCount;
        record.JobsDoingCount = workerDto.JobsDoingCount;
        record.JobsDoneSuccessfullyCount = workerDto.JobsDoneSuccessfullyCount;
        record.TotalWageEarned = workerDto.TotalWageEarned;
        record.TotalMaterialCostEarned = workerDto.TotalMaterialCostEarned;
        record.TotalMoneyEarned = workerDto.TotalMoneyEarned;
        record.TotalCompanyProfitEarnedFromWorker = workerDto.TotalCompanyProfitEarnedFromWorker;
        record.MoneyOwedToCompany = workerDto.MoneyOwedToCompany;
        record.CompanySharePercentage = workerDto.CompanySharePercentage;
        record.RatingByCostumers = workerDto.RatingByCostumers;
        record.RatingCount = workerDto.RatingCount;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int workerId)
    {
        var record = await _context.Workers.SingleAsync(x => x.Id == workerId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}