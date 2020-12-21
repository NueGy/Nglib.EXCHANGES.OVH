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


namespace Nglib.VENDORS.OVH.SMS {
	
	
	/// <summary>
	/// OVH API WRAPPER
	/// </summary>
	public class SMSWrapper : IWrapper
    {
        public System.Net.Http.HttpClient client { get; set; }

        public IOvhApiCredentials Credentials { get; set; }

        public SMSWrapper(IOvhApiCredentials credentials, System.Net.Http.HttpClient client=null)
        {
            this.Credentials = credentials;
            if (client != null) this.client = client;
            else this.client = OvhApiTools.FactoryClient(this.Credentials);
        }


        public async Task SendSMS()
        {
            //DomainFullApiModel retour = await OvhApiTools.ExecuteWithModelAsync<DomainFullApiModel>(this.client, this.Credentials, HttpMethod.Get, $"domain/{domainName}");
            //retour.serviceInfo = await OvhApiTools.ExecuteWithModelAsync<ServicesService>(this.client, this.Credentials, HttpMethod.Get, $"domain/{domainName}/serviceInfos");
            //retour.zoneInfo = await OvhApiTools.ExecuteWithModelAsync<DomainzoneZone>(this.client, this.Credentials, HttpMethod.Get, $"domain/zone/{domainName}");
            //return retour;
        }

 

        /// <summary>
        /// [GET] "/sms"  string[]--
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> ServicesGet()
            => await OvhApiTools.ExecuteWithModelAsync<List<string>>(this.client, this.Credentials, HttpMethod.Get, $"sms", null, null);

        /// <summary>
        /// [GET] "/sms/{serviceName}"  sms.Account--serviceName
        /// </summary>
        /// <param name="servicename"></param>
        /// <returns></returns>
        public async Task<SMS.SmsAccount> ServiceGet(string serviceName)
            => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsAccount>(this.client, this.Credentials, HttpMethod.Get, $"sms/{serviceName}", null, null);

        /// <summary>
        /// [GET] "/sms/{serviceName}/batches"  sms.Batch[]--serviceName
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<SMS.SmsBatch[]> SmsBatchesGet(string serviceName)
                => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatch[]>(this.client, this.Credentials, HttpMethod.Get, $"sms/{serviceName}/batches", null, null);

        /// <summary>
        /// [POST] "/sms/{serviceName}/batches"  sms.Batch--serviceName,
        /// SEND SMS
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="smsBatchParams"></param>
        /// <returns></returns>
        public async Task<SMS.SmsBatch> SmsBatchesPut(string serviceName, SmsBatchParams smsBatchParams)
            => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatch>(this.client, this.Credentials, HttpMethod.Post, $"sms/{serviceName}/batches", null, smsBatchParams);


        /// <summary>
        /// [GET] "/sms/{serviceName}/batches/{id}"  sms.Batch--serviceName,id
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SMS.SmsBatch> SmsBatchGet(string serviceName, string id)
            => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatch>(this.client, this.Credentials, HttpMethod.Get, $"sms/{serviceName}/batches/{id}", null, null);


        /// <summary>
        /// [PUT] "/sms/{serviceName}/batches/{id}"  sms.Batch--serviceName,,id
        /// </summary>
        public async Task<SMS.SmsBatch> SmsBatchPut(string serviceName, string id, SmsBatchUpdateParams model)
            => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatch>(this.client, this.Credentials, HttpMethod.Put, $"sms/{serviceName}/batches/{id}", null, model);


        /// <summary>
        /// [POST] "/sms/{serviceName}/batches/{id}/cancel"  sms.Batch--serviceName,id
        /// </summary>
        public async Task<SMS.SmsBatch> SmsBatchCancel(string serviceName, string id)
             => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatch>(this.client, this.Credentials, HttpMethod.Post, $"sms/{serviceName}/batches/{id}/cancel", null, null);


        /// <summary>
        /// [GET] "/sms/{serviceName}/batches/{id}/statistics"  sms.BatchStatistics--serviceName,id
        /// </summary>
        public async Task<SMS.SmsBatchStatistics> SmsBatchStatistics(string serviceName, string id)
            => await OvhApiTools.ExecuteWithModelAsync<SMS.SmsBatchStatistics>(this.client, this.Credentials, HttpMethod.Get, $"sms/{serviceName}/batches/{id}/statistics", null, null);




        /// <summary>
        /// //[GET] "/sms/{serviceName}/senders"  string[]--serviceName
        /// </summary>
        public async Task<List<string>> SendersGet(string serviceName)
            => await OvhApiTools.ExecuteWithModelAsync<List<string>>(this.client, this.Credentials, HttpMethod.Get, $"sms/{serviceName}/senders", null, null);






    }
}