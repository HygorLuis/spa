using Microsoft.AspNetCore.Identity;

namespace SPA.Domain.Entities;

public class Usuario : IdentityUser<Guid> 
{
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? InactivationDate { get; set; }
}