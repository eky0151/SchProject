using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using SchProject.TechSupportSecure;

namespace SchProject.Resources.Layout.StyleResources
{
    public enum TicketStatus
    {
        New,
        Solved,
        InProgress
    }

    public enum StaffStatus
    {
        Away,
        Available,
        Busy
    }

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

    public class ClientTemporary
    {
        public string FullName { get; set; }
        public ImageSource ProfilePicture { get; set; }

        public ClientTemporary(string fullName, ImageSource profilePicture)
        {
            FullName = fullName;
            ProfilePicture = profilePicture;
        }
    }

    public class DashboardViewModel : ViewModelBase
    {
        private ObservableCollection<WorkerData> _lastStaff;
        public int NewTickets { get; private set; } = 149;
        public int SolvedTickets { get; private set; } = 50;
        public int OpenedTickets { get; private set; } = 11;
        public ObservableCollection<ClientTemporary> LastClients { get; private set; }

        public ObservableCollection<WorkerData> LastStaff
        {
            get { return _lastStaff; }
            private set { Set(ref _lastStaff, value); }
        }

        public ObservableCollection<TicketTemporary> LastTickets { get; private set; }

        public DashboardViewModel()
        {
            LastClients = new ObservableCollection<ClientTemporary>();

            LastTickets = new ObservableCollection<TicketTemporary>()
            {
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"),
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"),
                new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New")
            };
            SetData();
        }

        private async Task SetData()
        {
            using (TechSupportServiceSecure1Client host = new TechSupportServiceSecure1Client())
            {
                host.ClientCredentials.UserName.UserName = "Flynn";
                host.ClientCredentials.UserName.Password = "sam";
                host.Open();
                var lop = await host.StaffListAsync();
                Dispatcher.CurrentDispatcher.Invoke(()=> { LastStaff = new ObservableCollection<WorkerData>(lop); });
            }
        }
    }
}
