using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SolvedQuestionInit.LUIS_Classes;

namespace SupportBot.LUIS_Classes
{
    public class LUIS
    {
        public static async Task<Intentsresult[]> ParseUserInput(string input)
        {
            
            using (var http = new HttpClient())
            {
                //hardcoding key is forbidden
                var client = new HttpClient();

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c6aa404fd36d48a79dc7574f6de3ca5c");
                string uri =
                    $"https://api.projectoxford.ai/luis/v1.0/prog/apps/0fce5e81-0ea1-462a-abcf-9026ef3b40df/predict";
                HttpResponseMessage response;

                var data2 = input.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace("'", "");
                // Request body
                byte[] byteData =
                    Encoding.UTF8.GetBytes($@"[""{input.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace("'","")}""]");

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
        }

        public static async Task UploadNewLabel(string question,string topic)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c6aa404fd36d48a79dc7574f6de3ca5c");

            var uri =
                "https://api.projectoxford.ai/luis/v1.0/prog/apps/0fce5e81-0ea1-462a-abcf-9026ef3b40df/example";

            HttpResponseMessage response;

            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UploadLabel() { ExampleText = question,SelectedIntentName = topic}));

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }

        public static async Task TrainModel()
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c6aa404fd36d48a79dc7574f6de3ca5c");

            var uri =
                "https://api.projectoxford.ai/luis/v1.0/prog/apps/0fce5e81-0ea1-462a-abcf-9026ef3b40df/train";

            HttpResponseMessage response;

            // Request body
            byte[] byteData =
                Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        public static async Task UploadNewIntent(string userdata)
        {

                var client = new HttpClient();

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c6aa404fd36d48a79dc7574f6de3ca5c");

                var uri =
                    "https://api.projectoxford.ai/luis/v1.0/prog/apps/0fce5e81-0ea1-462a-abcf-9026ef3b40df/intents?";

                HttpResponseMessage response;

                // Request body
                byte[] byteData =
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new UploadIntent() {Name = userdata}));

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.PostAsync(uri, content);
                }
        }

    }
}