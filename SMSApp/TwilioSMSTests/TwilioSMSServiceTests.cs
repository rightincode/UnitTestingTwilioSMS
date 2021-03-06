using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twilio.Rest.Api.V2010.Account;
using TwilioSMS;
using System.Threading.Tasks;

namespace TwilioSMSTests
{
    [TestClass]
    public class TwilioSMSServiceTests
    {
        private string AccountSID { get; set; }
        private string AuthToken { get; set; }

        private string ValidToNumber { get; set; }        
        private string InvalidToNumber { get; set; }
        private string CannotRouteToNumber { get; set; }
        private string CannotSendIntlSMSToNumber { get; set; }
        private string BlacklistedToNumber { get; set; }
        private string CannotRecieveSMSToNumber { get; set; }

        private string ValidFromNumber { get; set; }
        private string InvalidFromNumber { get; set; }
        private string NotSMSCapableFromPhoneNumber { get; set; }
        private string QueueFullFromPhoneNumber { get; set; }

        private string SMSMessage { get; set; }

        [TestInitialize]
        public void InitializeTwilioSMSServiceTests()
        {
            AccountSID = "AC2f9aaf74f002e00e3554d4fb9a37b62d";
            AuthToken = "67a209768e77668fb14d11cb28f47c32";
            
            ValidFromNumber = "15005550006";
            InvalidToNumber = "15005550001";
            CannotRouteToNumber = "15005550002";
            CannotSendIntlSMSToNumber = "15005550003";
            BlacklistedToNumber = "15005550004";
            CannotRecieveSMSToNumber = "15005550009";

            ValidToNumber = "15005550006";
            InvalidFromNumber = "15005550001";
            NotSMSCapableFromPhoneNumber = "15005550007";
            QueueFullFromPhoneNumber = "15005550008";

            SMSMessage = "Test message from SMSManagerTests";
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_Success()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            Assert.IsTrue(smsManager.Authenticate());
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_AuthenticationException()
        {
            var smsManager = new TwilloSMSService();

            Assert.ThrowsException<Twilio.Exceptions.AuthenticationException>(
                () => smsManager.Authenticate(), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public void SendSMS_ValidFromNumber_Queued()
        {
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService();

            Assert.ThrowsException<Twilio.Exceptions.AuthenticationException>(
                () => smsManager.SendSMS(ValidToNumber, ValidFromNumber, SMSMessage), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public void SendSMS_InvalidFromNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(ValidToNumber, InvalidFromNumber, SMSMessage), "Expected ApiException when using invalid from number!");

            Assert.AreEqual(21212, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_InvalidToNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(InvalidToNumber, ValidFromNumber, SMSMessage), "Expected ApiException when using invalid to number!");

            Assert.AreEqual(21211, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_NonRoutableNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(CannotRouteToNumber, ValidFromNumber, SMSMessage), "Expected ApiException when using invalid to number!");

            Assert.AreEqual(21612, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_NoIntlAccess_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(CannotSendIntlSMSToNumber, ValidFromNumber, SMSMessage), "Expected ApiException when using invalid to number!");

            Assert.AreEqual(21408, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_BlacklistedNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(BlacklistedToNumber, ValidFromNumber, SMSMessage), "Expected ApiException when using invalid to number!");

            Assert.AreEqual(21610, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_CannotRecieveSMSToPhoneNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
            {
                AccountSID = AccountSID,
                AuthToken = AuthToken
            };

            var returnedException = Assert.ThrowsException<Twilio.Exceptions.ApiException>(
                () => smsManager.SendSMS(CannotRecieveSMSToNumber, ValidFromNumber, SMSMessage), "Expected ApiException when using invalid to number!");

            Assert.AreEqual(21614, returnedException.Code);
        }

        [TestMethod]
        public void SendSMS_NotSMSCapableFromPhoneNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService();

            Assert.ThrowsExceptionAsync<Twilio.Exceptions.AuthenticationException>(
              async () => await smsManager.SendSMSAsync(ValidToNumber, ValidFromNumber, SMSMessage), "Expected AuthenticationException when using invalid from number!");
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidFromNumber_ApiException()
        {
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService
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
            var smsManager = new TwilloSMSService
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
