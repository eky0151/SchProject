using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace SchProject.ViewModel
{
    public class AdminsViewModel : ViewModelBase
    {
        public ObservableCollection<string> Admins { get; set; }=new ObservableCollection<string>() {"soidfsdf","nisdofsmdof","nisdnfsd"};
    }
}
