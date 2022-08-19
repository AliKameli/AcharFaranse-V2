using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class WorkerAppService : IWorkerAppService
{
    private readonly ICityRepo _cityService;
    private readonly IJobCategoryRepo _jobCategoryService;
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IWorkerRepo _workerService;

    public WorkerAppService(IWorkerRepo workerService,
        UserManager<IdentityUser<int>> userManager,
        ICityRepo cityService,
        IWebHostEnvironment webHostEnvironment,
        IJobCategoryRepo jobCategoryService)
    {
        _workerService = workerService;
        _userManager = userManager;
        _cityService = cityService;
        _webHostEnvironment = webHostEnvironment;
        _jobCategoryService = jobCategoryService;
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

    public async Task EditPictureAsync(WorkerDto workerDto)
    {
        var record = await _workerService.GetByIdAsync(workerDto.Id);

        if (workerDto.PictureFile != null)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            var uniqueFileName = Guid.NewGuid() + "_" + workerDto.PictureFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await workerDto.PictureFile.CopyToAsync(fileStream);
            }

            if (record.PictureFilePath is not null)
            {
                var oldFilePath = Path.Join(_webHostEnvironment.WebRootPath, record.PictureFilePath);
                File.Delete(oldFilePath);
            }

            record.PictureFilePath = @"/Images/" + uniqueFileName;

            await _workerService.UpdateAsync(record);

            return;
        }

        if (record.PictureFilePath is not null)
        {
            var oldFilePath = Path.Join(_webHostEnvironment.WebRootPath, record.PictureFilePath);
            File.Delete(oldFilePath);
            record.PictureFilePath = null;

            await _workerService.UpdateAsync(record);
        }
    }

    public async Task AddToJobCategory(int workerId, int jobCategoryId)
    {
        await _workerService.EnsureExistsByIdAsync(workerId);
        await _jobCategoryService.EnsureExistsByIdAsync(jobCategoryId);
        await _workerService.AddToJobCategory(workerId, jobCategoryId);
    }

    public async Task DeleteFromJobCategory(int workerId, int jobCategoryId)
    {
        await _workerService.EnsureExistsByIdAsync(workerId);
        await _jobCategoryService.EnsureExistsByIdAsync(jobCategoryId);
        await _workerService.DeleteFromJobCategory(workerId, jobCategoryId);
    }
}