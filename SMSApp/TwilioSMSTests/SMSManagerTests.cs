using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS;
using System.Threading.Tasks;

namespace TwilioSMSTests
{
    [TestClass]
    public class SMSManagerTests
    {
        private string AccountSID { get; set; }
        private string AuthToken { get; set; }
        private string ValidToNumber { get; set; }
        private string ValidFromNumber { get; set; }
        private string SMSMessage { get; set; }

        [TestInitialize]
        public void InitializeSMSManagerTests()
        {
            AccountSID = "AC2f9aaf74f002e00e3554d4fb9a37b62d";
            AuthToken = "67a209768e77668fb14d11cb28f47c32";
            ValidToNumber = "17047771927";
            ValidFromNumber = "15005550006";
            SMSMessage = "Test message from SMSManagerTests";
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_Success()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            Assert.IsTrue(smsManager.Authenticate());
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_Failure()
        {
            var smsManager = new SMSManager();

            Assert.ThrowsException<Twilio.Exceptions.AuthenticationException>(
                () => smsManager.Authenticate());
        }

        [TestMethod]
        public void SendSMS_ValidNumber_Queued()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            MessageResource messageResource = smsManager.SendSMS(ValidToNumber, ValidFromNumber, SMSMessage);

            Assert.IsNotNull(messageResource);
            Assert.IsTrue(messageResource.Status == MessageResource.StatusEnum.Queued);
        }

        [TestMethod]
        public async Task SendSMSAsync_ValidNumber_Queued()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            MessageResource messageResource = await smsManager.SendSMSAsync(ValidToNumber, ValidFromNumber, SMSMessage);

            Assert.IsNotNull(messageResource);
            Assert.IsTrue(messageResource.Status == MessageResource.StatusEnum.Queued);
        }
    }
}
