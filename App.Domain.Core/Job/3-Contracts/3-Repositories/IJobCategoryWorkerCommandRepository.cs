using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobCategoryWorkerCommandRepository
{
    Task AddAsync(JobCategoryWorkerDto jobCategoryWorkerDto);
    Task DeleteAsync(int jobCategoryWorkerId);
}