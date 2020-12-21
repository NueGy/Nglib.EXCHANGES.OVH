using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH.DOMAIN
{
    public class DomainFullApiModel : DomainDomain
    {



        public ServicesService serviceInfo { get; set; }
        public DomainzoneZone zoneInfo { get; set; }

    }
}
