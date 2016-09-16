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
        private ObservableCollection<WorkerData> _workerList;
        public int NewTickets { get; private set; } = 149;
        public int SolvedTickets { get; private set; } = 50;
        public int OpenedTickets { get; private set; } = 11;
        public FixSizedObservableCollection<CustomerData> LastClients { get; private set; }

        public ObservableCollection<WorkerData> WorkerList
        {
            get { return _workerList; }
            private set { Set(ref _workerList, value); }
        }

        public ObservableCollection<TicketTemporary> LastTickets { get; private set; }

        public DashboardViewModel()
        {
            LastClients = new FixSizedObservableCollection<CustomerData>(9);

            LastTickets = new ObservableCollection<TicketTemporary>()
            {
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"),
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"),
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New")
            };
            SetData();
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
                    bus.StatusHandler += WorkerListUpdate;
                    bus.CustomerLoginHandler += CustomerLogin_event;
                });


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
