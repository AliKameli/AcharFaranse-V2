using App.Domain.Core.User.Costumer._1_Entitys;
using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class CostumerCommandRepository : ICostumerCommandRepository
{
    private readonly AppDbContext _context;

    public CostumerCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CostumerDto costumerDto)
    {
        Costumer record = new Costumer
        {
            FirstName = costumerDto.FirstName,
            LastName = costumerDto.LastName,
            NationalSecurityId = costumerDto.NationalSecurityId,
            HomeAddress = costumerDto.HomeAddress,
            IsConfirmed = costumerDto.IsConfirmed,
            ConfirmDateTime = costumerDto.ConfirmDateTime,
            UserCityId = costumerDto.UserCityId,
        };
        await _context.Costumers.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CostumerDto costumerDto)
    {
        var record = await _context.Costumers.SingleAsync(x => x.Id == costumerDto.Id);

        record.LastUpdateDateTime = DateTimeOffset.Now;
        record.FirstName = costumerDto.FirstName;
        record.LastName = costumerDto.LastName;
        record.HomeAddress = costumerDto.HomeAddress;
        record.IsConfirmed = costumerDto.IsConfirmed;
        record.ConfirmDateTime = costumerDto.ConfirmDateTime;
        record.UserCityId = costumerDto.UserCityId;
        record.JobsRequestedCount = costumerDto.JobsRequestedCount;
        record.JobsAcceptedByWorkersCount = costumerDto.JobsAcceptedByWorkersCount;
        record.JobsFailedByWorkersCount = costumerDto.JobsFailedByWorkersCount;
        record.JobsCanceledByCostumerCount = costumerDto.JobsCanceledByCostumerCount;
        record.JobsBeingDoneByWorkersCount = costumerDto.JobsBeingDoneByWorkersCount;
        record.JobsDoneSuccessfullyByWorkersCount = costumerDto.JobsDoneSuccessfullyByWorkersCount;
        record.TotalWagePaid = costumerDto.TotalWagePaid;
        record.TotalMaterialCostPaid = costumerDto.TotalMaterialCostPaid;
        record.TotalMoneyPaid = costumerDto.TotalMoneyPaid;
        record.TotalCompanyProfitEarnedFromCostumer = costumerDto.TotalCompanyProfitEarnedFromCostumer;
        record.RatingByWorkers = costumerDto.RatingByWorkers;
        record.RatingCount = costumerDto.RatingCount;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int costumerId)
    {
        var record = await _context.Costumers.SingleAsync(x => x.Id == costumerId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTime.Now;

        await _context.SaveChangesAsync();
    }
}