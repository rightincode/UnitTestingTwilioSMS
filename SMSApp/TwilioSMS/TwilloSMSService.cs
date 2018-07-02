using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS.Interfaces;
using Twilio.Exceptions;
using System.Threading.Tasks;

namespace TwilioSMS
{
    public class TwilloSMSService : ITwilioSMSService
    {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }

        public TwilloSMSService()
        {
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
            catch (AuthenticationException e)
            {
                //Debug.WriteLine(e.Message);
                throw e;
            }
            catch (ApiException e)
            {
                //Debug.WriteLine(e.Message);
               // Debug.WriteLine($"Twilio Error {e.Code} - {e.MoreInfo}");
                throw e;
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
            catch (AuthenticationException e)
            {
                //Debug.WriteLine(e.Message);
                throw e;
            }
            catch (ApiException e)
            {
                //Debug.WriteLine(e.Message);
                //Debug.WriteLine($"Twilio Error {e.Code} - {e.MoreInfo}");
                throw e;
            }

            return messageResource;
        }
        
    }
}
