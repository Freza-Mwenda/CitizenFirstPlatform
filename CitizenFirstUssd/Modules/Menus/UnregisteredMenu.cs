using CitizenFirstUssd.Controllers;
using UssdStateMachine;

namespace CitizenFirstUssd.Modules.Menus;

[UssdMenu]
public class UnregisteredMenu(IUssdMenuBuilder builder) : UssdMenuBase(builder)
{
    public override Task<string> DisplayAsync()
    {
        return Builder.SetPrompt("Welcome to Citizen First")
            .AddOption("1. Register")
            .Build();
    }

    public override async Task ProcessInputAsync()
    {
        await Builder.ProcessInputAsString()
            .Case("1", async () =>
            {
                await Builder.NavigateTo<RegistrationMenu>();
            })
            .FailWith(exception => NavigateErrorTo<ErrorMenu>($"An error occurred. Reason: {exception.Message}"));
    }
}