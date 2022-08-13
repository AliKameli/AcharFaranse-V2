using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobCategoryQueryRepository
{
    Task<List<JobCategoryDto>> GetAllAsync();
    Task<JobCategoryDto?> GetByIdAsync(int jobCategoryId);
    Task<JobCategoryDto?> GetByNameAsync(string jobCategoryName);
}