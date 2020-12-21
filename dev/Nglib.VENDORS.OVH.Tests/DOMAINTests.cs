using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Nglib.VENDORS.OVH
{
    [TestClass]
    public class DOMAINTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var cred = OvhApiTestTools.GetApiCredentials();
            OvhApiClient ovhApiClient = new OvhApiClient(cred);
            var resmodel = await ovhApiClient.DOMAINWrapper.GetDomainFull("nuegy.net");
            Assert.IsNotNull(resmodel);
        }
    }
}
