using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class CostumerAppService : ICostumerAppService
{
    private readonly ICityService _cityService;
    private readonly ICostumerService _costumerService;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public CostumerAppService(ICostumerService costumerService,
        UserManager<IdentityUser<int>> userManager,
        ICityService cityService)
    {
        _costumerService = costumerService;
        _userManager = userManager;
        _cityService = cityService;
    }

    public async Task<int> AddAsync(CostumerDto costumerDto)
    {
        await _cityService.EnsureExistsByIdAsync(costumerDto.CityId);
        int resultId;
        var user = new IdentityUser<int>
        {
            UserName = costumerDto.Email,
            PhoneNumber = costumerDto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, costumerDto.Password);

        if (result.Succeeded)
        {
            try
            {
                var record = new CostumerDto
                {
                    Id = user.Id,
                    FirstName = costumerDto.FirstName,
                    LastName = costumerDto.LastName,
                    NationalId = costumerDto.NationalId,
                    HomeAddress = costumerDto.HomeAddress,
                    CityId = costumerDto.CityId
                };
                resultId = await _costumerService.AddAsync(record);
            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }
        else
        {
            var errors = string.Empty;

            foreach (var error in result.Errors) errors += "\n" + error.Description;

            throw new Exception(errors);
        }

        return resultId;
    }

    public async Task UpdateAsync(CostumerDto costumerDto)
    {
        var user = await _userManager.FindByIdAsync(costumerDto.Id.ToString());

        var oldEmail = user.UserName;
        var oldPhone = user.PhoneNumber;

        user.UserName = costumerDto.Email;
        user.PhoneNumber = costumerDto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            try
            {
                await _costumerService.UpdateAsync(costumerDto);
            }
            catch (Exception)
            {
                user.UserName = oldEmail;
                user.PhoneNumber = oldPhone;

                await _userManager.UpdateAsync(user);
                throw;
            }
        }
        else
        {
            var errors = string.Empty;

            foreach (var error in result.Errors) errors += "\n" + error.Description;

            throw new Exception(errors);
        }
    }

    public async Task DeleteAsync(int costumerId)
    {
        var user = await _userManager.FindByIdAsync(costumerId.ToString());
        await _costumerService.DeleteAsync(costumerId);
        await _userManager.DeleteAsync(user);
    }

    public async Task<List<CostumerDto>> GetAllAsync()
    {
        return await _costumerService.GetAllAsync();
    }

    public async Task<CostumerDto> GetByIdAsync(int costumerId)
    {
        return await _costumerService.GetByIdAsync(costumerId);
    }

    public async Task<CostumerDto> GetByNationalIdAsync(string nationalId)
    {
        return await _costumerService.GetByNationalIdAsync(nationalId);
    }

    public async Task<List<CostumerDto>> SearchAsync(string? name = null, string? nationalId = null)
    {
        return await _costumerService.SearchAsync(name, nationalId);
    }

    public async Task ConfirmAsync(int costumerId)
    {
        var record = await _costumerService.GetByIdAsync(costumerId);

        record.IsConfirmed = true;
        record.ConfirmDateTime = DateTimeOffset.Now;

        await _costumerService.UpdateAsync(record);
    }
}