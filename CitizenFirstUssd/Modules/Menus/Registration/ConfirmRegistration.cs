using CitizenFirstUssd.Controllers;
using CitizenFirstUssd.Models;
using CitizenFirstUssd.Services;
using UssdStateMachine;

namespace CitizenFirstUssd.Modules.Menus.Registration;

[UssdMenu]
public class ConfirmRegistration(
    IUssdMenuBuilder builder,
    KycService service,
    UserService userService
) : UssdMenuBase(builder)
{
    public override async Task<string> DisplayAsync()
    {
        var kycDetails = new KycResponse();
        try
        {
            kycDetails = await service.GetClientDetails(Builder.Request.Msisdn);
        }
        catch (Exception ex)
        {
            await Builder.NavigateErrorTo<ErrorMenu>("An error occurred");
        }

        var user = new User
        {
            Nrc = await Builder.GetFromSessionAsync<string>(SessionKeys.Nrc),
            PhoneNumber = Builder.Request.Msisdn,
            LastName = kycDetails.LastName,
            FirstName = kycDetails.FirstName,
            PhysicalAddress = await Builder.GetFromSessionAsync<string>(SessionKeys.PhysicalAddress),
        };

        await SaveToSessionAsync(SessionKeys.User, user);

        return await Builder.SetPrompt($"Verify the following details to register for Citizens first:" +
                                       $"\n Names: {user.FirstName + " " + user.LastName} " +
                                       $"\n Nrc: {user.Nrc}" +
                                       $"\n Address: {user.PhysicalAddress}")
            .AddOption("1. Confirm Details")
            .AddOption("2. Cancel")
            .Build();
    }

    public override async Task ProcessInputAsync()
    {
        var user = await Builder.GetFromSessionAsync<User>(SessionKeys.User);

        await Builder.ProcessInputAsString()
            .Case("1", async () =>
            {
                try
                {
                    await userService.AddUser(new UserRequest
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Nrc = user.Nrc,
                        PhysicalAddress = user.PhysicalAddress,
                        PhoneNumber = user.PhoneNumber,
                    });

                    await Builder.NavigateTo<MainMenu>();
                }
                catch (Exception e)
                {
                    await Builder.NavigateErrorTo<ErrorMenu>("An error occurred");
                }
            })
            .Case("2", async () => { await Builder.NavigateTo<RegistrationMenu>(); })
            .FailWith(exception => NavigateErrorTo<ErrorMenu>($"An error occurred. Reason: {exception.Message}"));
    }
}