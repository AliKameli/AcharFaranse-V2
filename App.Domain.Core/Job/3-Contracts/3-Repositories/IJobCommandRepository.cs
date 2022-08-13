using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobCommandRepository
{
    Task AddAsync(JobDto jobDto);
    Task UpdateAsync(JobDto jobDto);
    Task DeleteAsync(int jobId);
}