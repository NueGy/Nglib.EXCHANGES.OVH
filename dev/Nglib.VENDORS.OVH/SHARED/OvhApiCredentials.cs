using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.SHARED
{
    public class OvhApiCredentials: IOvhApiCredentials
    {
        /// <summary>
        /// API Endpoint that this <c>Client</c> targets
        /// </summary>
        public Uri Endpoint { get; set; } = new Uri("https://api.ovh.com/1.0/");

        /// <summary>
        /// name, Not required
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// API application Key
        /// </summary>
        public string ApplicationKey { get; set; }
        /// <summary>
        /// API application secret
        /// </summary>
        public string ApplicationSecret { get; set; }
        /// <summary>
        /// Consumer key 
        /// </summary>
        public string ConsumerKey { get; set; }
    }
}
