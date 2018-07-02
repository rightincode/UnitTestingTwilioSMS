using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSMS.Interfaces
{
    public interface ITwilioSMSService
    {
        string AccountSID { get; set; }
        string AuthToken { get; set; }

        bool Authenticate();

        MessageResource SendSMS(string toPhoneNumber, string fromPhoneNumber, string message);

        Task<MessageResource> SendSMSAsync(string toPhoneNumber, string fromPhoneNumber, string message);
    }
}
