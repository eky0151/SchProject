using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Azure;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ImageProcessor;
using SchProject.TechSupportSecure1;
using TechSharedLibraries;

namespace SchProject.ViewModel
{
    public class ManagementViewModel : ViewModelBase
    {
        private string _profilePicture;
        public string FullName { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; } 
        public ICommand BrowseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ManagementViewModel()
        {
            BrowseCommand = new RelayCommand(Browse);
            SaveCommand = new RelayCommand<PasswordBox>(Save);
        }
        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
        }

        private void Save(PasswordBox obj)
        {
            string uploaded = null;
            if (ProfilePicture != null)
                uploaded = AzureBlobUploader.UploadImage(ProfilePicture);
            using (TechSupportServiceSecure1Client client = new TechSupportServiceSecure1Client())
            {
                client.ClientCredentials.UserName.UserName = "Maright";
                client.ClientCredentials.UserName.Password = "tetris";
                client.Open();
                WorkerDataRegistrationData data = new WorkerDataRegistrationData() { Address = Address, Bank = Bank.Erste, BankAccount = BankAccount, Email = Email, FullName = FullName, PassWD = obj.Password, Phone = Phone, ProfilePicture = uploaded, Username = Username, Role = Role.HelpDesk, Status = Status.Away };
                client.RegisterNewStaffMember(data);
            }
        }

        private void Browse()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ProfilePicture = dlg.FileName;
            }
        }
    }
}
