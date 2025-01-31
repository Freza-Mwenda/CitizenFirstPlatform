using CitizenFirstUssd.Controllers;
using UssdStateMachine;
using UssdStateMachine.DefaultMenus;

namespace CitizenFirstUssd.Modules.Menus.Registration;

[UssdMenu]
public class AddressMenu(
    IUssdMenuBuilder builder
) : UssdMenuBase(builder)
{
    public override Task<string> DisplayAsync()
    {
        return Builder.SetPrompt("Please enter your residential address below:")
            .Build();
        
    }

    public override async Task ProcessInputAsync()
    {
        var address = Builder.Request.Input;

        if (address is null)
        {
            await Builder.NavigateTo<CommonInputErrorMenu>();
        }
        else
        {
            await Builder.SaveToSessionAsync(SessionKeys.PhysicalAddress, Builder.Request.Input);
            await Builder.NavigateTo<ConfirmRegistration>();
        }
    }
}