using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using TechSharedLibraries;

namespace SchProject.ViewModel
{
    public class BugreportViewModel : ViewModelBase
    {
        private string _report;
        private bool _uploading;
        public ObservableCollection<string> AttachedFiles { get; private set; }
        public ICommand AttachFile { get; private set; }
        public ICommand SendReport { get; private set; }
        public BugreportViewModel()
        {
            Uploading = false;
            AttachedFiles = new ObservableCollection<string>();
            AttachFile = new RelayCommand(OpenDialog);
            SendReport = new RelayCommand(Send);
        }

        public bool Uploading
        {
            get { return _uploading; }
            private set { Set(ref _uploading, value); }
        }


        public string Report
        {
            get { return _report; }
            set { Set(ref _report, value); }
        }
        private void OpenDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                AttachedFiles.Add(filename);
            }
        }

        private async void Send()
        {
            Uploading = true;
            var host = ServiceLocator.Current.GetInstance<TechSupportServer>().host;
            string[] uploadedFiles=new string[0];
            if (AttachedFiles.Count > 0)
                uploadedFiles = await AzureBlobUploader.UploadFilesAsync(AttachedFiles.ToList());
            host.SendBugreport(Report, uploadedFiles);
            Report = "";
            AttachedFiles.Clear();
            Uploading = false;
        }
    }
}
