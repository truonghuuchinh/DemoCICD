using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Abstractions.Entities.Identity;
public class AppUser : IdentityUser<Guid>
{
    public string FisrtName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public bool? IsDirector { get; set; }
}
