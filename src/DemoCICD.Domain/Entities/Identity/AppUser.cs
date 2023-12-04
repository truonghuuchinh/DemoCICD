using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity;
public class AppUser : IdentityUser<Guid>
{
    public string FisrtName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public bool? IsDirector { get; set; }

    public bool? IsHeadOfDepartment { get; set; }

    public Guid ManagerId { get; set; }

    public Guid DepartmentId { get; set; }

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }

    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }

    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
}
