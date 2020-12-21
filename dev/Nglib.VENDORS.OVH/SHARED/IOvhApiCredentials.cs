using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.SHARED
{
    public interface IOvhApiCredentials
    {
        Uri Endpoint { get; }

        /// <summary>
        /// name, Not required
        /// </summary>
        string ApplicationName { get;  }

        /// <summary>
        /// API application Key
        /// </summary>
        string ApplicationKey { get;  }

        /// <summary>
        /// API application secret
        /// </summary>
        string ApplicationSecret { get; }

        /// <summary>
        /// Consumer key 
        /// </summary>
        string ConsumerKey { get; set; }
    }
}
