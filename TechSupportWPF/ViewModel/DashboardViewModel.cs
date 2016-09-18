using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using SchProject.TechSupportSecure;


namespace SchProject.Resources.Layout.StyleResources
{
    public class TicketTemporary
    {
        public string ID { get; set; }
        public string Question { get; set; }
        public string Status { get; set; }

        public TicketTemporary(string id, string question, string status)
        {
            ID = id;
            Question = question;
            Status = status;
        }
    }

    public class DashboardViewModel : ViewModelBase
    {
        private int _newTickets;
        private int _solvedTickets;
        private int _openedTickets;
        private ObservableCollection<WorkerData> _workerList;

        public DashboardViewModel()
        {
            LastClients = new FixSizedObservableCollection<CustomerData>(9);
            LastTickets = new FixSizedObservableCollection<TicketTemporary>(7);
            SetData();
        }

        public FixSizedObservableCollection<CustomerData> LastClients { get; }
        public FixSizedObservableCollection<TicketTemporary> LastTickets { get; }
        public ObservableCollection<WorkerData> WorkerList
        {
            get { return _workerList; }
            private set { Set(ref _workerList, value); }
        }
        public int NewTickets
        {
            get { return _newTickets; }
            private set { Set(ref _newTickets, value); }
        }


        public int SolvedTickets
        {
            get { return _solvedTickets; }
            private set { Set(ref _solvedTickets, value); }
        }

        public int OpenedTickets
        {
            get { return _openedTickets; }
            private set { Set(ref _openedTickets, value); ; }
        }
        private async void SetData()
        {
            var data = await SimpleIoc.Default.GetInstance<TechSupportServer>().host.StaffListAsync();
            WorkerList = new ObservableCollection<WorkerData>(data);
            WorkerList.CollectionChanged += WorkerList_CollectionChanged;
            await Task.Factory.StartNew(
                () =>
                {
                    AzureServiceBus bus = SimpleIoc.Default.GetInstance<AzureServiceBus>();
                    bus.CustomerMessage += Bus_CustomerMessage;
                    bus.StatusHandler += WorkerListUpdate;
                    bus.CustomerLoginHandler += CustomerLogin_event;
                    NewTickets = bus.GetMessagesCount();
                });


        }


        private void Bus_CustomerMessage(object sender, ServiceBus.NewCustomerMessageEventArgs e)
        {
            LastTickets.Put(new TicketTemporary(e.ID, e.Message, "New"));
        }

        private void CustomerLogin_event(object sender, ServiceBus.CustomerLoginEventArgs e)
        {
            LastClients.Put(e.Customer);
        }

        private void WorkerList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("WorkerList");
        }

        private async void WorkerListUpdate(object sender, ServiceBus.StatusChangedEventArgs e)
        {
            var user = await Task.Factory.StartNew(() =>
             {
                 return WorkerList.Where(x => x.Username == e.Username).Select(x => x).FirstOrDefault();
             });

            if (user != null)
                user.Status = e.Status;
        }
    }
}
