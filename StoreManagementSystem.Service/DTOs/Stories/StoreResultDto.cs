using StoreManagementSystem.Service.DTOs.StoreManagers;

namespace StoreManagementSystem.Service.DTOs.Stories;

public class StoreResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StoreManagerResultDto StoreManager { get; set; }
}