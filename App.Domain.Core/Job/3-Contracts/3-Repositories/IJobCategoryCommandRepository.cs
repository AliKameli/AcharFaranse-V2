using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobCategoryCommandRepository
{
    Task AddAsync(JobCategoryDto jobCategoryDto);
    Task UpdateAsync(JobCategoryDto jobCategoryDto);
    Task DeleteAsync(int jobCategoryId);
}