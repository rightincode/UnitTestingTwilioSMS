using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS.Interfaces;

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
        public void Authenticate()
        {
            throw new NotImplementedException();
        }

        public string SendSMS(string ToPhoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
