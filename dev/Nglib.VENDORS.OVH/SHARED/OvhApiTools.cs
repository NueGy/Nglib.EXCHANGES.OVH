using Nglib.VENDORS.OVH.SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nglib.VENDORS.OVH.SHARED
{
    public static class OvhApiTools
    {


        /// <summary>
        /// Sign OVH API REQUEST
        /// </summary>
        public static async Task SignRequestAsync(HttpRequestMessage request, IOvhApiCredentials credentials)
        {
            try
            {
                long currentTimestamp = OvhApiTools.Time(); //1608291240  //1608294594
                string requestdatastring = "";
                if (request.Content != null)
                    requestdatastring = await request.Content.ReadAsStringAsync();

                string target = request.RequestUri.ToString();
                if (!target.StartsWith("http")) { target = target.TrimStart('/'); target = credentials.Endpoint.ToString() + target; }
                string signature = GenerateSignature(credentials.ApplicationSecret, credentials.ConsumerKey, currentTimestamp, request.Method.ToString().ToUpper(), target, requestdatastring);


                //request.Headers.Add("X-UID", "");
                request.Headers.Add(SHARED.OvhConstants.OVH_APP_HEADER, credentials.ApplicationKey);
                request.Headers.Add(SHARED.OvhConstants.OVH_CONSUMER_HEADER, credentials.ConsumerKey);
                request.Headers.Add(SHARED.OvhConstants.OVH_TIME_HEADER, currentTimestamp.ToString());
                request.Headers.Add(SHARED.OvhConstants.OVH_SIGNATURE_HEADER, signature);
                // headers.Add(OVH_BATCH_HEADER, ParameterSeparator.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("OVH SignRequestAsync " + ex.Message, ex);
            }
        }


        /// <summary>
        /// Generates a signature for OVH's APi
        /// https://github.com/ovh/csharp-ovh/blob/master/csharp-ovh/Client/Client.cs
        /// </summary>
        private static string GenerateSignature(string applicationSecret, string consumerKey,
            long currentTimestamp, string method, string target, string data = null)
        {
            SHA1Managed sha1Hasher = new SHA1Managed();
            string toSign =
                string.Join("+", applicationSecret, consumerKey, method,
                target, data, currentTimestamp);
            byte[] binaryHash = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(toSign));
            string signature = string.Join("",
                binaryHash.Select(x => x.ToString("X2"))).ToLower();
            return $"$1${signature}";
        }


        public static HttpClient FactoryClient(IOvhApiCredentials credential)
        {
            HttpClient retour = new HttpClient() { BaseAddress = credential.Endpoint };
            return retour;
        }




        public static async Task<AUTH.CredentialResponseModel> OvhAuthRequestAsync(IOvhApiCredentials credential, AUTH.CredentialRequestModel credential1)
         => await new AUTH.AUTHWrapper(credential).OvhAuthRequestAsync(credential1);
        





        public static async Task<TResponseModel> ExecuteWithModelAsync<TResponseModel>(this HttpClient client, IOvhApiCredentials credential, HttpMethod method, string urlPart, Dictionary<string, object> formdata = null, object model = null)
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(method, urlPart);
                if (model != null) req.Content = OvhApiTools.JsonContent(model);
                await OvhApiTools.SignRequestAsync(req, credential);
                HttpResponseMessage resp = await client.SendAsync(req);
                OvhApiTools.Validate(resp);
                TResponseModel retour = await OvhApiTools.ReadWithModelAsync<TResponseModel>(resp);
                return retour;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static HttpRequestMessage PrepareRequest(HttpMethod method, string ServicePartUrl)
        {
            ServicePartUrl = ServicePartUrl.TrimStart('/');
            HttpRequestMessage req = new HttpRequestMessage(method, ServicePartUrl);
            //req.Headers.Accept.Clear();
            //req.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return req;
        }


        public static string StringLimit(string str, int sizemax) => (!string.IsNullOrEmpty(str) || str.Length < sizemax) ? str : str.Substring(0, sizemax);


        public static void Validate(this HttpResponseMessage resp)
        {
            if (resp == null) throw new Exception("HTTP ResponseMessage null");
            if (resp.IsSuccessStatusCode) return;
            string bodymsg = null;
            try
            {
                bodymsg = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidateHttpResponseMessage" + ex.Message);
            }
            bodymsg = StringLimit(bodymsg, 256);

            string resqEndUrl = null;
            if (resp.RequestMessage != null && resp.RequestMessage.RequestUri != null)
                resqEndUrl = $"{resp.RequestMessage.RequestUri.ToString()} [{resp.RequestMessage.Method}]";

            throw new Exception($"HTTP {resqEndUrl} ({((int)resp.StatusCode)}) {resp.ReasonPhrase} : {bodymsg}");
        }



        public static void SetBearerToken(this HttpRequestMessage httpRequestMessage, string token)
        {
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
        }



        //public static System.Net.Http.HttpContent PrepareJsonContent(object model)
        //{
        //    if (model == null) return null;
        //    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { IgnoreNullValues = true };
        //    string bodyjsoncontent = JsonSerializer.Serialize(model, model.GetType(), jsonSerializerOptions);
        //    return new System.Net.Http.StringContent(bodyjsoncontent, Encoding.UTF8, "application/json");
        //}



        //public static T ReadResponseContent<T>(HttpResponseMessage res)
        //{
        //    var txtdata = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //    if (string.IsNullOrWhiteSpace(txtdata)) return default;
        //    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        //    return JsonSerializer.Deserialize<T>(txtdata, jsonSerializerOptions);
        //}

        public static string ReadResponseText(HttpResponseMessage res)
        {
            var txtdata = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return txtdata;
        }
        public static string ReadResponseByte(HttpResponseMessage res)
        {
            var txtdata = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return txtdata;
        }

        public static async Task<TResponseModel> ReadWithModelAsync<TResponseModel>(this HttpResponseMessage resp)
        {
            try
            {
                if (resp.StatusCode == System.Net.HttpStatusCode.NoContent) return default(TResponseModel); // return null si vide
                string txtContent = await resp.Content.ReadAsStringAsync();

                Newtonsoft.Json.JsonSerializerSettings options = new Newtonsoft.Json.JsonSerializerSettings();
                TResponseModel retour = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponseModel>(txtContent, options);

                //System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions();
                //options.IgnoreNullValues = true;
                //options.PropertyNameCaseInsensitive = true;
                //TResponseModel retour = System.Text.Json.JsonSerializer.Deserialize<TResponseModel>(txtContent, options);
                return retour;
            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception($"ReadWithModelAsync({typeof(TResponseModel).Name}) {ex.Message}", ex);
            }
        }



        public static System.Net.Http.HttpContent JsonContent(object model)
        {
            if (model == null) return null;
            string bodyjsoncontent = null;
            //JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { IgnoreNullValues = true };
            //bodyjsoncontent=JsonSerializer.Serialize(model, model.GetType(), jsonSerializerOptions);

            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling= Newtonsoft.Json.NullValueHandling.Ignore };
            bodyjsoncontent = Newtonsoft.Json.JsonConvert.SerializeObject(model, jsonSerializerSettings);

            return new System.Net.Http.StringContent(bodyjsoncontent, Encoding.UTF8, "application/json");
        }


        public static long Time()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan span = DateTime.UtcNow.Subtract(unixEpoch);
            return (long)span.TotalSeconds;
        }


    }
}
