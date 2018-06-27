using System;
using System.Collections.Generic;
using System.Text;

namespace TwilioSMS.Interfaces
{
    public interface ISMSManager
    {
        string AccountSID { get; }
        string AuthToken { get; }

        bool Authenticate();

        bool SendSMS(string ToPhoneNumber);
    }
}
