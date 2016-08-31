using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace SchProject.ViewModel
{
    public class BugreportViewModel : ViewModelBase
    {
        public string Report { get; set; }
        public ObservableCollection<string> AttachedFiles { get; private set; }
        public ICommand AttachFile { get; private set; }
        public ICommand SendReport { get; private set; }
        public BugreportViewModel()
        {
            AttachedFiles = new ObservableCollection<string>();
            AttachFile = new RelayCommand(OpenDialog);
            SendReport = new RelayCommand(Send);
        }

        private void OpenDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                AttachedFiles.Add(filename);
            }
        }

        private void Send()
        {
            throw new NotImplementedException();
        }
    }
}
