using Twilio.Rest.Api.V2010.Account;
using TwilioSMS.Interfaces;
using Twilio.Exceptions;
using System.Threading.Tasks;

namespace SMSManagers
{
    public class TwilioSMSManager 
    {
        private readonly ITwilioSMSService _twilioSMSService;

        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public TwilioSMSManager(ITwilioSMSService twilioSMSService, string accountSID, string authToken)
        {
            _twilioSMSService = twilioSMSService;

            if (_twilioSMSService != null)
            {
                _twilioSMSService.AccountSID = accountSID;
                _twilioSMSService.AuthToken = authToken;
                _twilioSMSService.Authenticate();
            }
        }
        
        public bool SendSMS(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            MessageResource messageResource = null;
            ClearError();

            try
            {
               messageResource = _twilioSMSService.SendSMS(toPhoneNumber, fromPhoneNumber, message);
            }
            catch (AuthenticationException e)
            {
                ErrorMessage = e.Message;
            }
            catch (ApiException e)
            {
                ErrorCode = e.Code;
                ErrorMessage = e.MoreInfo;
            }
            
            return (messageResource != null) ? true : false ;
        }

        public async Task<bool> SendSMSAsync(string toPhoneNumber, string fromPhoneNumber, string message)
        {
            MessageResource messageResource = null;
            ClearError();

            try
            {
                messageResource = await _twilioSMSService.SendSMSAsync(toPhoneNumber, fromPhoneNumber, message);
            }
            catch (AuthenticationException e)
            {
                ErrorMessage = e.Message;
            }
            catch (ApiException e)
            {
                ErrorCode = e.Code;
                ErrorMessage = e.MoreInfo;
            }

            return (messageResource != null) ? true : false;
        }

        private void ClearError()
        {
            ErrorCode = 0;
            ErrorMessage = string.Empty;
        }
    }
}
