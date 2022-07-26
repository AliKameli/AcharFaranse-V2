using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;

namespace App.AppServices;

public class CostumerAddressAppService : ICostumerAddressAppService
{
    private readonly ICityService _cityService;
    private readonly ICostumerAddressService _costumerAddressService;
    private readonly ICostumerService _costumerService;

    public CostumerAddressAppService(ICostumerAddressService costumerAddressService,
        ICostumerService costumerService,
        ICityService cityService)
    {
        _costumerAddressService = costumerAddressService;
        _costumerService = costumerService;
        _cityService = cityService;
    }

    public async Task<int> AddAsync(CostumerAddressDto costumerAddressDto)
    {
        await _cityService.EnsureExistsByIdAsync(costumerAddressDto.CityId);
        await _costumerService.EnsureExistsByIdAsync(costumerAddressDto.CostumerId);
        return await _costumerAddressService.AddAsync(costumerAddressDto);
    }

    public async Task UpdateAsync(CostumerAddressDto costumerAddressDto)
    {
        if (await _costumerAddressService.InUseStatus(costumerAddressDto.Id))
            throw new Exception(
                "این آدرس قبلا استفاده شده است و قابل تغییر نیست . \n برای تغییر این آدرس را حذف کرده و آدرس جدید بسازید");
        await _costumerAddressService.UpdateAsync(costumerAddressDto);
    }

    public async Task DeleteAsync(int costumerAddressId)
    {
        if (await _costumerAddressService.InUseStatus(costumerAddressId))
        {
            await _costumerAddressService.SoftDeleteAsync(costumerAddressId);
        }
        else
        {
            await _costumerAddressService.DeleteAsync(costumerAddressId);
        }
    }

    public async Task<List<CostumerAddressDto>> GetAllAsync()
    {
        return await _costumerAddressService.GetAllAsync();
    }

    public async Task<CostumerAddressDto> GetByIdAsync(int costumerAddressId)
    {
        return await _costumerAddressService.GetByIdAsync(costumerAddressId);
    }

    public async Task<List<CostumerAddressDto>> GetByCostumerIdAsync(int costumerId)
    {
        await _costumerService.EnsureExistsByIdAsync(costumerId);
        return await _costumerAddressService.GetByCostumerIdAsync(costumerId);
    }
}