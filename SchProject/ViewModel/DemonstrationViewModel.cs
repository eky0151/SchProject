using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ServiceModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SchProject.ViewModel
{
    public class TechAndWorksData : ObservableObject
    {
        private string techName;
        public string TechName
        {
            get { return techName; }
            set { Set(ref techName, value); }
        }

        private int works;

        public int Works
        {
            get { return works; }
            set { Set(ref works, value); }
        }
    }

    public class DemonstrationViewModel : ViewModelBase
    {
        public ObservableCollection<TechAndWorksData> data { get; private set; } =
            new ObservableCollection<TechAndWorksData>();





    }
}
