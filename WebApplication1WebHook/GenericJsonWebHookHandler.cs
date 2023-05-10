using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            System.Diagnostics.Debug.WriteLine("Called1-----------------------");
            

            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            System.Diagnostics.Debug.WriteLine("test: "  + data.ToString());

            System.Diagnostics.Debug.WriteLine("Called2-----------------------");

            //if (context.Id == "i")
            //{
            //    //You can use the passed in Id to route differently depending on source.
            //}
            //else if (context.Id == "z")
            //{
            //}

            string action = context.Actions.FirstOrDefault();

            return Task.FromResult(true);
        }
    }
}