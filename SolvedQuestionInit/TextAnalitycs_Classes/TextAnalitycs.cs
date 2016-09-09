using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SupportBot.TextAnalitycs_Classes
{
    public class TextAnalitycs
    {
        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";

        // hardcoding API and account keys is very bad
        private const string AccountKey = "4770a7abf03a44009792c02311ccac1d";

        public static async Task<KeyPhrases> MakeRequests(string input)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                byte[] byteData = Encoding.UTF8.GetBytes(CreateJson(input));
                var uri = "text/analytics/v2.0/keyPhrases";
                var response = await CallEndpoint(client, uri, byteData);
                var vissza = JsonConvert.DeserializeObject<RootObject>(response);
                return vissza.documents.FirstOrDefault();
            }
        }
        private static async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(uri, content).Result;
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static string CreateJson(string input)
        {
            string json = @"{""documents"":[{""id"":""1"",""text"":" + '"' + input + '"' + @"}]}";
            return json;
        }
    }
}