using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Cognitive.LUIS;
using SolvedQuestionInit.TechSupportService;
using SupportBot.LUIS_Classes;
using SupportBot.TextAnalitycs_Classes;

namespace SolvedQuestionInit.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _question;
        private string _answer;
        private string _newTopic;
        private string _keyPhrases;
        static Random rnd = new Random();
        public WorkerData _selectedWorker;
        private List<WorkerData> _workers;
        private Intentsresult[] _luisIntents;
        private List<CustomerData> _customers;
        public CustomerData _selectedCustomer;
        private TechSupportService1Client _host;

        public MainPageViewModel()
        {
            _host = new TechSupportService1Client();
            AddNewTopic = new RelayCommand(UploadNewIntent);
            SendSolved = new RelayCommand(Send);
            Init();
        }

        public Intentsresult SelectedTopic { get; set; }
        public ICommand AddNewTopic { get; private set; }
        public ICommand SendSolved { get; private set; }
        public string Question
        {
            get { return _question; }
            set
            {
                Set(ref _question, value);
                SetData();
            }
        }

        public string Answer
        {
            get { return _answer; }
            set { Set(ref _answer, value); }
        }

        public string NewTopic
        {
            get { return _newTopic; }
            set { Set(ref _newTopic, value); }
        }

        public string KeyPhrases
        {
            get { return _keyPhrases; }
            set { Set(ref _keyPhrases, value); }
        }

        public List<WorkerData> Workers
        {
            get { return _workers; }
            set { Set(ref _workers, value); }
        }
        public List<CustomerData> Customers
        {
            get { return _customers; }
            set { Set(ref _customers, value); }
        }

        public WorkerData SelectedWorker
        {
            get { return _selectedWorker; }
            set { Set(ref _selectedWorker, value); }
        }

        public CustomerData SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { Set(ref _selectedCustomer, value); }
        }

        public Intentsresult[] LuisIntents
        {
            get { return _luisIntents; }
            set { Set(ref _luisIntents, value); }
        }

        private async void SetData()
        {
            if (!String.IsNullOrEmpty(Question))
            {
                var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    SetTopic();
                    SetTextAnalitycsData();
                });
            }

        }
        //business logic should be separeted
        private async void Send()
        {
            await Task.Factory.StartNew(() =>
            {
                var keyWords = new ObservableCollection<string>();
                foreach (var item in _keyPhrases.Split(','))
                {
                    keyWords.Add(item);
                }
                _host.UploadSolvedQuestionAsync(new SolvedQuestion()
                {
                    Answer = Answer,
                    Category = "",
                    CustomerID = SelectedCustomer.ID,
                    KeyWords = keyWords,
                    Question = Question,
                    TimeAsked = DateTime.Now,
                    TimeAnswered = DateTime.Now.AddHours(rnd.Next(24)),
                    Topic = SelectedTopic.Name,
                    WorkerID = SelectedWorker.ID
                });
               
            });
            await LUIS.UploadNewLabel(Question, SelectedTopic.Name);
            await LUIS.TrainModel();
            Answer = "";
            Question = "";
            NewTopic = "";
            LuisIntents = null;
            KeyPhrases = "";
        }

        public async void UploadNewIntent()
        {
            await LUIS.UploadNewIntent(_newTopic);
            await LUIS.UploadNewLabel(Question.Replace("\"", ""), NewTopic);
            await LUIS.TrainModel();
            SetTopic();
        }
        private async Task Init()
        {
            var data = await _host.CustomerListAsync();
            var data2 = await _host.HelpDeskWorkerListAsync();
            Customers = data.ToList();
            Workers = data2.ToList();
            SelectedCustomer = Customers.FirstOrDefault();
            SelectedWorker = Workers.FirstOrDefault();
        }

        private async void SetTopic()
        {
            LuisIntents = await LUIS.ParseUserInput(Question);
        }

        private async void SetTextAnalitycsData()
        {
            var root = await TextAnalitycs.MakeRequests(Question);
            KeyPhrases = string.Join(",", root.keyPhrases);
        }


    }
}
