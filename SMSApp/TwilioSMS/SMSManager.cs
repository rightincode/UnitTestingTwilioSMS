using System;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS.Interfaces;
using Twilio.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TwilioSMS
{
    public class SMSManager : ISMSManager
    {
        public string AccountSID { get; private set; }

        public string AuthToken { get; private set; }

        public SMSManager(string accountSID, string authToken)
        {
            AccountSID = accountSID;
            AuthToken = authToken;
        }
        public bool Authenticate()
        {
            TwilioClient.Init(AccountSID, AuthToken);

            var restClient = TwilioClient.GetRestClient();

            return (restClient.AccountSid == AccountSID) ? true : false;
        }

        public MessageResource SendSMS(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            MessageResource messageResource = null;

            try
            {
                TwilioClient.Init(AccountSID, AuthToken);

                messageResource = MessageResource.Create(body: message,
                    from: new PhoneNumber(fromPhoneNumber),
                    to: new PhoneNumber(toPhoneNumber));
                               
            }
            catch (ApiException e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine($"Twilio Error {e.Code} - {e.MoreInfo}");
            }

            return messageResource;
        }

        public async Task<MessageResource> SendSMSAsync(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            MessageResource messageResource = null;

            try
            {
                TwilioClient.Init(AccountSID, AuthToken);

                messageResource = await MessageResource.CreateAsync(body: message,
                    from: new PhoneNumber(fromPhoneNumber),
                    to: new PhoneNumber(toPhoneNumber));

            }
            catch (ApiException e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine($"Twilio Error {e.Code} - {e.MoreInfo}");
            }

            return messageResource;
        }
    }
}
