using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class JobService : IJobService
{
    private readonly AppDbContext _dbContext;

    public JobService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int jobId)
    {
        var result = await _dbContext.Jobs.AnyAsync(x => x.Id == jobId);

        if (!result) throw new Exception($"کار با شناسه {jobId} وجود ندارد !");
    }


    public async Task<int> AddAsync(JobDto jobDto)
    {
        var record = new Job
        {
            Description = jobDto.Description,
            IsOnlinePayment = jobDto.IsOnlinePayment,
            IsPictureAttached = jobDto.IsPictureAttached,
            JobStartTimeRequestedByUserDateTime = jobDto.JobStartTimeRequestedByUserDateTime,
            CostumerEstimatedFinalCost = jobDto.CostumerEstimatedFinalCost,
            CostumerId = jobDto.CostumerId,
            JobCityId = jobDto.JobCityId,
            JobCategoryId = jobDto.JobCategoryId,
            CostumerAddressId = jobDto.CostumerAddressId,
            WorkerId = jobDto.WorkerId
        };

        var result = await _dbContext.Jobs.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(JobDto jobDto)
    {
        await EnsureExistsByIdAsync(jobDto.Id);

        var record = await _dbContext.Jobs.FirstAsync(x => x.Id == jobDto.Id);

        record.LastUpdateDateTime = DateTimeOffset.Now;
        record.Description = jobDto.Description;
        record.JobStatus = jobDto.JobStatus;
        record.OnlinePaymentReceiptInfo = jobDto.OnlinePaymentReceiptInfo;
        record.IsOnlinePayment = jobDto.IsOnlinePayment;
        record.IsClosed = jobDto.IsClosed;
        record.IsPictureAttached = jobDto.IsPictureAttached;
        record.JobAcceptedByWorkerDateTime = jobDto.JobAcceptedByWorkerDateTime;
        record.JobStartedByWorkerDateTime = jobDto.JobStartedByWorkerDateTime;
        record.JobClosedDateTime = jobDto.JobClosedDateTime;
        record.CostumerRatingForWorker = jobDto.CostumerRatingForWorker;
        record.WorkerRatingForCostumer = jobDto.WorkerRatingForCostumer;
        record.WageCost = jobDto.WageCost;
        record.MaterialCost = jobDto.MaterialCost;
        record.CompanyProfit = jobDto.CompanyProfit;
        record.FinalCost = jobDto.FinalCost;
        record.WorkerId = jobDto.WorkerId;
        record.CostumerAddressId = jobDto.CostumerAddressId;
        record.JobCategoryId = jobDto.JobCategoryId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobId)
    {
        await EnsureExistsByIdAsync(jobId);

        var record = await _dbContext.Jobs.FirstAsync(x => x.Id == jobId);

        _dbContext.Jobs.Remove(record);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<JobDto>> GetAllAsync()
    {
        var records = await _dbContext.Jobs.Select(x => new JobDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            Description = x.Description,
            OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
            IsClosed = x.IsClosed,
            IsOnlinePayment = x.IsOnlinePayment,
            IsPictureAttached = x.IsPictureAttached,
            JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
            JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
            JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
            JobClosedDateTime = x.JobClosedDateTime,
            JobStatus = x.JobStatus,
            CostumerRatingForWorker = x.CostumerRatingForWorker,
            WorkerRatingForCostumer = x.WorkerRatingForCostumer,
            CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
            WageCost = x.WageCost,
            MaterialCost = x.MaterialCost,
            CompanyProfit = x.CompanyProfit,
            FinalCost = x.FinalCost,
            CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
            WorkerName = x.Worker!.FirstName + ' ' + x.Worker!.LastName,
            CostumerAddressName = x.CostumerAddress!.Name,
            JobCategoryName = x.JobCategory!.Name,
            JobCityName = x.JobCity!.Name,
            CostumerId = x.CostumerId,
            JobCityId = x.JobCityId,
            JobCategoryId = x.JobCategoryId,
            CostumerAddressId = x.CostumerAddressId,
            WorkerId = x.WorkerId,
            CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
            WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
        })
            .ToListAsync();

        return records;
    }

    public async Task<JobDto> GetByIdAsync(int jobId)
    {
        await EnsureExistsByIdAsync(jobId);

        var result = await _dbContext.Jobs.Select(x => new JobDto
        {
            Id = x.Id,
            CreationDateTime = x.CreationDateTime,
            LastUpdateDateTime = x.LastUpdateDateTime,
            Description = x.Description,
            OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
            IsClosed = x.IsClosed,
            IsOnlinePayment = x.IsOnlinePayment,
            IsPictureAttached = x.IsPictureAttached,
            JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
            JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
            JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
            JobClosedDateTime = x.JobClosedDateTime,
            JobStatus = x.JobStatus,
            CostumerRatingForWorker = x.CostumerRatingForWorker,
            WorkerRatingForCostumer = x.WorkerRatingForCostumer,
            CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
            WageCost = x.WageCost,
            MaterialCost = x.MaterialCost,
            CompanyProfit = x.CompanyProfit,
            FinalCost = x.FinalCost,
            CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
            WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
            CostumerAddressName = x.CostumerAddress!.Name,
            JobCategoryName = x.JobCategory!.Name,
            JobCityName = x.JobCity!.Name,
            CostumerId = x.CostumerId,
            JobCityId = x.JobCityId,
            JobCategoryId = x.JobCategoryId,
            CostumerAddressId = x.CostumerAddressId,
            WorkerId = x.WorkerId,
            CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
            WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
        })
            .FirstAsync(x => x.Id == jobId);

        return result;
    }

    public async Task<List<JobDto>> GetByJobCategoryIdAsync(int jobCategoryId)
    {
        var records = await _dbContext.Jobs
            .Where(x => x.JobCategoryId == jobCategoryId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Description = x.Description,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                IsPictureAttached = x.IsPictureAttached,
                JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                CompanyProfit = x.CompanyProfit,
                FinalCost = x.FinalCost,
                CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
                WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
                CostumerAddressName = x.CostumerAddress!.Name,
                JobCategoryName = x.JobCategory!.Name,
                JobCityName = x.JobCity!.Name,
                CostumerId = x.CostumerId,
                JobCityId = x.JobCityId,
                JobCategoryId = x.JobCategoryId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId,
                CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
                WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobDto>> GetByCostumerIdAsync(int costumerId)
    {
        var records = await _dbContext.Jobs
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Description = x.Description,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                IsPictureAttached = x.IsPictureAttached,
                JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                CompanyProfit = x.CompanyProfit,
                FinalCost = x.FinalCost,
                CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
                WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
                CostumerAddressName = x.CostumerAddress!.Name,
                JobCategoryName = x.JobCategory!.Name,
                JobCityName = x.JobCity!.Name,
                CostumerId = x.CostumerId,
                JobCityId = x.JobCityId,
                JobCategoryId = x.JobCategoryId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId,
                CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
                WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobDto>> GetByWorkerIdAsync(int workerId)
    {
        var records = await _dbContext.Jobs
            .Where(x => x.WorkerId == workerId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Description = x.Description,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                IsPictureAttached = x.IsPictureAttached,
                JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                CompanyProfit = x.CompanyProfit,
                FinalCost = x.FinalCost,
                CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
                WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
                CostumerAddressName = x.CostumerAddress!.Name,
                JobCategoryName = x.JobCategory!.Name,
                JobCityName = x.JobCity!.Name,
                CostumerId = x.CostumerId,
                JobCityId = x.JobCityId,
                JobCategoryId = x.JobCategoryId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId,
                CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
                WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobDto>> GetByCityIdAsync(int cityId)
    {
        var records = await _dbContext.Jobs
            .Where(x => x.JobCityId == cityId)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Description = x.Description,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                IsPictureAttached = x.IsPictureAttached,
                JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                CompanyProfit = x.CompanyProfit,
                FinalCost = x.FinalCost,
                CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
                WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
                CostumerAddressName = x.CostumerAddress!.Name,
                JobCategoryName = x.JobCategory!.Name,
                JobCityName = x.JobCity!.Name,
                CostumerId = x.CostumerId,
                JobCityId = x.JobCityId,
                JobCategoryId = x.JobCategoryId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId,
                CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
                WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<JobDto>> GetByUserNameAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new Exception("کاربری با این نام کاربری وجود ندارد");
        }
        var records = await _dbContext.Jobs
            .Where(x => x.WorkerId == user.Id || x.CostumerId == user.Id)
            .Select(x => new JobDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Description = x.Description,
                OnlinePaymentReceiptInfo = x.OnlinePaymentReceiptInfo,
                IsClosed = x.IsClosed,
                IsOnlinePayment = x.IsOnlinePayment,
                IsPictureAttached = x.IsPictureAttached,
                JobStartTimeRequestedByUserDateTime = x.JobStartTimeRequestedByUserDateTime,
                JobAcceptedByWorkerDateTime = x.JobAcceptedByWorkerDateTime,
                JobStartedByWorkerDateTime = x.JobStartedByWorkerDateTime,
                JobClosedDateTime = x.JobClosedDateTime,
                JobStatus = x.JobStatus,
                CostumerRatingForWorker = x.CostumerRatingForWorker,
                WorkerRatingForCostumer = x.WorkerRatingForCostumer,
                CostumerEstimatedFinalCost = x.CostumerEstimatedFinalCost,
                WageCost = x.WageCost,
                MaterialCost = x.MaterialCost,
                CompanyProfit = x.CompanyProfit,
                FinalCost = x.FinalCost,
                CostumerName = x.Costumer!.FirstName + ' ' + x.Costumer.LastName,
                WorkerName = x.Worker != null ? x.Worker.FirstName + ' ' + x.Worker.LastName : null,
                CostumerAddressName = x.CostumerAddress!.Name,
                JobCategoryName = x.JobCategory!.Name,
                JobCityName = x.JobCity!.Name,
                CostumerId = x.CostumerId,
                JobCityId = x.JobCityId,
                JobCategoryId = x.JobCategoryId,
                CostumerAddressId = x.CostumerAddressId,
                WorkerId = x.WorkerId,
                CostumerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Customer)
                    .Select(y => y.Id)
                    .First(),
                WorkerCommentId = x.Comments.Where(y => y.UserType == UserTypeEnum.Worker)
                    .Select(y => y.Id)
                    .First()
            })
            .ToListAsync();
        return records;
    }
}