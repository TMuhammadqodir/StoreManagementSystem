using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Domain.Entities;

public class StoreManager : Auditable
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public ICollection<Store> Stores { get; set;}
}
