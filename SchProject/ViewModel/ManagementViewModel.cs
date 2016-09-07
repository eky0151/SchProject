using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Azure;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ImageProcessor;
using Microsoft.Practices.ServiceLocation;
using SchProject.TechSupportSecure;
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
        public bool Technician { get; set; }
        public string[] Banks { get; } = Enum.GetNames(typeof(Bank));
        public string[] Roles { get; } = Enum.GetNames(typeof(Role));
        public string SelectedRole { get; set; }
        public string SelectedBank { get; set; }
        public ICommand BrowseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ManagementViewModel()
        {
            BrowseCommand = new RelayCommand(Browse);
            SaveCommand = new RelayCommand<PasswordBox>(Save);
            SelectedBank = Banks.FirstOrDefault();
            SelectedRole = Roles.FirstOrDefault();
        }

        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
        }

        private async void Save(PasswordBox obj)
        {
            //busyindicator
            await Task.Factory.StartNew(() =>
            {
                string file = "";
                if (ProfilePicture != null)
                    file = AzureBlobUploader.UploadImageAsync(ProfilePicture).Result;
                WorkerDataRegistrationData regdata = new WorkerDataRegistrationData()
                {
                    Address = Address,
                    Bank = (Bank)Enum.Parse(typeof(Bank), SelectedBank),
                    BankAccount = BankAccount,
                    Email = Email,
                    FullName = FullName,
                    PassWD = obj.Password,
                    Phone = Phone,
                    ProfilePicture = file,
                    Username = Username,
                    Role = (Role)Enum.Parse(typeof(Role), SelectedRole),
                    Technician = Technician,
                    Status = Status.Away
                };
                try
                {
                    ServiceLocator.Current.GetInstance<TechSupportServer>().host.RegisterNewStaffMember(regdata);
                }
                catch (MessageSecurityException e)
                {

                    //log
                }
            });
            //busyindicator
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
