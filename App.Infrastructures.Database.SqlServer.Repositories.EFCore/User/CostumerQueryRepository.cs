using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Costumer._1_Entitys;
using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class CostumerQueryRepository : ICostumerQueryRepository
{
    private readonly AppDbContext _context;

    public CostumerQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CostumerDto>> GetAllAsync()
    {
        return await _context.Costumers.Select(x => new CostumerDto
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
            JobsRequestedCount = x.JobsRequestedCount,
            JobsAcceptedByWorkersCount = x.JobsAcceptedByWorkersCount,
            JobsFailedByWorkersCount = x.JobsFailedByWorkersCount,
            JobsCanceledByCostumerCount = x.JobsCanceledByCostumerCount,
            JobsBeingDoneByWorkersCount = x.JobsBeingDoneByWorkersCount,
            JobsDoneSuccessfullyByWorkersCount = x.JobsDoneSuccessfullyByWorkersCount,
            TotalWagePaid = x.TotalWagePaid,
            TotalMaterialCostPaid = x.TotalMaterialCostPaid,
            TotalMoneyPaid = x.TotalMoneyPaid,
            TotalCompanyProfitEarnedFromCostumer = x.TotalCompanyProfitEarnedFromCostumer,
            RatingByWorkers = x.RatingByWorkers,
            RatingCount = x.RatingCount
        }).ToListAsync();
    }

    public async Task<CostumerDto?> GetByIdAsync(int costumerId)
    {
        var record = await _context.Costumers
            .Where(x => x.Id == costumerId)
            .Select(x => new CostumerDto
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
                JobsRequestedCount = x.JobsRequestedCount,
                JobsAcceptedByWorkersCount = x.JobsAcceptedByWorkersCount,
                JobsFailedByWorkersCount = x.JobsFailedByWorkersCount,
                JobsCanceledByCostumerCount = x.JobsCanceledByCostumerCount,
                JobsBeingDoneByWorkersCount = x.JobsBeingDoneByWorkersCount,
                JobsDoneSuccessfullyByWorkersCount = x.JobsDoneSuccessfullyByWorkersCount,
                TotalWagePaid = x.TotalWagePaid,
                TotalMaterialCostPaid = x.TotalMaterialCostPaid,
                TotalMoneyPaid = x.TotalMoneyPaid,
                TotalCompanyProfitEarnedFromCostumer = x.TotalCompanyProfitEarnedFromCostumer,
                RatingByWorkers = x.RatingByWorkers,
                RatingCount = x.RatingCount
            }).FirstOrDefaultAsync();

        return record;
    }

    public async Task<CostumerDto?> GetByNationalSecurityIdAsync(string costumerNationalSecurityId)
    {
        var record = await _context.Costumers
            .Where(x => x.NationalSecurityId == costumerNationalSecurityId)
            .Select(x => new CostumerDto
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
                JobsRequestedCount = x.JobsRequestedCount,
                JobsAcceptedByWorkersCount = x.JobsAcceptedByWorkersCount,
                JobsFailedByWorkersCount = x.JobsFailedByWorkersCount,
                JobsCanceledByCostumerCount = x.JobsCanceledByCostumerCount,
                JobsBeingDoneByWorkersCount = x.JobsBeingDoneByWorkersCount,
                JobsDoneSuccessfullyByWorkersCount = x.JobsDoneSuccessfullyByWorkersCount,
                TotalWagePaid = x.TotalWagePaid,
                TotalMaterialCostPaid = x.TotalMaterialCostPaid,
                TotalMoneyPaid = x.TotalMoneyPaid,
                TotalCompanyProfitEarnedFromCostumer = x.TotalCompanyProfitEarnedFromCostumer,
                RatingByWorkers = x.RatingByWorkers,
                RatingCount = x.RatingCount
            }).FirstOrDefaultAsync();

        return record;
    }
}