using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Practices.ServiceLocation;
using SchProject.TechSupportSecure;

namespace SchProject.ViewModel
{
    public class AdminsViewModel : ViewModelBase
    {

        private TechnicianData _selectedTechnician;
        private ObservableCollection<TechnicianData> _admins;

        public AdminsViewModel()
        {
            DownloadData();
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
            var downloaded =await Task.Factory.StartNew(() =>
            {
                return ServiceLocator.Current.GetInstance<TechSupportServer>().host.TechnicianList();
            });
            Admins=new ObservableCollection<TechnicianData>(downloaded);
            SelectedTechnician = Admins.FirstOrDefault();
        }


    }
}
