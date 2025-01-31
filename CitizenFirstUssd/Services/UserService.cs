using CitizenFirstUssd.Database;
using CitizenFirstUssd.Models;
using Mapster;

namespace CitizenFirstUssd.Services;

public class UserService (CitizensFirstDatabaseContext database)
{
    public async Task<User> AddUser(UserRequest request)
    { 
        database.Users.Add(new User
        {
            
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Nrc = request.Nrc,
            PhysicalAddress = request.PhysicalAddress,
        });
        
        await database.SaveChangesAsync();
        
        return request.Adapt<User>();
    }
}