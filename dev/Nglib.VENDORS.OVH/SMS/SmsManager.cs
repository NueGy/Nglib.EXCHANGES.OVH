using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nglib.VENDORS.OVH.SMS
{
    public class SmsManager
    {
        SMSWrapper wrapper { get; set; }

        public string defaultServiceName { get; set; }
        public string defaultSenderFrom { get; set; }


        public System.Collections.Concurrent.ConcurrentBag<SmsManagerSendContext> SmsCache = new System.Collections.Concurrent.ConcurrentBag<SmsManagerSendContext>();


        public SmsManager(SMSWrapper wrapper)
        {
            this.wrapper = wrapper;
        }
        public SmsManager(SHARED.IOvhApiCredentials credentials)
        {
            this.wrapper = new SMSWrapper(credentials);
        }


        /// <summary>
        /// Envoi d'un SMS
        /// </summary>
        public async Task<SmsManagerSendContext> SendSmsAsync(string mobileNumber, string message)
        {
            // Write SMS model
            SMS.SmsBatchParams smsparam = new SMS.SmsBatchParams();
            smsparam.To = new List<string>() { mobileNumber }.ToArray();
            smsparam.Message = message;
            smsparam.NoStop = true;
            smsparam.Deferred = null;
            return await SendSmsAsync(smsparam);
        }

        /// <summary>
        /// Envoi d'un SMS (avec param)
        /// </summary>
        public async Task<SmsManagerSendContext> SendSmsAsync(SmsBatchParams smsSendData)
        {
            if (smsSendData == null) throw new ArgumentNullException("smsSendData");
            try
            {
                SmsManagerSendContext ctx = new SmsManagerSendContext();
                if (string.IsNullOrEmpty(smsSendData.Message)) throw new ArgumentNullException("smsSendData.Message");
                if (smsSendData.To == null || smsSendData.To.Length == 0 || string.IsNullOrWhiteSpace(smsSendData.To[0])) throw new ArgumentNullException("smsSendData.To");
                if (string.IsNullOrWhiteSpace(this.defaultSenderFrom)) await this.InitManager();
                if (string.IsNullOrEmpty(smsSendData.From)) smsSendData.From = this.defaultSenderFrom;
                ctx.SendRequest = smsSendData;
                // validation mobilephone

                // Send
                ctx.SendResponse = await this.wrapper.SmsBatchesPut(this.defaultServiceName, smsSendData);
                ctx.SmsModel = ctx.SendResponse;


                // mise en cache pour suivis
                SmsCache.Add(ctx);

                return ctx;
            }
            catch (Exception ex)
            {
                throw new Exception("SendSms "+ex.Message,ex);
            }

        }




        public async Task InitManager()
        {
            try
            {
                // Service OVH non défini
                if (string.IsNullOrWhiteSpace(this.defaultServiceName))
                    this.defaultServiceName = (await this.wrapper.ServicesGet()).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(this.defaultServiceName)) throw new Exception("ServiceName empty, Please create SMS service, Go to OVH.com");

                // Sender ovh non défini
                if (string.IsNullOrWhiteSpace(this.defaultSenderFrom))
                    this.defaultSenderFrom = (await this.wrapper.SendersGet(this.defaultServiceName)).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(this.defaultSenderFrom)) this.defaultSenderFrom = await this.InitInstallFirstSender();

            }
            catch (Exception ex)
            {
                throw new Exception("InitManager "+ex.Message,ex);
            }
        }



        public async Task<string> InitInstallFirstSender()
        {
            if (string.IsNullOrWhiteSpace(this.defaultSenderFrom))
                throw new Exception("defaultSenderFrom empty, Please create sender service, Go to OVH.com");
            return null;
        }



        /// <summary>
        /// Sync API
        /// </summary>
        /// <returns></returns>
        public async Task SyncApi()
        {
      
        }



        public class SmsManagerSendContext
        {
            public DateTime RequestDate = DateTime.Now;

            public SmsBatchParams SendRequest { get; set; }
            public SmsBatch SendResponse { get; set; }

            public SmsBatch SmsModel { get; set; }



            

        }

    }
}
