using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.Helpers;

namespace StoreManagementSystem.Service.Interfaces;

public interface IStoreService
{
    Task<Response<StoreResultDto>> AddAsync(StoreCreationDto dto);
    Task<Response<StoreResultDto>> ModifyAsync(StoreUpdateDto dto);
    Task<Response<bool>> RemoveAsync(long id);
    Task<Response<StoreResultDto>> RetrieveByIdAsync(long id);
    Task<Response<IEnumerable<StoreResultDto>>> RetrieveAllAsync();
}
