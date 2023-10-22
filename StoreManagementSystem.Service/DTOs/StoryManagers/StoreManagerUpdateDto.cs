using StoreManagementSystem.Service.DTOs.Stories;

namespace StoreManagementSystem.Service.DTOs.StoryManagers;

public class StoreManagerUpdateDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<StoreResultDto> Stores { get; set; }
}
