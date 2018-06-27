using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwilioSMS;

namespace TwilioSMSTests
{
    [TestClass]
    public class SMSManagerTests
    {
        private string AccountSID { get; set; }
        private string AuthToken { get; set; }
        private string ValidNumber { get; set; }

        [TestInitialize]
        public void InitializeSMSManagerTests()
        {
            AccountSID = "";
            AuthToken = "";
            ValidNumber = "";
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_Success()
        {
            var smsManager = new SMSManager(AccountSID, AuthToken);

            Assert.IsTrue(smsManager.Authenticate());
        }

        [TestMethod]
        public void Authenticate_InValidCredentials_Fail()
        {
            var smsManager = new SMSManager(AccountSID, AuthToken);

            Assert.IsFalse(smsManager.Authenticate());
        }

        [TestMethod]
        public void SendSMS_ValidNumber_Sid()
        {
            var smsManager = new SMSManager(AccountSID, AuthToken);

            bool sent = smsManager.SendSMS(ValidNumber);

            Assert.IsTrue(sent);
        }
    }
}
