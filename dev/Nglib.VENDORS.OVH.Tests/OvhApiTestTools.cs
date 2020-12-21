using System;
using System.Collections.Generic;
using System.Text;

namespace Nglib.VENDORS.OVH
{
    public static class OvhApiTestTools
    {


        public static SHARED.IOvhApiCredentials GetApiCredentials() 
        {
            string json = System.IO.File.ReadAllText(@"C:\TEST\SECUR\OvhApiCredentials.json"); // password-tokens in external file
            SHARED.OvhApiCredentials retour = System.Text.Json.JsonSerializer.Deserialize<SHARED.OvhApiCredentials>(json);


            //string json = System.Text.Json.JsonSerializer.Serialize(retour);
            //System.IO.File.WriteAllText(@"C:\TEST\SECUR\OvhApiCredentials.json", json);
            return retour;
        }




    }
}
