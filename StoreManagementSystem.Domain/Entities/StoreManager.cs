using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Domain.Entities;

public class StoreManager : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}
