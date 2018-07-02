using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMSManagers;
using SMSManagersTests.Mocks;
using System.Threading.Tasks;

namespace SMSManagersTests
{
    [TestClass]
    public class TwilioSMSManagerTests
    {

        private string AccountSID { get; set; }
        private string AuthToken { get; set; }

        private string SMSMessage { get; set; }

        private string ValidFromNumber { get; set; }

        private string ValidToNumber { get; set; }

        [TestInitialize]
        public void InitializeTwilioSMSManagerTests()
        {
            AccountSID = "FakeAccountSID";
            AuthToken = "FakeAuthToken";

            ValidFromNumber = "7045551212";
            ValidToNumber = "7045552121";
        }

        [TestMethod]
        public void SendSMS_InvalidAccountSIDAndAuthToken_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), string.Empty, string.Empty);

            bool result = twilioSMSManager.SendSMS(ValidFromNumber, ValidToNumber, SMSMessage);

            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public void SendSMS_ValidToAndFromNumbers_Success()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = twilioSMSManager.SendSMS(ValidToNumber, ValidToNumber, SMSMessage);

            Assert.IsTrue(result);
            Assert.AreEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public void SendSMS_InValidToNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = twilioSMSManager.SendSMS("", ValidFromNumber, SMSMessage);

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public void SendSMS_InValidFromNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = twilioSMSManager.SendSMS(ValidToNumber, "", SMSMessage);

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public void SendSMS_InValidToAndFromNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = twilioSMSManager.SendSMS("", "", SMSMessage);

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidAccountAIDAndAuthToken_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), string.Empty, string.Empty);

            bool result = await Task.Run(() => {
                return twilioSMSManager.SendSMSAsync(ValidFromNumber, ValidToNumber, SMSMessage);
            });

            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public async Task SendSMSAsync_ValidToAndFromNumbers_Success()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = await Task.Run(() => {
                return twilioSMSManager.SendSMSAsync(ValidFromNumber, ValidToNumber, SMSMessage);
            });

            Assert.IsTrue(result);
            Assert.AreEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidToNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = await Task.Run(() => {
                return twilioSMSManager.SendSMSAsync("", ValidToNumber, SMSMessage);
            });

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidFromNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = await Task.Run(() => {
                return twilioSMSManager.SendSMSAsync(ValidToNumber, "", SMSMessage);
            });

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }

        [TestMethod]
        public async Task SendSMSAsync_InvalidToAndFromNumber_Failure()
        {
            var twilioSMSManager = new TwilioSMSManager(new TwilioSMSServiceMock(), AccountSID, AuthToken);

            bool result = await Task.Run(() => {
                return twilioSMSManager.SendSMSAsync("", "", SMSMessage);
            });

            Assert.IsFalse(result);
            Assert.AreNotEqual(0, twilioSMSManager.ErrorCode);
            Assert.AreNotEqual(string.Empty, twilioSMSManager.ErrorMessage);
        }
    }
}
