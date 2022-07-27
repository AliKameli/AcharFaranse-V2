using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface ICostumerAppService
{
    Task<int> AddAsync(CostumerDto costumerDto);
    Task UpdateAsync(CostumerDto costumerDto);
    Task DeleteAsync(int costumerId);
    Task<List<CostumerDto>> GetAllAsync();
    Task<CostumerDto> GetByIdAsync(int costumerId);
    Task<CostumerDto> GetByNationalIdAsync(string nationalId);
    Task<List<CostumerDto>> GetByCityIdAsync(int cityId);
    Task<List<CostumerDto>> SearchAsync(string? name = null, string? nationalId = null);
    Task ConfirmAsync(int costumerId);
}