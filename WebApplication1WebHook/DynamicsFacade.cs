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
        private string dockerIP = "172.25.169.245:7048";

        public async Task CreateSalesOrder(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payload = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine("JsonData: "+jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine("StringContent: " + content);

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "FromWoo";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessCreateSalesOrder";

            HttpResponseMessage response = await client.PostAsync("http://"+ this.dockerIP + "/BC/ODataV4/"+ serviceName + "_"+ procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            System.Diagnostics.Debug.WriteLine("Response: " + response);
            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response: " + response);
                System.Diagnostics.Debug.WriteLine("Content: " + content);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error in DynamicsFacade");
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

            Payload payload = new Payload() { payload = jObject.ToString() };
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

            Payload payload = new Payload() { payload = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine("jsonData: "+jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine("StringContent: " + content);

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "FromWoo";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessCreateCustomer";
            //http://172.25.169.245:7048/BC/ODataV4/FromWoo_ProcessCreateCustomer?company=CRONUS%20Danmark%20A%2FS

            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            System.Diagnostics.Debug.WriteLine("Response: " + response);
            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response: "+response);
                System.Diagnostics.Debug.WriteLine("Content: " + content);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error in DynamicsFacade");
            }
            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }
        public async Task CreateItem(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payload = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "FromWoo";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessCreateItem";

            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response: " + response);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error in DynamicsFacade");
            }
            System.Diagnostics.Debug.WriteLine("Result: " + data);

        }
        public async Task UpdateItemStock(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Token to authenticate request, to be added to HTTP request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payload = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "WooIn";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessUpdateStock";

            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Response: " + response);
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
        [JsonProperty("payload")]
        public string payload { get; set; }
    }
}