using StoreManagementSystem.Service.DTOs.StoreManagers;
using StoreManagementSystem.Service.DTOs.StoryManagers;
using StoreManagementSystem.Service.Helpers;

namespace StoreManagementSystem.Service.Interfaces;

public interface IStoreManagerService
{
    Task<Response<StoreManagerResultDto>> AddAsync(StoreManagerCreationDto dto);
    Task<Response<StoreManagerResultDto>> ModifyAsync(StoreManagerUpdateDto dto);
    Task<Response<bool>> RemoveAsync(long id);
    Task<Response<StoreManagerResultDto>> RetrieveByIdAsync(long id);
    Task<Response<StoreManagerResultDto>> RetrieveByUsernameAsync(string username);
    Task<Response<IEnumerable<StoreManagerResultDto>>> RetrieveAllAsync();
    Task<Response<bool>> IsStrongPassword(string password);
    Task<Response<bool>> IsValidPassword(string username, string password);
}
