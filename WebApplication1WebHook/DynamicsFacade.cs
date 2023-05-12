using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace WebApplication1WebHook
{
    public class DynamicsFacade
    {
        //User and pass for you login for BC365
        private string Login = $"admin:Password";

        //The IP for docker
        private string dockerIP = "172.25.161.237:7048";

        //The name of container, if it doesnt work, use dockerIP
        private string dockerContainer = "bc-container";

        public async Task CreateSalesOrder(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payloadinfo = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine(jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessCreateSalesOrder";

            HttpResponseMessage response = await client.PostAsync("http://"+ this.dockerIP + "/BC/ODataV4/"+ serviceName + "_"+ procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
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
        public async Task UpdateSalesOrder(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payloadinfo = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessUpdateOrder";

            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
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
        public async Task CreateCustomer(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payloadinfo = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooNewCustomer";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessWebhookPayload";

            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
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

    }

    class Payload
    {
        [JsonProperty("Payload")]
        public string payloadinfo { get; set; }
    }
}