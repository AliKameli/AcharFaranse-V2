using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class CostumerAppService : ICostumerAppService
{
    private readonly ICityRepo _cityService;
    private readonly ICostumerRepo _costumerService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CostumerAppService(ICostumerRepo costumerService,
        UserManager<IdentityUser<int>> userManager,
        ICityRepo cityService,
        IWebHostEnvironment webHostEnvironment)
    {
        _costumerService = costumerService;
        _userManager = userManager;
        _cityService = cityService;
        _webHostEnvironment = webHostEnvironment;
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
        await _userManager.AddToRoleAsync(user, "Costumer");

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

    public async Task<List<CostumerDto>> GetByCityIdAsync(int cityId)
    {
        return await _costumerService.GetByCityIdAsync(cityId);
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

    public async Task EditPictureAsync(CostumerDto costumerDto)
    {
        var record = await _costumerService.GetByIdAsync(costumerDto.Id);

        if (costumerDto.PictureFile != null)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            var uniqueFileName = Guid.NewGuid() + "_" + costumerDto.PictureFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await costumerDto.PictureFile.CopyToAsync(fileStream);
            }

            if (record.PictureFilePath is not null)
            {
                var oldFilePath = Path.Join(_webHostEnvironment.WebRootPath, record.PictureFilePath);
                File.Delete(oldFilePath);
            }

            record.PictureFilePath = @"/Images/" + uniqueFileName;

            await _costumerService.UpdateAsync(record);

            return;
        }

        if (record.PictureFilePath is not null)
        {
            var oldFilePath = Path.Join(_webHostEnvironment.WebRootPath, record.PictureFilePath);
            File.Delete(oldFilePath);
            record.PictureFilePath = null;

            await _costumerService.UpdateAsync(record);
        }
    }
}