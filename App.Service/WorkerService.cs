using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class WorkerService : IWorkerService
{
    private readonly AppDbContext _dbContext;

    public WorkerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int workerId)
    {
        var result = await _dbContext.Workers.AnyAsync(x => x.Id == workerId);

        if (!result) throw new Exception($"کارمند با شناسه {workerId} وجود ندارد !");
    }

    public async Task EnsureExistsByNationalIdAsync(string nationalId)
    {
        var result = await _dbContext.Workers.AnyAsync(x => x.NationalId == nationalId);

        if (!result) throw new Exception($"کارمند با شماره ملی {nationalId} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(string nationalId)
    {
        var result = await _dbContext.Workers.AnyAsync(x => x.NationalId == nationalId);

        if (result) throw new Exception($"کارمند با شماره ملی {nationalId} وجود دارد !");
    }

    public async Task<int> AddAsync(WorkerDto workerDto)
    {
        await EnsureDoesNotExistAsync(workerDto.NationalId);

        var record = new Worker
        {
            Id = workerDto.Id,
            FirstName = workerDto.FirstName,
            LastName = workerDto.LastName,
            NationalId = workerDto.NationalId,
            HomeAddress = workerDto.HomeAddress,
            PictureFilePath = workerDto.PictureFilePath,
            UserCityId = workerDto.CityId,
            Description = workerDto.Description
        };

        var result = await _dbContext.Workers.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(WorkerDto workerDto)
    {
        await EnsureExistsByIdAsync(workerDto.Id);

        if (await _dbContext.Workers.AnyAsync(x =>
                x.NationalId == workerDto.NationalId &&
                x.Id != workerDto.Id))
            throw new Exception($"کارمند با شماره ملی {workerDto.NationalId} وجود دارد !");

        var record = await _dbContext.Workers.FirstAsync(x => x.Id == workerDto.Id);

        record.FirstName = workerDto.FirstName;
        record.LastName = workerDto.LastName;
        record.NationalId = workerDto.NationalId;
        record.HomeAddress = workerDto.HomeAddress;
        record.PictureFilePath = workerDto.PictureFilePath;
        record.UserCityId = workerDto.CityId;
        record.IsConfirmed = workerDto.IsConfirmed;
        record.ConfirmDateTime = workerDto.ConfirmDateTime;
        record.TotalWageEarned = workerDto.TotalWageEarned;
        record.MoneyOwedToCompany = workerDto.MoneyOwedToCompany;
        record.TotalCompanyProfitEarnedFromWorker = workerDto.TotalCompanyProfitEarnedFromWorker;
        record.RatingByCostumers = workerDto.RatingByCostumers;
        record.RatingCount = workerDto.RatingCount;
        record.Description = workerDto.Description;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int workerId)
    {
        await EnsureExistsByIdAsync(workerId);

        var record = await _dbContext.Workers.FirstAsync(x => x.Id == workerId);

        try
        {
            _dbContext.Workers.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e,
                message: $"کارمند {record.FirstName + ' ' + record.LastName} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        var records = await _dbContext.Workers.Select(x => new WorkerDto
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
                Description = x.Description,
                TotalWageEarned = x.TotalWageEarned,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }

    public async Task<WorkerDto> GetByIdAsync(int workerId)
    {
        await EnsureExistsByIdAsync(workerId);

        var record = await _dbContext.Workers
            .Include(x => x.IdentityUser)
            .Include(x => x.UserCity).AsNoTracking()
            .FirstAsync(x => x.Id == workerId);

        var result = new WorkerDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Email = record.IdentityUser!.UserName,
            FirstName = record.FirstName,
            LastName = record.LastName,
            NationalId = record.NationalId,
            HomeAddress = record.HomeAddress,
            PhoneNumber = record.IdentityUser!.PhoneNumber,
            PictureFilePath = record.PictureFilePath,
            IsConfirmed = record.IsConfirmed,
            ConfirmDateTime = record.ConfirmDateTime,
            CityId = record.UserCityId,
            CityName = record.UserCity!.Name,
            Description = record.Description,
            TotalWageEarned = record.TotalWageEarned,
            MoneyOwedToCompany = record.MoneyOwedToCompany,
            TotalCompanyProfitEarnedFromWorker = record.TotalCompanyProfitEarnedFromWorker,
            RatingByCostumers = record.RatingByCostumers,
            RatingCount = record.RatingCount
        };

        return result;
    }

    public async Task<WorkerDto> GetByNationalIdAsync(string nationalId)
    {
        await EnsureExistsByNationalIdAsync(nationalId);

        var record = await _dbContext.Workers
            .Include(x => x.IdentityUser)
            .Include(x => x.UserCity).AsNoTracking()
            .FirstAsync(x => x.NationalId == nationalId);

        var result = new WorkerDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Email = record.IdentityUser!.UserName,
            FirstName = record.FirstName,
            LastName = record.LastName,
            NationalId = record.NationalId,
            HomeAddress = record.HomeAddress,
            PhoneNumber = record.IdentityUser!.PhoneNumber,
            PictureFilePath = record.PictureFilePath,
            IsConfirmed = record.IsConfirmed,
            ConfirmDateTime = record.ConfirmDateTime,
            CityId = record.UserCityId,
            CityName = record.UserCity!.Name,
            Description = record.Description,
            TotalWageEarned = record.TotalWageEarned,
            MoneyOwedToCompany = record.MoneyOwedToCompany,
            TotalCompanyProfitEarnedFromWorker = record.TotalCompanyProfitEarnedFromWorker,
            RatingByCostumers = record.RatingByCostumers,
            RatingCount = record.RatingCount
        };

        return result;
    }

    public async Task<List<WorkerDto>> GetByJobCategoryIdAsync(int jobCategoryId)
    {
        var records = await _dbContext.Workers
            .Where(x => x.JobCategories.Any(y => y.Id == jobCategoryId))
            .Select(x => new WorkerDto
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
                Description = x.Description,
                TotalWageEarned = x.TotalWageEarned,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<WorkerDto>> GetByCityIdAsync(int cityId)
    {
        var records = await _dbContext.Workers
            .Where(x => x.UserCityId == cityId)
            .Select(x => new WorkerDto
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
                Description = x.Description,
                TotalWageEarned = x.TotalWageEarned,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<WorkerDto>> SearchAsync(string? name = null, string? nationalId = null)
    {
        var records = await _dbContext.Workers
            .Where(x => name == null ||
                        (x.FirstName + ' ' + x.LastName).Contains(name))
            .Where(x => nationalId == null ||
                        x.NationalId.Contains(nationalId))
            .Select(x => new WorkerDto
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
                Description = x.Description,
                TotalWageEarned = x.TotalWageEarned,
                MoneyOwedToCompany = x.MoneyOwedToCompany,
                TotalCompanyProfitEarnedFromWorker = x.TotalCompanyProfitEarnedFromWorker,
                RatingByCostumers = x.RatingByCostumers,
                RatingCount = x.RatingCount
            })
            .ToListAsync();

        return records;
    }

    public async Task<bool> IsInJobCategory(int workerId, int jobCategoryId)
    {
        var result = await _dbContext.JobCategories.Where(x => x.Id == jobCategoryId)
            .AnyAsync(x => x.Workers.Any(y => y.Id == workerId));

        return result;
    }

    public async Task AddToJobCategory(int workerId, int jobCategoryId)
    {
        if (await IsInJobCategory(workerId,jobCategoryId))
        {
            throw new Exception("کارمند در این دسته‌بندی قرار دارد");
        }

        await _dbContext.JobCategoryWorkers.AddAsync(new JobCategoryWorker
        {
            JobCategoryId = jobCategoryId,
            WorkerId = workerId,
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFromJobCategory(int workerId, int jobCategoryId)
    {
        if (!await IsInJobCategory(workerId, jobCategoryId))
        {
            throw new Exception("کارمند در این دسته‌بندی قرار ندارد");
        }

        _dbContext.JobCategoryWorkers.Remove(
            await _dbContext.JobCategoryWorkers
                .FirstAsync(x =>
                x.WorkerId == workerId && 
                x.JobCategoryId == jobCategoryId));

        await _dbContext.SaveChangesAsync();
    }
}