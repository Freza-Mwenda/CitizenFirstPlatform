using CitizenFirstUssd.Controllers;
using CitizenFirstUssd.Modules.Menus.Registration;
using CitizenFirstUssd.Utils;
using UssdStateMachine;
using UssdStateMachine.DefaultMenus;

namespace CitizenFirstUssd.Modules.Menus;

[UssdMenu]
public class RegistrationMenu(IUssdMenuBuilder builder) : UssdMenuBase(builder)
{
    public override async Task<string> DisplayAsync()
    {
        return await Builder.SetPrompt("To begin registration, please enter your NRC below:")
            .Build();
    }

    public override async Task ProcessInputAsync()
    {
        var nrc = Builder.Request.Input;
        if (nrc is null)
        {
            await Builder.NavigateTo<CommonInputErrorMenu>();
        }
        else
        {
            await Builder.SaveToSessionAsync(SessionKeys.Nrc, nrc.FormatNrc());
            await Builder.NavigateTo<AddressMenu>();
        }
    }
}