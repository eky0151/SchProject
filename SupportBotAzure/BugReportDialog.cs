using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace SupportBot
{
    [Serializable]
    public class BugReportDialog : IDialog<object>
    {
        //várom az adatbázis, RestApi tervet azure-ban lesz ez a project hostolva
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(ConversationStartedAsync);
            return Task.CompletedTask;
        }

        public async Task ConversationStartedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            int availableHelpDesk = int.Parse(GetAvailableSupportCount());
            string sendmessage = availableHelpDesk > 0
                ? $"Hi my name is Sarah, how can I help you?\nIf you want to call an Administrator type \"Call admin\", and follow the dialog.\nYou can speak to the {availableHelpDesk} available support member directly by typing \"Live\".\n If you have a question please start typing"
                : $"Hi my name is Sarah, how can I help you?\nIf you want to call an Administrator type \"Call admin\", and follow the dialog.\n If you have a question please start typing";

            PromptDialog.Text(
                context: context,
                resume: ResumeAndPromptChoicemAsync,
                prompt: sendmessage,
                retry: "I didn't understand. Please try again.");


        }

        private async Task ResumeAndPromptChoicemAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var choice = await argument;
            if (choice == "Call admin")
            {
                int technicians = int.Parse(GetAvailableAdminCount());
                string message = technicians > 0
                    ? $"There are {technicians} available Sytem Administrator. Please send your location."
                    : "There is no available Technician at the moment, but you can send your location and we will send a worker";
                PromptDialog.Text(
                    context: context,
                    resume: ResumeAndPromptAdminInformations,
                    prompt: message,
                    retry: "I didn't understand. Please try again.");
            }
            else if (choice == "Live")
            {
                await context.PostAsync("Hold on!");
                context.Done(context);

            }
            else
            {
                Rootobject res = await PostSimilarities(choice);
                if (res != null && res.FindSimilarResult.Length > 0)
                {

                    PromptDialog.Text(
                        context: context,
                        resume: EndConversation,
                        prompt: $"I found this solution for you, is it helpful?\n{res.FindSimilarResult[0].Answer}",
                        retry: "I didn't understand. Please try again.");
                }
            }
        }

        private async Task ResumeAndPromptAdminInformations(IDialogContext context, IAwaitable<string> argument)
        {
            var location = await argument;
            var informaton = SendAdmin(location);
            await
                context.PostAsync(
                    $"I've sent an Administrator. His/Her name is {informaton.Name}  you can reach his/her phone {informaton.Phone}  or email {informaton.Email}");
            context.Done(context);
        }

        private TechnicianData SendAdmin(string location)
        {
            return new TechnicianData() { Name = "Géza", Email = "sadokasdas@sr.re", Phone = "06304891378" };
        }


        private async Task EndConversation(IDialogContext context, IAwaitable<string> argument)
        {
            string hup = await argument;
            await context.PostAsync("You're welcome, it was my pleasure to help.");
            context.Done(context);
        }

        private string GetAvailableSupportCount()
        {
            //logic goes here
            //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            string resp;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/availablehelpdesk";
                resp = http.GetStringAsync(uri).Result;
            }
            return resp;
        }

        private string GetAvailableAdminCount()
        {
            //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            string resp;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/availabletechnician";
                resp = http.GetStringAsync(uri).Result;
            }
            return resp;
        }

        class MyClass
        {
            public string question { get; set; }
        }

        private async Task<Rootobject> PostSimilarities(string input)
        {
            //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            string cucc;
            Rootobject result;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/findsimilar";
                var cuccs = JsonConvert.SerializeObject(new MyClass() { question = input });
                byte[] byteData =
                    Encoding.UTF8.GetBytes(cuccs);
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var dolog = await http.PostAsync(uri, content);
                    cucc = await dolog.Content.ReadAsStringAsync();
                }
                try
                {
                    result = JsonConvert.DeserializeObject<Rootobject>(cucc);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;
        }

        private async Task<string> SearchSolution(string bug)
        {
            //luis + database it takes some time
            //api hívás , várom aza adatbázis elkészültét ezért nincs implementálva
            return "dfdfgdf";
        }
    }
}