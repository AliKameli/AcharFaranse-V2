using App.Domain.Core.User.Costumer._1_Entitys;
using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class CostumerAddressCommandRepository : ICostumerAddressCommandRepository
{
    private readonly AppDbContext _context;

    public CostumerAddressCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CostumerAddressDto costumerAddressDto)
    {
        CostumerAddress record = new CostumerAddress
        {
            FullAddress = costumerAddressDto.FullAddress,
            GpsCoordinates = costumerAddressDto.GpsCoordinates,
            IsReceivingByCostumer = costumerAddressDto.IsReceivingByCostumer,
            ReceivingPersonFullName = costumerAddressDto.ReceivingPersonFullName,
            ReceivingPersonPhoneNumber = costumerAddressDto.ReceivingPersonPhoneNumber,
            AddressCityId = costumerAddressDto.AddressCityId,
            CostumerId = costumerAddressDto.CostumerId,
        };

        await _context.CostumerAddresses.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CostumerAddressDto costumerAddressDto)
    {
        var record = await _context.CostumerAddresses.SingleAsync(x => x.Id == costumerAddressDto.Id);
        record.FullAddress = costumerAddressDto.FullAddress;
        record.GpsCoordinates = costumerAddressDto.GpsCoordinates;
        record.AddressCityId = costumerAddressDto.AddressCityId;
        record.IsReceivingByCostumer = costumerAddressDto.IsReceivingByCostumer;
        record.ReceivingPersonFullName = costumerAddressDto.ReceivingPersonFullName;
        record.ReceivingPersonPhoneNumber = costumerAddressDto.ReceivingPersonPhoneNumber;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int costumerAddressId)
    {
        var record = await _context.CostumerAddresses.SingleAsync(x => x.Id == costumerAddressId);
        record.IsDeleted = true;
        record.DeleteDateTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}