using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobCategoryWorkerQueryRepository
{
    Task<List<JobCategoryWorkerDto>> GetAllAsync();
    Task<List<JobCategoryWorkerDto>> GetAllByWorkerIdAsync(int workerId);
    Task<List<JobCategoryWorkerDto>> GetAllByJobCategoryIdAsync(int jobCategoryId);
    Task<JobCategoryWorkerDto?> GetByIdAsync(int jobCategoryWorkerId);
}