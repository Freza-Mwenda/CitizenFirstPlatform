using UssdStateMachine;

namespace CitizenFirstUssd.Modules;

[UssdMenu]
public class MainMenu(IUssdMenuBuilder builder) : UssdMenuBase(builder)
{
    public override Task<string> DisplayAsync()
    {
        return Builder.SetPrompt("Welcome to Citizen First")
            .AddOption("1. What is Citizens First")
            .AddOption("2. Citizens ")
            .AddOption("3. Be part of the Activities")
            .AddOption("4. Be Empowered, join CFCP")
            .AddOption("5. Recruit New Members")
            .AddOption("6. CF Media")
            .Build();
    }
}