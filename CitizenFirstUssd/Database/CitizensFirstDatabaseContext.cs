using CitizenFirstUssd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CitizenFirstUssd.Database;

public class CitizensFirstDatabaseContext(DbContextOptions<CitizensFirstDatabaseContext> options) : IdentityUserContext<User, int, UserClaim, UserLogin, UserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("Users");
    }
}