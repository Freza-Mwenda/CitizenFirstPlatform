using UssdStateMachine;

namespace CitizenFirstUssd.Controllers;

[UssdMenu]
public class ErrorMenu : UssdMenuBase
{
    public ErrorMenu(IUssdMenuBuilder builder) : base(builder)
    {
    }

    public override Task<string> DisplayAsync()
    {
        return Builder.SetPrompt($"{GetErrorFromSession()}")
            .AddOption("1. Go back")
            .Build();
    }

    public override Task ProcessInputAsync()
    {
        return Builder.ProcessInputAsString()
            .Case("1", async () => { await Builder.NavigateBackAsync(); })
            .FailWith(_ => CloseSessionAsync());
    }
}