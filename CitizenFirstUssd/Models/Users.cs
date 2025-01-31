using Microsoft.AspNetCore.Identity;

namespace CitizenFirstUssd.Models;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Nrc { get; set; }
    public string? PhysicalAddress { get; set; }
}

public class Role : IdentityRole<int>
{
}

public class UserRole : IdentityUserRole<int>
{
    public int Id { get; set; }
}

public class UserClaim : IdentityUserClaim<int>
{
}

public class RoleClaim : IdentityRoleClaim<int>
{
}

public class UserLogin : IdentityUserLogin<int>
{
    public int Id { get; set; }
}

public class UserToken : IdentityUserToken<int>
{
    public int Id { get; set; }
}

public class UserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Nrc { get; set; }
    public string? PhysicalAddress { get; set; }
    
    public string? PhoneNumber { get; set; }
}