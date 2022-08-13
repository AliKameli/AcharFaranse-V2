using App.Domain.Core.User.Costumer._2_Dtos;
using App.Domain.Core.User.Worker._2_Dtos;

namespace App.Domain.Core.User.Worker._3_Contracts._3_Repositories;

public interface IWorkerCommandRepository
{
    Task AddAsync(WorkerDto workerDto);
    Task UpdateAsync(WorkerDto workerDto);
    Task DeleteAsync(int workerId);
}