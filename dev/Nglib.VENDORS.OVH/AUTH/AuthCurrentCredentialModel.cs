using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.AUTH
{
    public class AuthCurrentCredentialModel
    {
        public DateTime lastUse { get; set; }
        public int applicationId { get; set; }
        public DateTime creation { get; set; }
        public int credentialId { get; set; }
        public List<Rule> rules { get; set; }
        public DateTime expiration { get; set; }
        public string status { get; set; }
        public object allowedIPs { get; set; }
        public bool ovhSupport { get; set; }
        public class Rule
        {
            public string path { get; set; }
            public string method { get; set; }
        }
    }
}
