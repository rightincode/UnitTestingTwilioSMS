using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS.Interfaces;

namespace SMSManagersTests.Mocks
{
    public class TwilioSMSServiceMock : ITwilioSMSService
    {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }

        public TwilioSMSServiceMock() { }

        public bool Authenticate()
        {
            return (!string.IsNullOrEmpty(AccountSID)) ? true : false;
        }

        public MessageResource SendSMS(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            if ((string.IsNullOrEmpty(AccountSID)) || (string.IsNullOrEmpty(AuthToken)))
            {
                throw new AuthenticationException("Missing/Invalid AccountSID or AuthToken");
            }

            if ((string.IsNullOrEmpty(toPhoneNumber)) || (string.IsNullOrEmpty(fromPhoneNumber)))
            {
                throw new ApiException(111, 111, "Invalid To/From phone number", "Invalid To/From phone number");
            }

            string messageResourceJson = "{ 'To':'" + toPhoneNumber + "', 'From':'" + fromPhoneNumber + "', 'status':'Queued'}";

            return MessageResource.FromJson(messageResourceJson);
        }

        public async Task<MessageResource> SendSMSAsync(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            return await Task.Run(() => {
                if ((string.IsNullOrEmpty(AccountSID)) || (string.IsNullOrEmpty(AuthToken)))
                {
                    throw new AuthenticationException("Missing/Invalid AccountSID or AuthToken");
                }

                if ((string.IsNullOrEmpty(toPhoneNumber)) || (string.IsNullOrEmpty(fromPhoneNumber)))
                {
                    throw new ApiException(112, 112, "Invalid To/From phone number", "Invalid To/From phone number");
                }

                string messageResourceJson = "{ 'To':'" + toPhoneNumber + "', 'From':'" + fromPhoneNumber + "', 'status':'Queued'}";                

                return MessageResource.FromJson(messageResourceJson);
            });
        }
    }
}
