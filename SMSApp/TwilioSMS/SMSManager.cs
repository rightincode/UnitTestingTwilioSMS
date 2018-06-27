using System;
using Twilio;
using Twilio.Types;
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
        public bool Authenticate()
        {
            throw new NotImplementedException();
        }

        public bool SendSMS(string ToPhoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
