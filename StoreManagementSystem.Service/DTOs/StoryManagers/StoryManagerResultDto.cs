using StoreManagementSystem.Domain.Entities;
using StoreManagementSystem.Service.DTOs.Stories;

namespace StoreManagementSystem.Service.DTOs.StoreManagers;

public class StoreManagerResultDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public ICollection<StoreResultDto> Stores { get; set; }

}