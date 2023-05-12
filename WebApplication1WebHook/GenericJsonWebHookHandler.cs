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

        //This task is to handle the webhook from woocommerce depending on what kind it is
        //And then launch the correct and matching task from dynamics
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();
            Console.WriteLine("json: " + data);

            try
            {
                String topic = context.Request.Headers.GetValues("X-WC-Webhook-Topic").First();
                String eventType = context.Request.Headers.GetValues("x-wc-webhook-event").First();

                //order.updated for testing
                if (topic.ToLower().Equals("order.updated"))
                {
                    //call Web Service Order created
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateCustomer(data); //change back to order update after testing
                    System.Diagnostics.Debug.WriteLine("Order Updated");
                }
                else if (topic.ToLower().Equals("order.created"))
                {
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    dynamicsFacade.CreateSalesOrder(data);
                    System.Diagnostics.Debug.WriteLine("Order Created");
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
                System.Diagnostics.Debug.WriteLine("Error");
            }

            System.Diagnostics.Debug.WriteLine("Time: " + DateTime.Now.TimeOfDay.ToString());
            return Task.FromResult(HttpStatusCode.OK);
        }
    }
}