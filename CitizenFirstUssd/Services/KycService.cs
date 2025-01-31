using Flurl.Http;

namespace CitizenFirstUssd.Services;

public class KycService
{
    
    public Task<KycResponse> GetClientDetails(string phoneNumber)
    {
        return $"{GetKycUrl(phoneNumber)}/{phoneNumber}"
            .GetJsonAsync<KycResponse>();
    }

    private string GetKycUrl(string phoneNumber)
    {
        var index = phoneNumber[4].ToString();
        if (index == "7")
        {
            return "https://patumba-airtel.hobbiton.app/kyc";
        }

        if (index == "6")
        {
            return "https://patumba-mtn.hobbiton.dev/kyc";
        }

        if (index == "5")
        {
            return "https://zamtel-kyc.hobbiton.io/kyc";
        }

        throw new Exception("Invalid phone number");
    }
}