using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Enums;

namespace App.AppServices;

public class CommentAppService : ICommentAppService
{
    private readonly ICommentRepo _commentService;
    private readonly ICostumerRepo _costumerService;
    private readonly IJobRepo _jobService;
    private readonly IWorkerRepo _workerService;

    public CommentAppService(ICommentRepo commentService,
        ICostumerRepo costumerService,
        IJobRepo jobService,
        IWorkerRepo workerService)
    {
        _commentService = commentService;
        _costumerService = costumerService;
        _jobService = jobService;
        _workerService = workerService;
    }

    public async Task<int> AddAsync(CommentDto commentDto)
    {
        await (commentDto.UserType == UserTypeEnum.Customer
            ? _costumerService.EnsureExistsByIdAsync((int) commentDto.CostumerId!)
            : _workerService.EnsureExistsByIdAsync((int) commentDto.WorkerId!));
        await _jobService.EnsureExistsByIdAsync(commentDto.JobId);

        return await _commentService.AddAsync(commentDto);
    }

    public async Task UpdateAsync(CommentDto commentDto)
    {
        var record = await _commentService.GetByIdAsync(commentDto.Id);
        var recordJob = await _jobService.GetByIdAsync(record.JobId);

        if (recordJob.IsClosed) throw new Exception("کار مربوطه بسته شده و قابل تغییر نیست");

        await _commentService.UpdateAsync(commentDto);
    }

    public async Task DeleteAsync(int commentId)
    {
        var record = await _commentService.GetByIdAsync(commentId);
        var recordJob = await _jobService.GetByIdAsync(record.JobId);

        if (recordJob.IsClosed && record.IsConfirmed) throw new Exception("کار مربوطه بسته شده و قابل تغییر نیست");

        await _commentService.DeleteAsync(commentId);
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        return await _commentService.GetAllAsync();
    }

    public async Task<CommentDto> GetByIdAsync(int commentId)
    {
        return await _commentService.GetByIdAsync(commentId);
    }

    public async Task<List<CommentDto>> GetByJobIdAsync(int jobId)
    {
        return await _commentService.GetByJobIdAsync(jobId);
    }

    public async Task<List<CommentDto>> GetByCostumerIdAsync(int costumerId)
    {
        return await _commentService.GetByCostumerIdAsync(costumerId);
    }

    public async Task<List<CommentDto>> GetByWorkerIdAsync(int workerId)
    {
        return await _commentService.GetByWorkerIdAsync(workerId);
    }

    public async Task ConfirmAsync(int commentId)
    {
        var record = await _commentService.GetByIdAsync(commentId);
        record.IsConfirmed = true;

        await _commentService.UpdateAsync(record);
    }
}