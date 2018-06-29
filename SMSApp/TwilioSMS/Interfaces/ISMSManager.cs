using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSMS.Interfaces
{
    public interface ISMSManager
    {
        string AccountSID { get; }
        string AuthToken { get; }

        bool Authenticate();

        MessageResource SendSMS(string toPhoneNumber, string fromPhoneNumber, string message);

        Task<MessageResource> SendSMSAsync(string toPhoneNumber, string fromPhoneNumber, string message);
    }
}
