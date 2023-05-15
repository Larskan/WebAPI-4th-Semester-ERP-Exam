using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebApplication1WebHook
{
    public class DynamicsFacade
    {
        //User and pass for your login for BC365
        private string Login = $"admin:Password";

        //The IP for docker
        private string dockerIP = "172.25.175.92:7048";

        public async Task CreateSalesOrder(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payload = jObject.ToString() };
            System.Diagnostics.Debug.WriteLine("Payload: " + jObject);
            String jsonData = JsonConvert.SerializeObject(payload);
            System.Diagnostics.Debug.WriteLine("Payload: "+ jsonData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine("Payload: " + content);

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "FromWoo"; 
            //Name of the procedure you wish to call based on your Service Name
            //
            var procedureName = "ProcessCreateSalesOrder"; 
            HttpResponseMessage response = await client.PostAsync("http://" + this.dockerIP + "/BC/ODataV4/"+ serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            
            string data = "";
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error in DynamicsFacade");
            }
            System.Diagnostics.Debug.WriteLine("Result: " + data);
        }
        public async Task CreateCustomer(JObject jObject)
        {
            var _token = Login;
            var _tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_token));

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _tokenBase64);

            Payload payload = new Payload() { payload = jObject.ToString() };
            String jsonData = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Name of the service attached to your Web Service(The name you gave your Codeunit in BC365 Web Service)
            var serviceName = "FromWoo";
            //Name of the procedure you wish to call based on your Service Name
            var procedureName = "ProcessCreateCustomer"; 
            HttpResponseMessage response = await client.PostAsync(
                "http://" + this.dockerIP + "/BC/ODataV4/" + serviceName + "_" + procedureName + "?company=CRONUS%20Danmark%20A%2FS", content);
            string data = "";

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error in DynamicsFacade");
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