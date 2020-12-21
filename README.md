OVH APIs Wrapper C# .net standard Library
==========================

OVH APIs Wrapper

Create Tokens (AK/AS/CK) here : https://docs.ovh.com/gb/en/customer/first-steps-with-ovh-api/  
API Url : https://api.ovh.com/

Nglib.VENDORS.OVH

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/NueGy/NgLib/blob/master/Licence.md)




# Installation Nuget
```
Install-Package Nglib.VENDORS.OVH
```

# Examples Usages
Send SMS : 
```
var cred = new Nglib.VENDORS.OVH.SHARED.OvhApiCredentials()
{ ApplicationKey="Your-AK", ApplicationSecret="Your-AS", ConsumerKey="Your-CK" };
var smsManager = new Nglib.VENDORS.OVH.SMS.SmsManager(cred);
await smsManager.SendSmsAsync("+33600000000", "message test");
```
 
