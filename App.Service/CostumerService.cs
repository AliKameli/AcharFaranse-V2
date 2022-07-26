using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class CostumerService : ICostumerService
{
    private readonly AppDbContext _dbContext;

    public CostumerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int costumerId)
    {
        var result = await _dbContext.Costumers.AnyAsync(x => x.Id == costumerId);
        if (!result) throw new Exception($"مشتری با شناسه {costumerId} وجود ندارد !");
    }

    public async Task EnsureExistsByNationalIdAsync(string nationalId)
    {
        var result = await _dbContext.Costumers.AnyAsync(x => x.NationalId == nationalId);
        if (!result) throw new Exception($"مشتری با شماره ملی {nationalId} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(string nationalId)
    {
        var result = await _dbContext.Costumers.AnyAsync(x => x.NationalId == nationalId);
        if (result) throw new Exception($"مشتری با شماره ملی {nationalId} وجود دارد !");
    }

    public async Task<int> AddAsync(CostumerDto costumerDto)
    {
        await EnsureDoesNotExistAsync(costumerDto.NationalId);

        var record = new Costumer
        {
            Id = costumerDto.Id,
            FirstName = costumerDto.FirstName,
            LastName = costumerDto.LastName,
            NationalId = costumerDto.NationalId,
            HomeAddress = costumerDto.HomeAddress,
            PictureFilePath = costumerDto.PictureFilePath,
            UserCityId = costumerDto.CityId
        };

        var result = await _dbContext.Costumers.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(CostumerDto costumerDto)
    {
        await EnsureExistsByIdAsync(costumerDto.Id);

        if (await _dbContext.Costumers.AnyAsync(x =>
                x.NationalId == costumerDto.NationalId &&
                x.Id != costumerDto.Id))
            throw new Exception($"مشتری با شماره ملی {costumerDto.NationalId} وجود دارد !");

        var record = await _dbContext.Costumers.FirstAsync(x => x.Id == costumerDto.Id);

        record.FirstName = costumerDto.FirstName;
        record.LastName = costumerDto.LastName;
        record.NationalId = costumerDto.NationalId;
        record.HomeAddress = costumerDto.HomeAddress;
        record.PictureFilePath = costumerDto.PictureFilePath;
        record.UserCityId = costumerDto.CityId;
        record.IsConfirmed = costumerDto.IsConfirmed;
        record.ConfirmDateTime = costumerDto.ConfirmDateTime;
        record.TotalMoneyPaid = costumerDto.TotalMoneyPaid;
        record.TotalCompanyProfitEarnedFromCostumer = costumerDto.TotalCompanyProfitEarnedFromCostumer;
        record.RatingByWorkers = costumerDto.RatingByWorkers;
        record.RatingCount = costumerDto.RatingCount;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int costumerId)
    {
        await EnsureExistsByIdAsync(costumerId);

        var record = await _dbContext.Costumers.FirstAsync(x => x.Id == costumerId);

        try
        {
            _dbContext.Costumers.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e,
                message: $"مشتری {record.FirstName + ' ' + record.LastName} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<CostumerDto>> GetAllAsync()
    {
        var records = await _dbContext.Costumers.Select(x => new CostumerDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Email = x.IdentityUser!.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.IdentityUser.PhoneNumber,
                NationalId = x.NationalId,
                HomeAddress = x.HomeAddress,
                PictureFilePath = x.PictureFilePath,
                IsConfirmed = x.IsConfirmed,
                ConfirmDateTime = x.ConfirmDateTime,
                CityId = x.UserCityId,
                CityName = x.UserCity!.Name,
                TotalMoneyPaid = x.TotalMoneyPaid,
                TotalCompanyProfitEarnedFromCostumer = x.TotalCompanyProfitEarnedFromCostumer,
                RatingByWorkers = x.RatingByWorkers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }

    public async Task<CostumerDto> GetByIdAsync(int costumerId)
    {
        await EnsureExistsByIdAsync(costumerId);

        var record = await _dbContext.Costumers
            .Include(x => x.IdentityUser)
            .Include(x => x.UserCity).AsNoTracking()
            .FirstAsync(x => x.Id == costumerId);

        var result = new CostumerDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Email = record.IdentityUser!.UserName,
            FirstName = record.FirstName,
            LastName = record.LastName,
            PhoneNumber = record.IdentityUser.PhoneNumber,
            NationalId = record.NationalId,
            HomeAddress = record.HomeAddress,
            PictureFilePath = record.PictureFilePath,
            IsConfirmed = record.IsConfirmed,
            ConfirmDateTime = record.ConfirmDateTime,
            CityId = record.UserCityId,
            CityName = record.UserCity!.Name,
            TotalMoneyPaid = record.TotalMoneyPaid,
            TotalCompanyProfitEarnedFromCostumer = record.TotalCompanyProfitEarnedFromCostumer,
            RatingByWorkers = record.RatingByWorkers,
            RatingCount = record.RatingCount
        };

        return result;
    }

    public async Task<CostumerDto> GetByNationalIdAsync(string nationalId)
    {
        await EnsureExistsByNationalIdAsync(nationalId);

        var record = await _dbContext.Costumers
            .Include(x => x.IdentityUser)
            .Include(x => x.UserCity).AsNoTracking()
            .FirstAsync(x => x.NationalId == nationalId);

        var result = new CostumerDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Email = record.IdentityUser!.UserName,
            FirstName = record.FirstName,
            LastName = record.LastName,
            PhoneNumber = record.IdentityUser.PhoneNumber,
            NationalId = record.NationalId,
            HomeAddress = record.HomeAddress,
            PictureFilePath = record.PictureFilePath,
            IsConfirmed = record.IsConfirmed,
            ConfirmDateTime = record.ConfirmDateTime,
            CityId = record.UserCityId,
            CityName = record.UserCity!.Name,
            TotalMoneyPaid = record.TotalMoneyPaid,
            TotalCompanyProfitEarnedFromCostumer = record.TotalCompanyProfitEarnedFromCostumer,
            RatingByWorkers = record.RatingByWorkers,
            RatingCount = record.RatingCount
        };

        return result;
    }

    public async Task<List<CostumerDto>> SearchAsync(string? name = null, string? nationalId = null)
    {
        var records = await _dbContext.Costumers
            .Where(x => name == null ||
                        (x.FirstName + ' ' + x.LastName).Contains(name))
            .Where(x => nationalId == null ||
                        x.NationalId.Contains(nationalId))
            .Select(x => new CostumerDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Email = x.IdentityUser!.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.IdentityUser.PhoneNumber,
                NationalId = x.NationalId,
                HomeAddress = x.HomeAddress,
                PictureFilePath = x.PictureFilePath,
                IsConfirmed = x.IsConfirmed,
                ConfirmDateTime = x.ConfirmDateTime,
                CityId = x.UserCityId,
                CityName = x.UserCity!.Name,
                TotalMoneyPaid = x.TotalMoneyPaid,
                TotalCompanyProfitEarnedFromCostumer = x.TotalCompanyProfitEarnedFromCostumer,
                RatingByWorkers = x.RatingByWorkers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }
}