//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Newtonsoft.Json;
using Nglib.VENDORS.OVH.SHARED;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;


namespace Nglib.VENDORS.OVH.AUTH {
	
	
	/// <summary>
	/// OVH API WRAPPER
	/// </summary>
	public class AUTHWrapper : IWrapper
    {
        public System.Net.Http.HttpClient client { get; set; }

        public IOvhApiCredentials Credentials { get; set; }

        public AUTHWrapper(IOvhApiCredentials credentials, System.Net.Http.HttpClient client = null)
        {
            this.Credentials = credentials;
            if (client != null) this.client = client;
            else this.client = OvhApiTools.FactoryClient(this.Credentials);
        }




        public async Task<AUTH.CredentialResponseModel> OvhAuthRequestAsync(AUTH.CredentialRequestModel credential1)
        {
            try
            {
                var request = OvhApiTools.PrepareRequest(HttpMethod.Post, "auth/credential");
                request.Headers.Add(SHARED.OvhConstants.OVH_APP_HEADER, this.Credentials.ApplicationKey);
                request.Content = OvhApiTools.JsonContent(credential1);
                var response = await client.SendAsync(request);
                OvhApiTools.Validate(response);
                var modelresult = await OvhApiTools.ReadWithModelAsync<AUTH.CredentialResponseModel>(response);
                return modelresult;
            }
            catch (Exception ex)
            {
                throw new Exception("OvhAuthRequestAsync " + ex.Message, ex);
            }
        }

        public async Task<AUTH.AuthCurrentCredentialModel> OvhAuthInfoAsync()
        {
            try
            {
                var request = OvhApiTools.PrepareRequest(HttpMethod.Get, "auth/currentCredential");
                await OvhApiTools.SignRequestAsync(request, this.Credentials);
                var response = await client.SendAsync(request);
                OvhApiTools.Validate(response);
                var modelresult = await OvhApiTools.ReadWithModelAsync<AUTH.AuthCurrentCredentialModel>(response);
                return modelresult;
            }
            catch (Exception ex)
            {
                //throw new Exception("OvhAuthRequestAsync " + ex.Message, ex);
                return null;
            }
        }
    }
}