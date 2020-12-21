using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.SHARED
{
    public static class OvhConstants
    {
        public static string UrlApiBase = "https://api.ovh.com/1.0/";

        public const string OVH_APP_HEADER = "X-Ovh-Application";
        public const string OVH_CONSUMER_HEADER = "X-Ovh-Consumer";
        public const string OVH_TIME_HEADER = "X-Ovh-Timestamp";
        public const string OVH_SIGNATURE_HEADER = "X-Ovh-Signature";
        public const string OVH_BATCH_HEADER = "X-Ovh-Batch";
    }
}
