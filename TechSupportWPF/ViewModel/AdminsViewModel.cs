using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Practices.ServiceLocation;
using SchProject.TechSupportSecure;

namespace SchProject.ViewModel
{
    public class AdminsViewModel : ViewModelBase
    {

        private TechnicianData _selectedTechnician;
        private ObservableCollection<TechnicianData> _admins;
        private string _message;
        public ICommand SendMessage { get; private set; }

        public AdminsViewModel()
        {
            DownloadData();
            SendMessage=new RelayCommand(SendToTechnician);
        }
        public string Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }

        public ObservableCollection<TechnicianData> Admins
        {
            get { return _admins; }
            private set { Set(ref _admins, value); }
        }
        public TechnicianData SelectedTechnician
        {
            get { return _selectedTechnician; }
            set { Set(ref _selectedTechnician, value); }
        }

        private async void DownloadData()
        {
            var downloaded = await Task.Factory.StartNew(() =>
             {
                 return ServiceLocator.Current.GetInstance<TechSupportServer>().host.TechnicianList();
             });
            Admins = new ObservableCollection<TechnicianData>(downloaded);
            Admins.CollectionChanged += Admins_CollectionChanged;
            SimpleIoc.Default.GetInstance<AzureServiceBus>().StatusHandler += AdminsViewModel_StatusHandler;
            SelectedTechnician = Admins.FirstOrDefault();
        }

        private void Admins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("Admins");
        }

        private async void AdminsViewModel_StatusHandler(object sender, ServiceBus.StatusChangedEventArgs e)
        {
            var admin = await Task.Factory.StartNew(() =>
            {
                return _admins.FirstOrDefault(y => y.Username == e.Username);
            });
            if (admin != null)
            {
                admin.Status = e.Status;
            }
        }

        private async void SendToTechnician()
        {
            await SimpleIoc.Default.GetInstance<TechSupportServer>()
                .host.SendMessageToTechnicianAsync(SelectedTechnician.Username, Message);
            Message = "";
        }
    }

}
