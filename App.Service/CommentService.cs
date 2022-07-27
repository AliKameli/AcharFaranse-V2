using App.Domain.Common;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Service;

public class CommentService : ICommentService
{
    private readonly AppDbContext _dbContext;

    public CommentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int commentId)
    {
        var result = await _dbContext.Comments.AnyAsync(x => x.Id == commentId);

        if (!result) throw new Exception($"گزارش با شناسه {commentId} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(CommentDto commentDto)
    {
        var result = await _dbContext.Comments.AnyAsync(x =>
            x.UserType == commentDto.UserType &&
            x.JobId == commentDto.JobId);

        if (result) throw new Exception($"گزارش {commentDto.UserType} برای این کار وجود دارد");
    }

    public async Task<int> AddAsync(CommentDto commentDto)
    {
        await EnsureDoesNotExistAsync(commentDto);

        var record = new Comment
        {
            UserType = commentDto.UserType,
            CostumerId = commentDto.CostumerId,
            WorkerId = commentDto.WorkerId,
            JobId = commentDto.JobId,
            Description = commentDto.Description
        };

        var result = await _dbContext.Comments.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(CommentDto commentDto)
    {
        await EnsureExistsByIdAsync(commentDto.Id);

        var record = await _dbContext.Comments.FirstAsync(x => x.Id == commentDto.Id);

        record.Description = commentDto.Description;
        record.IsConfirmed = commentDto.IsConfirmed;
        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int commentId)
    {
        await EnsureExistsByIdAsync(commentId);

        var record = await _dbContext.Comments.FirstAsync(x => x.Id == commentId);

        try
        {
            _dbContext.Comments.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e, message: $"گزارش {record.Id} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        var records = await _dbContext.Comments.Select(x => new CommentDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker : x.Costumer!).ToString(),
                JobId = x.JobId,
                Description = x.Description,
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<CommentDto> GetByIdAsync(int commentId)
    {
        await EnsureExistsByIdAsync(commentId);

        var record = await _dbContext.Comments
            .Select(x => new CommentDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker : x.Costumer!).ToString(),
                JobId = x.JobId,
                Description = x.Description,
                IsConfirmed = x.IsConfirmed
            })
            .FirstAsync(x => x.Id == commentId);


        return record;
    }

    public async Task<List<CommentDto>> GetByJobIdAsync(int jobId)
    {
        var records = await _dbContext.Comments
            .Where(x => x.JobId == jobId)
            .Select(x => new CommentDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker : x.Costumer!).ToString(),
                JobId = x.JobId,
                Description = x.Description,
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<CommentDto>> GetByCostumerIdAsync(int costumerId)
    {
        var records = await _dbContext.Comments
            .Where(x => x.CostumerId == costumerId)
            .Select(x => new CommentDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker : x.Costumer!).ToString(),
                JobId = x.JobId,
                Description = x.Description,
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<CommentDto>> GetByWorkerIdAsync(int workerId)
    {
        var records = await _dbContext.Comments
            .Where(x => x.WorkerId == workerId)
            .Select(x => new CommentDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                UserType = x.UserType,
                CostumerId = x.CostumerId,
                WorkerId = x.WorkerId,
                UserFullName = (x.Worker != null ? (BaseUser) x.Worker : x.Costumer!).ToString(),
                JobId = x.JobId,
                Description = x.Description,
                IsConfirmed = x.IsConfirmed
            })
            .ToListAsync();

        return records;
    }
}