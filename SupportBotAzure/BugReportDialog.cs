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
        protected Findsimilarresult[] _answers;
        protected int _currentAnswer;
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
                ? $"Hi my name is Sarah, how can I help you?\nIf you want to call an Administrator type \"Call admin\", and follow the dialog.\nYou can speak to the {availableHelpDesk} available support member directly by typing \"Live\".\n You can start typing your question"
                : $"Hi my name is Sarah, how can I help you?\nIf you want to call an Administrator type \"Call admin\", and follow the dialog.\n You can start typing your question";

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
                SimilarQuestions res = await PostSimilarities(choice);
                if (res != null && res.FindSimilarResult.Length > 0)
                {
                    if (res.FindSimilarResult.Length > 1)
                    {
                        _answers = res.FindSimilarResult;
                        _currentAnswer = 0;
                        PromptDialog.Confirm(
                        context: context,
                        resume: MultipleAnswerFound,
                        prompt: $"{_currentAnswer + 1}/{_answers.Length}. The first answer i found is {_answers[_currentAnswer]}.",
                        retry: "I didn't understand. Please try again.");
                    }
                    PromptDialog.Text(
                        context: context,
                        resume: EndConversation,
                        prompt: $"I found this solution for you, is it helpful?\n{res.FindSimilarResult[0].Answer}",
                        retry: "I didn't understand. Please try again.");
                }
            }
        }

        private async Task MultipleAnswerFound(IDialogContext context, IAwaitable<bool> argument)
        {
            bool confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("You're welcome, it was my pleasure to help.");
                context.Done(context);
            }
            else
            {
                if (_currentAnswer >= _answers.Length)
                {
                    await
                        context.PostAsync(
                            "Sorry there is no more result to show. Please contact one of our employee at ....");
                    context.Done(context);
                }
                else
                {
                    _currentAnswer++;
                    PromptDialog.Confirm(
                        context: context,
                        resume: MultipleAnswerFound,
                        prompt: $"{_currentAnswer + 1}/{_answers.Length}. The next answer which if found is {_answers[_currentAnswer]}.",
                        retry: "I didn't understand. Please try again.");
                }
            }
        }
        private async Task ResumeAndPromptAdminInformations(IDialogContext context, IAwaitable<string> argument)
        {
            var location = await argument;
            var informaton = SendAdmin(location,"Teszt Elek");
            await
                context.PostAsync(
                    $"I've sent an Administrator. His/Her name is {informaton.Name}  you can reach his/her phone {informaton.Phone}  or email {informaton.Email}");
            context.Done(context);
        }

        private TechnicianData SendAdmin(string location,string fullname)
        {
            string resp;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/registertechwork?location={location}&fullname={fullname}";
                resp = http.GetStringAsync(uri).Result;
            }
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
            string resp;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/availabletechnician";
                resp = http.GetStringAsync(uri).Result;
            }
            return resp;
        }


        private async Task<SimilarQuestions> PostSimilarities(string input)
        {
            SimilarQuestions result;
            using (var http = new HttpClient())
            {
                string uri = $"http://techsupportserver.azurewebsites.net/techsupportbotservice.svc/findsimilar";
                var cuccs = JsonConvert.SerializeObject(new JsonQuestion() { question = input });
                byte[] byteData =
                    Encoding.UTF8.GetBytes(cuccs);
                string serverResponse;
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var dolog = await http.PostAsync(uri, content);
                    serverResponse = await dolog.Content.ReadAsStringAsync();
                }
                try
                {
                    result = JsonConvert.DeserializeObject<SimilarQuestions>(serverResponse);
                }
                catch (JsonSerializationException)
                {

                    throw;
                }
            }
            return result;
        }
        class JsonQuestion
        {
            public string question;
        }
    }
}