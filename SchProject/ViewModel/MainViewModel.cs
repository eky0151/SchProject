using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace SchProject.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Navigator Navigator { get; private set; }

        public MainViewModel()
        {
            Navigator = NavigatorFactory.Navigator;
            Messenger.Default.Send<Navigator>(Navigator);
        }
    }
}
