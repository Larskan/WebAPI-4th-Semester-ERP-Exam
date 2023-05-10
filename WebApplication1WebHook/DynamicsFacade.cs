using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1WebHook
{
    public class DynamicsFacade
    {

        /// <summary>
        /// Sends HTTP POST request to create new customer with name and mail.
        /// This code is not correct, but is a starter code that can be leaned on
        /// </summary>
        /// <param name="name">CustomerName</param>
        /// <param name="email">CustomerMail</param>
        /// <returns></returns>
        public async Task CreateCustomer(String name, String email)
        {
            //This user and pass is your login for BC365
            var _token = $"admin:Password";
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            parameter p = new parameter { name = name, email = email };
            String jsonData = JsonConvert.SerializeObject(p);

            //JSON Object
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Change this one to your container name
            var nameOfContainer = "bc-container";
            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessWebhookPayload";

            HttpResponseMessage response = await client.PostAsync("http://"+ nameOfContainer + ":7048/BC/ODataV4/"+ serviceName + "_"+ procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);

            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }

            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }


        public async Task InsertData(String pname, String pemail)
        {
            //l/p
            var _token = $"admin:Password";
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            var parameter = new Customer { CustomerName = pname, CustomerLastName = pemail };
            String jsonData = JsonConvert.SerializeObject(parameter);

            var payload = new Payload { CustomerName = jsonData };
            var payloadJSON = JsonConvert.SerializeObject(payload);


            var content = new StringContent(payloadJSON, Encoding.Unicode, "application/json");

            System.Diagnostics.Debug.WriteLine("json: " + await content.ReadAsStringAsync());

            //Change this one to your container name
            var nameOfContainer = "bc-container";
            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessWebhookPayload";

            HttpResponseMessage response = await client.PostAsync("http://"+ nameOfContainer + ":7048/BC/ODataV4/"+ serviceName + "_"+ procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);

            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("Result: " + data);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error: " + response.ReasonPhrase);
            }
        }

    }

    class parameter
    {
        public string name { get; set; }
        public string email { get; set; }
    }



    class Customer
    {
        [JsonProperty("CustomerName")]
        public String CustomerName { get; set; }
        [JsonProperty("CustomerLastName")]
        public String CustomerLastName { get; set; }
    }

    class Payload
    {
        [JsonProperty("TubberwareCustomer")]
        public String CustomerName { get; set; }
    }
}