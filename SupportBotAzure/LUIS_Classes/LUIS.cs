using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace SupportBot.LUIS_Classes
{
    public class LUIS
    {
        public static async Task<RootObject> ParseUserInput(string input)
        {

            string escaped = Uri.EscapeDataString(input);
            using (var http = new HttpClient())
            {
                //hardcoding key is forbidden
                string key = "c88b6dde7ad945ce9476515863777ed8";
                string id = "e32d972d-5b13-4d2e-9eb9-3a6edb4e241b";
                string uri =
                    $"https://api.projectoxford.ai/luis/v1/application?id={id}&subscription-key={key}&q={escaped}";
                var resp = http.GetStringAsync(uri).Result;
                var data = JsonConvert.DeserializeObject<RootObject>(resp);
                return data;
            }
        }
    }
}