using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TechSharedLibraries.LUIS_Classes
{
    public class LUIS
    {
        static HttpClient client = new HttpClient();
        private const string KEY = "c6aa404fd36d48a79dc7574f6de3ca5c";
        private const string APPID = "0fce5e81-0ea1-462a-abcf-9026ef3b40df";


        static LUIS()
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", KEY);
        }
        public static async Task<Intentsresult[]> ParseUserInput(string input)
        {
            string uri =
                $"https://api.projectoxford.ai/luis/v1.0/prog/apps/{APPID}/predict";
            HttpResponseMessage response;

            var data2 = input.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace("'", "");
            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes($@"[""{input.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace("'", "")}""]");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            var result = await response.Content.ReadAsStringAsync();
            Regex rg = new Regex(@"""IntentsResults"":(.*)],""Enti");
            var res = rg.Match(result).Groups[1].Value;
            var final = @" {""IntentsResults"":" + res + "]}";
            var data = JsonConvert.DeserializeObject<Rootobject>(final);
            return data.IntentsResults;

        }

        public static async Task UploadNewLabel(string question, string topic)
        {

            var uri =
                $"https://api.projectoxford.ai/luis/v1.0/prog/apps/{APPID}/example";

            HttpResponseMessage response;

            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UploadLabel() { ExampleText = question.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace("'", ""), SelectedIntentName = topic }));

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }

        public static async Task TrainModel()
        {

            var uri =
                $"https://api.projectoxford.ai/luis/v1.0/prog/apps/{APPID}/train";

            HttpResponseMessage response;

            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            response.EnsureSuccessStatusCode();
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        public static async Task UploadNewIntent(string userdata)
        {
            var uri =
                $"https://api.projectoxford.ai/luis/v1.0/prog/apps/{APPID}/intents?";

            HttpResponseMessage response;

            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UploadIntent() { Name = userdata }));

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }

    }
}