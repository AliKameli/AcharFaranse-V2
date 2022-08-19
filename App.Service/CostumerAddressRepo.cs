using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Repo;

public class CostumerAddressRepo : ICostumerAddressRepo
{
    private readonly AppDbContext _dbContext;

    public CostumerAddressRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int costumerAddressId)
    {
        var result = await _dbContext.CostumerAddresses.AnyAsync(x => x.Id == costumerAddressId);

        if (!result) throw new Exception($"آدرس با شناسه {costumerAddressId} وجود ندارد !");
    }

    public async Task<bool> InUseStatus(int costumerAddressId)
    {
        await EnsureExistsByIdAsync(costumerAddressId);
        var result = await _dbContext.Jobs.AnyAsync(x => x.CostumerAddressId == costumerAddressId);

        return result;
    }

    public async Task<int> AddAsync(CostumerAddressDto costumerAddressDto)
    {
        var record = new CostumerAddress
        {
            Name = costumerAddressDto.Name,
            FullAddress = costumerAddressDto.FullAddress,
            GpsCoordinates = costumerAddressDto.GpsCoordinates,
            ReceivingPersonFullName = costumerAddressDto.ReceivingPersonFullName,
            ReceivingPersonPhoneNumber = costumerAddressDto.ReceivingPersonPhoneNumber,
            AddressCityId = costumerAddressDto.CityId,
            CostumerId = costumerAddressDto.CostumerId
        };

        var result = await _dbContext.CostumerAddresses.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(CostumerAddressDto costumerAddressDto)
    {
        await EnsureExistsByIdAsync(costumerAddressDto.Id);

        var record = await _dbContext.CostumerAddresses.FirstAsync(x => x.Id == costumerAddressDto.Id);

        record.Name = costumerAddressDto.Name;
        record.FullAddress = costumerAddressDto.FullAddress;
        record.GpsCoordinates = costumerAddressDto.GpsCoordinates;
        record.ReceivingPersonFullName = costumerAddressDto.ReceivingPersonFullName;
        record.ReceivingPersonPhoneNumber = costumerAddressDto.ReceivingPersonPhoneNumber;
        record.AddressCityId = costumerAddressDto.CityId;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int costumerAddressId)
    {
        await EnsureExistsByIdAsync(costumerAddressId);

        var record = await _dbContext.CostumerAddresses.FirstAsync(x => x.Id == costumerAddressId);

        try
        {
            _dbContext.CostumerAddresses.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e, message: $"آدرس {record.Name} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task SoftDeleteAsync(int costumerAddressId)
    {
        await EnsureExistsByIdAsync(costumerAddressId);

        var record = await _dbContext.CostumerAddresses.FirstAsync(x => x.Id == costumerAddressId);

        record.IsDeleted = true;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CostumerAddressDto>> GetAllAsync()
    {
        var records = await _dbContext.CostumerAddresses.Select(x => new CostumerAddressDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                FullAddress = x.FullAddress,
                GpsCoordinates = x.GpsCoordinates,
                ReceivingPersonFullName = x.ReceivingPersonFullName,
                ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
                CityId = x.AddressCityId,
                CityName = x.AddressCity!.Name,
                CostumerId = x.CostumerId,
                IsDeleted = x.IsDeleted
            })
            .ToListAsync();

        return records;
    }

    public async Task<CostumerAddressDto> GetByIdAsync(int costumerAddressId)
    {
        await EnsureExistsByIdAsync(costumerAddressId);

        var record = await _dbContext.CostumerAddresses
            .Select(x => new CostumerAddressDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                FullAddress = x.FullAddress,
                GpsCoordinates = x.GpsCoordinates,
                ReceivingPersonFullName = x.ReceivingPersonFullName,
                ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
                CityId = x.AddressCityId,
                CityName = x.AddressCity!.Name,
                CostumerId = x.CostumerId,
                IsDeleted = x.IsDeleted
            })
            .FirstAsync(x => x.Id == costumerAddressId);

        return record;
    }

    public async Task<List<CostumerAddressDto>> GetByCostumerIdAsync(int costumerId)
    {
        var records = await _dbContext.CostumerAddresses
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new CostumerAddressDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name,
                FullAddress = x.FullAddress,
                GpsCoordinates = x.GpsCoordinates,
                ReceivingPersonFullName = x.ReceivingPersonFullName,
                ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
                CityId = x.AddressCityId,
                CityName = x.AddressCity!.Name,
                CostumerId = x.CostumerId,
                IsDeleted = x.IsDeleted
            })
            .ToListAsync();

        return records;
    }
}