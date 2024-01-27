using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Infinity.Ers.IdentityServer.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SmsService'
    public class SmsService : IIdentityMessageService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SmsService'
    {
        //#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SmsService.SendAsync(IdentityMessage)'
        //        public Task SendAsync(IdentityMessage message)
        //#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SmsService.SendAsync(IdentityMessage)'
        //        {
        //            string smsBaisUrl = "https://api.mobireach.com.bd/SendTextMultiMessage?Username=infinittech&Password=Infinity%40123&From=MobiReach";
        //            using (HttpClient client = new HttpClient())
        //            {

        //                var url = smsBaisUrl + "&To=" + message.Destination + "&Message=" + message.Body;
        //                client.DefaultRequestHeaders.Accept.Clear();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                var response = client.GetAsync(url).Result;
        //                return Task.FromResult(response.StatusCode);
        //            }

        //        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SmsService.SendAsync(IdentityMessage)'
        public Task SendAsync(IdentityMessage msg)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SmsService.SendAsync(IdentityMessage)'
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                //http://apibd.rmlconnect.net/bulksms/bulksms?username=InfinityTBDENT&password=kk32yBRH&type=0&dlr=1&destination=8801750130101&source=Naval%20Sectt&message=Demo%20Message
                
                String number = msg.Destination; //Recipient Phone Number multiple number must be separated by comma
                String message = System.Uri.EscapeUriString(msg.Body); //do not use single quotation (') in the message to avoid forbidden result
                String url = @"http://apibd.rmlconnect.net/bulksms/bulksms?username=InfinityTBDENT&password=kk32yBRH&type=0&dlr=1&destination=88"+ number + "&source=Naval%20Sectt&message="+ message;
                request = WebRequest.Create(url);

                response = (HttpWebResponse)request.GetResponse();
                
                return Task.FromResult(response.StatusCode);
            }
            catch (Exception exp)
            {
                return Task.FromResult(response.StatusCode);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }
}