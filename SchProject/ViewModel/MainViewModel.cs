using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace SchProject.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Navigator Navigator { get; private set; }

        public MainViewModel()
        {
            Navigator = ServiceLocator.Current.GetInstance<NavigatorSingleton>().Navigator;
        }
    }
}
