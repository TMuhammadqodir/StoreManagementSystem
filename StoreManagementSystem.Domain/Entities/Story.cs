using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Domain.Entities;

public class Story : Auditable
{
    public string Name { get; set; }
    public long StoreManagerId { get; set; }
    public StoreManager StoreManager { get; set; }
}
