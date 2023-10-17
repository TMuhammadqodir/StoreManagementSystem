using StoreManagementSystem.Service.DTOs.StoryManagers;

namespace StoreManagementSystem.Service.DTOs.Stories;

public class StoryResultDto
{
    public string Name { get; set; }
    public StoryManagerResultDto StoryManagerResult { get; set; }
}