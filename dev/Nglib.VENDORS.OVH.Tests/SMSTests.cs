using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nglib.VENDORS.OVH
{
    [TestClass]
    public class SMSTests
    {

        [TestMethod]
        public async Task SmsManagerTest()
        {
            // More information for AK/AS/CK : https://docs.ovh.com/gb/en/customer/first-steps-with-ovh-api/ 
            var cred = OvhApiTestTools.GetApiCredentials();

            cred = new Nglib.VENDORS.OVH.SHARED.OvhApiCredentials() { ApplicationKey="Your-AK", ApplicationSecret="Your-AS", ConsumerKey="Your-CK" };
            var smsManager = new Nglib.VENDORS.OVH.SMS.SmsManager(cred);
            await smsManager.SendSmsAsync("+33600000000", "message test");

        }



        [TestMethod]
        public async Task SendSmsTest()
        {

            var cred = OvhApiTestTools.GetApiCredentials();
            OvhApiClient ovhApiClient = new OvhApiClient(cred);

            string serviceName = (await ovhApiClient.SMSWrapper.ServicesGet()).FirstOrDefault();
            Assert.IsNotNull(serviceName, "Please create SMS service, Go to OVH.com");

            string senderFrom = (await ovhApiClient.SMSWrapper.SendersGet(serviceName)).FirstOrDefault();

            // Write SMS model
            SMS.SmsBatchParams smsparam = new SMS.SmsBatchParams();
            smsparam.From = senderFrom;
            smsparam.To = new List<string>() { "+33600000000" }.ToArray();
            smsparam.Message = "Message test alex";
            smsparam.NoStop = true;
            smsparam.Deferred = null;

            var resSend = await ovhApiClient.SMSWrapper.SmsBatchesPut(serviceName, smsparam);
            System.Threading.Thread.Sleep(1000); // wait send

            var resInfo = await ovhApiClient.SMSWrapper.SmsBatchGet(serviceName, resSend.Id);

            var resStats = await ovhApiClient.SMSWrapper.SmsBatchStatistics(serviceName, resSend.Id);
            Assert.IsNotNull(resStats);

        }

        [TestMethod]
        public async Task InfoSmsTest()
        {
            var cred = OvhApiTestTools.GetApiCredentials();
            OvhApiClient ovhApiClient = new OvhApiClient(cred);

            string serviceName = (await ovhApiClient.SMSWrapper.ServicesGet()).FirstOrDefault();
            Assert.IsNotNull(serviceName, "Please create SMS service, Go to OVH.com");

            var resInfo = await ovhApiClient.SMSWrapper.SmsBatchGet(serviceName, "774823e5-b726-48a3-8684-b27d94d96af2");
            Assert.IsNotNull(resInfo);

            var resStats = await ovhApiClient.SMSWrapper.SmsBatchStatistics(serviceName, "774823e5-b726-48a3-8684-b27d94d96af2");
            Assert.IsNotNull(resStats);

        }

           
    }
}
