using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Worker._2_Dtos;

namespace App.Domain.Core.User.Worker._3_Contracts._3_Repositories;

public interface IWorkerQueryRepository
{
    Task<List<WorkerDto>> GetAllAsync();
    Task<WorkerDto?> GetByIdAsync(int workerId);
    Task<WorkerDto?> GetByNationalSecurityIdAsync(string workerNationalSecurityId);
}