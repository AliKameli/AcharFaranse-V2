using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class WorkerAppService : IWorkerAppService
{
    private readonly ICityService _cityService;
    private readonly IWorkerService _workerService;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public WorkerAppService(IWorkerService workerService,
        UserManager<IdentityUser<int>> userManager,
        ICityService cityService)
    {
        _workerService = workerService;
        _userManager = userManager;
        _cityService = cityService;
    }

    public async Task<int> AddAsync(WorkerDto workerDto)
    {
        await _cityService.EnsureExistsByIdAsync(workerDto.CityId);
        int resultId;
        var user = new IdentityUser<int>
        {
            UserName = workerDto.Email,
            PhoneNumber = workerDto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, workerDto.Password);
        await _userManager.AddToRoleAsync(user, "Worker");

        if (result.Succeeded)
        {
            try
            {
                var record = new WorkerDto
                {
                    Id = user.Id,
                    FirstName = workerDto.FirstName,
                    LastName = workerDto.LastName,
                    NationalId = workerDto.NationalId,
                    HomeAddress = workerDto.HomeAddress,
                    CityId = workerDto.CityId,
                    Description = workerDto.Description
                };
                resultId = await _workerService.AddAsync(record);
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

    public async Task UpdateAsync(WorkerDto workerDto)
    {
        var user = await _userManager.FindByIdAsync(workerDto.Id.ToString());

        var oldEmail = user.UserName;
        var oldPhone = user.PhoneNumber;

        user.UserName = workerDto.Email;
        user.PhoneNumber = workerDto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            try
            {
                await _workerService.UpdateAsync(workerDto);
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

    public async Task DeleteAsync(int workerId)
    {
        var user = await _userManager.FindByIdAsync(workerId.ToString());
        await _workerService.DeleteAsync(workerId);
        await _userManager.DeleteAsync(user);
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        return await _workerService.GetAllAsync();
    }

    public async Task<WorkerDto> GetByIdAsync(int workerId)
    {
        return await _workerService.GetByIdAsync(workerId);
    }

    public async Task<WorkerDto> GetByNationalIdAsync(string nationalId)
    {
        return await _workerService.GetByNationalIdAsync(nationalId);
    }

    public async Task<List<WorkerDto>> GetByJobCategoryIdAsync(int jobCategoryId)
    {
        return await _workerService.GetByJobCategoryIdAsync(jobCategoryId);
    }

    public async Task<List<WorkerDto>> GetByCityIdAsync(int cityId)
    {
        return await _workerService.GetByCityIdAsync(cityId);
    }

    public async Task<List<WorkerDto>> SearchAsync(string? name = null, string? nationalId = null)
    {
        return await _workerService.SearchAsync(name, nationalId);
    }

    public async Task ConfirmAsync(int workerId)
    {
        var record = await _workerService.GetByIdAsync(workerId);

        record.IsConfirmed = true;
        record.ConfirmDateTime = DateTimeOffset.Now;

        await _workerService.UpdateAsync(record);
    }
}