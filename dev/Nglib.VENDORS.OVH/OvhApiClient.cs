using Nglib.VENDORS.OVH.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH
{
    public class OvhApiClient
    {
        private SHARED.IOvhApiCredentials Credentials { get; set; }
        private System.Net.Http.HttpClient client { get; set; }
        public OvhApiClient(SHARED.IOvhApiCredentials ovhApiCredentials)
        {
            this.Credentials = ovhApiCredentials;
            this.client = OvhApiTools.FactoryClient(this.Credentials);
        }




        public OVH.AUTH.AUTHWrapper AUTHWrapper => new AUTH.AUTHWrapper(this.Credentials, this.client);
        public OVH.DOMAIN.DOMAINWrapper DOMAINWrapper => new DOMAIN.DOMAINWrapper(this.Credentials, this.client);
        public OVH.SMS.SMSWrapper SMSWrapper => new SMS.SMSWrapper(this.Credentials, this.client);


    }
}
