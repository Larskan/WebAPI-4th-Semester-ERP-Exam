using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
           
            

            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            try
            {
                String topic = context.Request.Headers.GetValues("X-WC-Webhook-Topic").First();
                String eventType = context.Request.Headers.GetValues("x-wc-webhook-event").First();

                dynamic dData = data;
                string email = dData.email;
                string name = "TestName";


                if (topic.ToLower().Equals("customer.created"))
                {
                    //call Web Service Order created
                    DynamicsFacade dynamicsFacade = new DynamicsFacade();
                    //dynamicsFacade.CreateCustomer(name, email);
                    dynamicsFacade.InsertData(name, email);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }

            System.Diagnostics.Debug.WriteLine("Called1-----------------------");
            System.Diagnostics.Debug.WriteLine("test: "  + data.ToString());

            System.Diagnostics.Debug.WriteLine("Time: " + DateTime.Now.TimeOfDay.ToString());

            ////else if(/*  topic == customer created */)

            //System.Diagnostics.Debug.WriteLine("web hook topic is: " + topic);
            //System.Diagnostics.Debug.WriteLine("web hook eventType is: " + eventType);

            //if (context.Id == "i")
            //{
            //    //You can use the passed in Id to route differently depending on source.
            //}
            //else if (context.Id == "z")
            //{
            //}


            //string action = context.Actions.FirstOrDefault();

            return Task.FromResult(HttpStatusCode.OK);
        }
    }
}