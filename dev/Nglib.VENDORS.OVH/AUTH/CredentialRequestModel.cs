using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.AUTH
{
    public class CredentialRequestModel
    {
        /// <summary>
        /// The actual rights asked, composed of a set of paths and authorized methods
        /// </summary>
        public List<AccessRight> accessRules { get; set; } = new List<AccessRight>();

        /// <summary>
        /// The URL on which to redirect the client when he confirms his credentials
        /// </summary>
        public string redirection { get; set; }


        public void GrantAll()
        {
            accessRules.Add(new AccessRight() { method = "GET", path = "/*" });
            accessRules.Add(new AccessRight() { method = "POST", path = "/*" });
            accessRules.Add(new AccessRight() { method = "PUT", path = "/*" });
            accessRules.Add(new AccessRight() { method = "DELETE", path = "/*" });
        }


        /// <summary>
        /// Class representing API Access rights composed of paths and methods
        /// </summary>
        public class AccessRight
        {


            /// <summary>
            /// HTTP Method to authorize
            /// </summary>

            public string method { get; set; }


            /// <summary>
            /// API resource to authorize access to
            /// </summary>

            public string path { get; set; }

        }
    }
}
