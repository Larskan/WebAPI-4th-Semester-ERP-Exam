using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1WebHook
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }

        /// <summary>
        /// This task is to handle the webhook from woocommerce depending on what kind it is
        /// And then launch the correct and matching task from dynamics
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="context">Context of Webhook</param>
        /// <returns></returns>
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            try
            {
                String topic = context.Request.Headers.GetValues("X-WC-Webhook-Topic").First();
                String eventType = context.Request.Headers.GetValues("x-wc-webhook-event").First();

                if (topic.ToLower().Equals("order.created"))
                {
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateSalesOrder(data); 
                    System.Diagnostics.Debug.WriteLine("Sales Order Created");
                }
                else if (topic.ToLower().Equals("customer.created"))
                {
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateCustomer(data);
                    System.Diagnostics.Debug.WriteLine("Customer Created");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in webhook");
            }
            System.Diagnostics.Debug.WriteLine("Time: " + DateTime.Now.TimeOfDay.ToString());
            return Task.FromResult(HttpStatusCode.OK);
        }
    }
}