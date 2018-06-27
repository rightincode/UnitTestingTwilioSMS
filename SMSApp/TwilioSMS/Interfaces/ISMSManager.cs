using System;
using System.Collections.Generic;
using System.Text;

namespace TwilioSMS.Interfaces
{
    public interface ISMSManager
    {
        string AccountSID { get; }
        string AuthToken { get; }

        void Authenticate();

        string SendSMS(string ToPhoneNumber);
    }
}
