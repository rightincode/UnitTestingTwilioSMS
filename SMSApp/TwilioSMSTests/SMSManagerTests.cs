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
        private string InvalidToNumber { get; set; }
        private string InvalidFromNumber { get; set; }
        private string NotSMSCapableFromPhoneNumber { get; set; }
        private string QueueFullFromPhoneNumber { get; set; }

        private string SMSMessage { get; set; }

        [TestInitialize]
        public void InitializeSMSManagerTests()
        {
            AccountSID = "AC2f9aaf74f002e00e3554d4fb9a37b62d";
            AuthToken = "67a209768e77668fb14d11cb28f47c32";
            ValidToNumber = "17047771927";
            ValidFromNumber = "15005550006";
            InvalidToNumber = "1777777777";
            InvalidFromNumber = "15005550001";
            NotSMSCapableFromPhoneNumber = "15005550007";
            QueueFullFromPhoneNumber = "15005550008";

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
        public void Authenticate_InvalidCredentials_AuthenticationException()
        {
            var smsManager = new SMSManager();

            Assert.ThrowsException<Twilio.Exceptions.AuthenticationException>(
                () => smsManager.Authenticate(), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public void SendSMS_ValidFromNumber_Queued()
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
        public void SendSMS_ValidNumberInvalidCredentials_AuthenticationException()
        {
            var smsManager = new SMSManager();

            Assert.ThrowsException<Twilio.Exceptions.AuthenticationException>(
                () => smsManager.SendSMS(ValidToNumber, ValidFromNumber, SMSMessage), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public void SendSMS_InvalidFromNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(ValidToNumber, InvalidFromNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21212, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_NotSMSCapableFromPhoneNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(ValidToNumber, NotSMSCapableFromPhoneNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21606, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_QueueFullFromPhoneNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(ValidToNumber, QueueFullFromPhoneNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21611, returnedException.Code);
        }

        [TestMethod]
        public async Task SendSMSAsync_ValidFromNumber_Queued()
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

        [TestMethod]
        public void SendSMSAsync_ValidNumberInvalidCredentials_AuthenticationException()
        {
            var smsManager = new SMSManager();

            Assert.ThrowsExceptionAsync<Twilio.Exceptions.AuthenticationException>(
              async () => await smsManager.SendSMSAsync(ValidToNumber, ValidFromNumber, SMSMessage), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidFromNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = await Assert.ThrowsExceptionAsync<Twilio.Exceptions.ApiException>(
               async () => await smsManager.SendSMSAsync(ValidToNumber, InvalidFromNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21212, returnedException.Code);
        }

        [TestMethod]
        public async Task SendSMSAsync_NotSMSCapableFromPhoneNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = await Assert.ThrowsExceptionAsync<Twilio.Exceptions.ApiException>(
               async () => await smsManager.SendSMSAsync(ValidToNumber, NotSMSCapableFromPhoneNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21606, returnedException.Code);
        }

        [TestMethod]
        public async Task SendSMSAsync_QueueFullFromPhoneNumber_ApiException()
        {
            var smsManager = new SMSManager
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = await Assert.ThrowsExceptionAsync<Twilio.Exceptions.ApiException>(
               async () => await smsManager.SendSMSAsync(ValidToNumber, QueueFullFromPhoneNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21611, returnedException.Code);
        }
    }
}
