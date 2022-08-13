using App.Domain.Core.Job._1_Entitys;
using App.Domain.Core.Job._2_Dtos;

namespace App.Domain.Core.Job._3_Contracts._3_Repositories;

public interface IJobQueryRepository
{
    Task<List<JobDto>> GetAllAsync();
    Task<List<JobDto>> GetAllByWorkerIdAsync(int workerId);
    Task<List<JobDto>> GetAllByCostumerIdAsync(int costumerId);
    Task<JobDto?> GetByIdAsync(int jobId);
}