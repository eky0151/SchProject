using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace SchProject.Resources.Layout.StyleResources
{
    public enum TicketStatus
    {
        New, Solved, InProgress
    }

    public enum StaffStatus
    {
        Away, Available, Busy
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

    public class StaffStatusTemporary
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public StaffStatus Status { get; set; }

        public StaffStatusTemporary(string id, string fullName, string role, StaffStatus status)
        {
            ID = id;
            FullName = fullName;
            Role = role;
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
        public int NewTickets { get; private set; } = 149;
        public int SolvedTickets { get; private set; } = 50;
        public int OpenedTickets { get; private set; } = 11;
        public ObservableCollection<ClientTemporary> LastClients { get; private set; }
        public ObservableCollection<StaffStatusTemporary> LastStaff { get; private set; }
        public ObservableCollection<TicketTemporary> LastTickets { get; private set; }

        public DashboardViewModel()
        {
            LastClients = new ObservableCollection<ClientTemporary>();
            LastStaff = new ObservableCollection<StaffStatusTemporary>() { new StaffStatusTemporary("#123123123", "MIAmdsdo MIos", "lop", StaffStatus.Busy) };
            LastTickets = new ObservableCollection<TicketTemporary>() { new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"), new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New"), new TicketTemporary("#324342", "Msmaksasmomofm isodmfoskdfmkfdoms", "New") };
        }
    }
}
