using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.User;

public class CostumerAddressQueryRepository : ICostumerAddressQueryRepository
{
    private readonly AppDbContext _context;

    public CostumerAddressQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CostumerAddressDto>> GetAllAsync()
    {
        return await _context.CostumerAddresses.Select(x => new CostumerAddressDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            IsDeleted = x.IsDeleted,
            DeleteDateTime = x.DeleteDateTime,
            FullAddress = x.FullAddress,
            GpsCoordinates = x.GpsCoordinates,
            IsReceivingByCostumer = x.IsReceivingByCostumer,
            ReceivingPersonFullName = x.ReceivingPersonFullName,
            ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
            AddressCityId = x.AddressCityId,
            CostumerId = x.CostumerId
        }).ToListAsync();
    }

    public async Task<CostumerAddressDto?> GetByIdAsync(int costumerAddressId)
    {
        var record = await _context.CostumerAddresses
            .Where(x => x.Id == costumerAddressId)
            .Select(x => new CostumerAddressDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                FullAddress = x.FullAddress,
                GpsCoordinates = x.GpsCoordinates,
                IsReceivingByCostumer = x.IsReceivingByCostumer,
                ReceivingPersonFullName = x.ReceivingPersonFullName,
                ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
                AddressCityId = x.AddressCityId,
                CostumerId = x.CostumerId
            })
            .FirstOrDefaultAsync();

        return record;
    }

    public async Task<List<CostumerAddressDto>?> GetAllByCostumerIdAsync(int costumerId)
    {
        return await _context.CostumerAddresses
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new CostumerAddressDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                IsDeleted = x.IsDeleted,
                DeleteDateTime = x.DeleteDateTime,
                FullAddress = x.FullAddress,
                GpsCoordinates = x.GpsCoordinates,
                IsReceivingByCostumer = x.IsReceivingByCostumer,
                ReceivingPersonFullName = x.ReceivingPersonFullName,
                ReceivingPersonPhoneNumber = x.ReceivingPersonPhoneNumber,
                AddressCityId = x.AddressCityId,
                CostumerId = x.CostumerId
            }).ToListAsync();
    }
}