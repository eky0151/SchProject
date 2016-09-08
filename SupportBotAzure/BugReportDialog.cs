using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SupportBot.LUIS_Classes;
using SupportBot.TextAnalitycs_Classes;

namespace SupportBot
{
    [Serializable]
    public class BugReportDialog : IDialog<object>
    {
        //várom az adatbázis, RestApi tervet azure-ban lesz ez a project hostolva
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ConversationStartedAsync);
        }

        public async Task ConversationStartedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            PromptDialog.Text(
                context: context,
                resume: ResumeAndPromptChoicemAsync,
                prompt: $"Hi my name is AI,how can I help you?\nIf you want to call an Administrator type \"Call admin\", and follow the dialog.\nYou can speak to the {GetAvailableSupportCount()} available support member directly by typing \"Live\".",
                retry: "I didn't understand. Please try again.");

        }

        private async Task ResumeAndPromptChoicemAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var choice = await argument;
            if (choice == "Call admin")
            {

                PromptDialog.Text(
                    context: context,
                    resume: ResumeAndPromptAdminInformations,
                    prompt: $"There are {GetAvailableAdminCount()} available Sytem Administrator. Please send your location.",
                    retry: "I didn't understand. Please try again.");
            }
            else if (choice == "Live")
            {
                await context.PostAsync("Hold on!");
            }
            else
            {
                PromptDialog.Confirm(
                context: context,
                resume: ResumeAndHandleConfirmAsync,
                prompt: $"I found this solution for you, is it helpful?\n{SearchSolution(choice).Result}",
                retry: "I didn't understand. Please try again.");
            }
        }

        private async Task ResumeAndPromptAdminInformations(IDialogContext context, IAwaitable<string> argument)
        {
            var location = await argument;
            var informaton = SendAdmin(location);
            await
                context.PostAsync(
                    $"I've sent an Administrator. His/Her name is {informaton.Name}  you can reach his/her phone {informaton.Phone}  or email {informaton.Email}");
            context.Wait(ConversationStartedAsync);
        }

        private TechnicianData SendAdmin(string location)
        {
            return new TechnicianData() { Name = "Géza", Email = "sadokasdas@sr.re", Phone = "06304891378" };
        }

        private async Task ResumeAndHandleConfirmAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            bool isHelpful = await argument;
            if (isHelpful)
            {
                await context.PostAsync("You're welcome, it was my pleasure to help.");
            }
            else
            {
                await context.PostAsync("Sorry to hear that! I will send your message to the support. Hold on");
                //send hiba to support
                //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            }
        }

        private string GetAvailableSupportCount()
        {
            //logic goes here
            //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            return "1";
        }

        private string GetAvailableAdminCount()
        {
            //api hívás, várom aza adatbázis elkészültét ezért nincs implementálva
            return "3";
        }

        private async Task<string> SearchSolution(string bug)
        {
            //luis + database it takes some time
            //api hívás , várom aza adatbázis elkészültét ezért nincs implementálva
            var guess = await LUIS.ParseUserInput(bug);
            var keyPhrases = await TextAnalitycs.MakeRequests(bug);
            string kereso = guess.intents.FirstOrDefault()?.intent + keyPhrases.keyPhrases.FirstOrDefault();
            return kereso;
        }
    }
}