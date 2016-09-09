using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SolvedQuestionInit.TechSupportService;

namespace SolvedQuestionInit.ViewModels
{
    public class MainPageViewModel:ViewModelBase
    {
        private TechSupportServiceCrossClient _host;
        private string _question;
        private string _answer;
        private string _newTopic;
        private string _keyPhrases;
        private List<WorkerData> _workers;
        private List<CustomerData> _customers;


        public MainPageViewModel()
        {
            _host=new TechSupportServiceCrossClient();
            Init();
        }

        private async Task Init()
        {
            var data = await _host.CustomerListAsync();
            _customers = data.ToList();
        }
        public string Question
        {
            get { return _question; }
            set { Set(ref _question, value); }
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
    }
}
