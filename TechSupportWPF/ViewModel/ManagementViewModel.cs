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
using System.Timers;

namespace SchProject.ViewModel
{
    public class ManagementViewModel : ViewModelBase
    {
        private Timer timer;

        private string _profilePicture;
        private string _bankAccount;
        private string _address;
        private string _phone;
        private string _email;
        private string _username;
        private string _fullName;
        private string _loginMessage;

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

            timer = new Timer();
            timer.Interval = 3000;
            timer.Elapsed += (s, e) => {
                                           LoginMessage = string.Empty;
                                           timer.Stop();
                                       };
        }

        public string FullName
        {
            get { return _fullName; }
            set { Set(ref _fullName, value); }
        }

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { Set(ref _phone, value); }
        }

        public string Address
        {
            get { return _address; }
            set { Set(ref _address, value); }
        }

        public string BankAccount
        {
            get { return _bankAccount; }
            set { Set(ref _bankAccount, value); }
        }


        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
        }

        public string LoginMessage
        {
            get { return _loginMessage; }
            set { Set(ref _loginMessage, value); }
        }

        private async void Save(PasswordBox obj)
        {
            //busyindicator
            bool isSucceed = await Task.Factory.StartNew<bool>(() =>
                    {
                        string file = "";
                        if (ProfilePicture != null && ProfilePicture != string.Empty) //the second time someone wanna save ProfilPicture is string.Empty not null
                            file = AzureBlobUploader.UploadImageAsync(ProfilePicture).Result;

                        string empty = string.Empty;
                        WorkerDataRegistrationData regdata;
                        if (_address != empty && _bankAccount != empty && _fullName != empty && obj.Password != empty && Username != empty)
                        {
                            regdata = new WorkerDataRegistrationData()
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
                                Status = Status.Away
                            };
                        }
                        else return false;

                
                        try
                        {
                            ServiceLocator.Current.GetInstance<TechSupportServer>().host.RegisterNewStaffMember(regdata);
                        }
                        catch (MessageSecurityException e)
                        {

                            //log
                        }

                        return true;

                });
            Address = "";
            BankAccount = "";
            Email = "";
            FullName = "";
            Phone = "";
            ProfilePicture = "";
            Username = "";
            //busyindicator

            LoginMessage = isSucceed ? "New worker saved" : "Something went wrong, some of the data is not valid";
            timer.Start();
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
