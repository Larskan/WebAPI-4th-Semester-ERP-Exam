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

        public async Task CreateCustomer(String name, String email)
        {
            var _token = $"admin:Password";
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Basic", "YWRtaW46UGFzc3dvcmQ=");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            //var parameter = new {  x = 10,  y = 20 };
            //String jsonData = JsonConvert.SerializeObject(parameter);            
            //String jsonData = "{\"x\": \"" + 50 +
            //                    "\", \"y\": \"" + 40 + "\" }";

            parameter p = new parameter { name = name, email = email };
            String jsonData = JsonConvert.SerializeObject(p);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            //HttpResponseMessage response = await client.PostAsync("http://172.19.16.193:7048/BC/ODataV4/Letter_Sum?company=CRONUS%20Danmark%20A%2FS", content);
            //HttpResponseMessage response = await client.PostAsync("http://172.19.25.212:7048/BC/ODataV4/Letter_Sum?Company('CRONUS%20Danmark%20A%2FS')", content);
            HttpResponseMessage response = await client.PostAsync("http://bc-container:7048/BC/ODataV4/wordpress_createcustomerws?company=CRONUS%20Danmark%20A%2FS", content);



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
            var _tokenBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            var parameter = new Customer { CustomerName = pname, CustomerLastName = pemail };
            String jsonData = JsonConvert.SerializeObject(parameter);

            var payload = new Payload { CustomerName = jsonData };
            var payloadJSON = JsonConvert.SerializeObject(payload);


            var inputData = new StringContent(payloadJSON, Encoding.Unicode, "application/json");

            System.Diagnostics.Debug.WriteLine("json: " + await inputData.ReadAsStringAsync());

            HttpResponseMessage response = await client.PostAsync("http://bc-container:7048/BC/ODataV4/WooNewCustomer_ProcessWebhookPayload?company=CRONUS%20Danmark%20A%2FS", inputData);

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