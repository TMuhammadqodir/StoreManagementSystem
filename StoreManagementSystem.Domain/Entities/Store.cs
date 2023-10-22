using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Domain.Entities;

public class Store : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long StoreManagerId { get; set; }
    public StoreManager StoreManager { get; set; }
}
