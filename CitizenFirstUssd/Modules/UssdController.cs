using CitizenFirstUssd.Controllers;
using CitizenFirstUssd.Database;
using CitizenFirstUssd.Modules.Menus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UssdStateMachine;

namespace CitizenFirstUssd.Modules;

[ApiController, Route("api/ussd")]
public class UssdController(
    IUssdService ussdService, 
    CitizensFirstDatabaseContext database
    ) : ControllerBase
{
    [HttpGet]
    public async Task MainMenu([FromQuery] UssdRequest request)
    {
        var registeredUser = await database.Users.AnyAsync(x=> x.PhoneNumber == request.Msisdn);
        
        if (!registeredUser)
        {
            await ussdService.ProcessRequestAsync<UnregisteredMenu>(request, ServiceProvider(request.Msisdn));
            return;
        }

        await ussdService.ProcessRequestAsync<MainMenu>(request, ServiceProvider(request.Msisdn));
    }
    
    private static NetworkServiceProvider ServiceProvider(string msisdn)
    {
        var network = msisdn[4].ToString();

        return network switch
        {
            "7" => NetworkServiceProvider.Airtel,
            "6" => NetworkServiceProvider.Mtn,
            "5" => NetworkServiceProvider.Zamtel,
            _ => throw new ArgumentOutOfRangeException(nameof(msisdn), msisdn, null)
        };
    }
}