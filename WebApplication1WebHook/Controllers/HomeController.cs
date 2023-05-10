using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1WebHook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public async Task<ActionResult> CreateCustomer(int customerNo, string customerName, string customerLastName, string customerMail)
        {
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerNo);
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerName);
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerLastName);
            System.Diagnostics.Debug.WriteLine("CreateCustomer call with: " + customerMail);

            // Change this one to your container name
            var nameOfContainer = "bc-container";
            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessWebhookPayload";
            Uri adress = new Uri(@"http://" + nameOfContainer + ":7048/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS");

            var ck = "ck_85a060bf066868da1c40742290aaf79986798d71";
            var cs = "cs_1c1aa151473eaaf6d085c48a0e30831abfd405cb";
            var user = "admin";
            var pass = "Password";

            var credentialsCache = new CredentialCache();
            credentialsCache.Add(adress, "NTLM", new NetworkCredential(user, pass));//Negotiate
            var handler = new HttpClientHandler() { Credentials = credentialsCache, PreAuthenticate = true };//, PreAuthenticate = true

            using (var httpClient = new HttpClient(handler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(2);
                var response = await httpClient.PostAsJsonAsync(adress, new Customer { CustomerID = customerNo, CustomerName = customerName, CustomerLastName = customerLastName, CustomerMail = customerMail });

                var result = await response.Content.ReadAsStringAsync();
                
                if ( response.IsSuccessStatusCode ) //error here
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call result: " + result);
                else
                    System.Diagnostics.Debug.WriteLine("CreateCustomer call error: " + response.StatusCode.ToString());
            }
            return RedirectToAction("Index");
        }
    }
    class Customer
    {
        public int CustomerID { get; set; }
        public String CustomerName { get; set; }
        public String CustomerLastName { get; set; }
        public String CustomerMail { get; set; }
    }
}
