using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.AppServices;

public class WorkerAppService : IWorkerAppService
{
    private readonly UserManager<IdentityUser<int>> _userManager;
    private readonly IWorkerService _workerService;


    public WorkerAppService(IWorkerService workerService, UserManager<IdentityUser<int>> userManager)
    {
        _workerService = workerService;
        _userManager = userManager;
    }

    public async Task<int> AddAsync(WorkerDto workerDto)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(WorkerDto workerDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int workerId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<WorkerDto> GetByIdAsync(int workerId)
    {
        throw new NotImplementedException();
    }

    public async Task<WorkerDto> GetByNationalIdAsync(string nationalId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WorkerDto>> GetByJobCategoryIdAsync(int jobCategoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WorkerDto>> GetByCityIdAsync(int cityId)
    {
        var result = await _workerService.GetByCityIdAsync(cityId);
        return result;
    }

    public async Task<List<WorkerDto>> SearchAsync(string? name = null, string? nationalId = null)
    {
        throw new NotImplementedException();
    }
}