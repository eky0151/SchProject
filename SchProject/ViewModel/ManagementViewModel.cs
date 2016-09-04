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
using SchProject.TechSupportService;
using ImageProcessor;

namespace SchProject.ViewModel
{
    public class ManagementViewModel : ViewModelBase
    {
        public string FullName { get; set; } = "Harry Geza";
        public string Username { get; set; } = "Maright";
        public string Email { get; set; } = "MichaelADeal@dayrep.com";
        public string Phone { get; set; } = "06304896357";
        public string Address { get; set; } = "3152 Nemti Apáczai Csere János 16";
        public string ProfilePicture { get; set; }
        public string BankAccount { get; set; } = "5332773384";
        public ICommand BrowseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ManagementViewModel()
        {
            BrowseCommand = new RelayCommand(Browse);
            SaveCommand = new RelayCommand<PasswordBox>(Save);
        }

        private void Save(PasswordBox obj)
        {
            string uploaded = null;
            if (ProfilePicture != null)
                uploaded = UploadImage();
            using (TechSupportService1Client client = new TechSupportService1Client())
            {
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

        private string UploadImage()
        {
            string fileName = Guid.NewGuid().ToString("N").Substring(0, 12);
            ImageFactory factory = new ImageFactory();
            factory.Load(ProfilePicture);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            CloudBlockBlob originalBlob = container.GetBlockBlobReference($"original/{fileName}.png");
            using (var fileStream = System.IO.File.OpenRead(ProfilePicture))
            {
                originalBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob mediumBlob = container.GetBlockBlobReference($"512/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(512, 512)).Save(fileStream);
                mediumBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob smallBlob = container.GetBlockBlobReference($"64/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(64, 64)).Save(fileStream);
                smallBlob.UploadFromStream(fileStream);
            }
            CloudBlockBlob extraSmallBlob = container.GetBlockBlobReference($"32/{fileName}.png");
            using (var fileStream = new MemoryStream())
            {
                factory.Resize(new Size(32, 32)).Save(fileStream);
                extraSmallBlob.UploadFromStream(fileStream);
            }
            return fileName + ".png";
        }



    }
}
