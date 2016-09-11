using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using TechSharedLibraries;

namespace SchProject.ViewModel
{
    public enum Language
    {
        Magyar, English
    }
    public class SettingsViewModel : ViewModelBase
    {
        private string _profilePicture;
        public IEnumerable<Language> Languages { get; } = Enum.GetValues(typeof(Language)).Cast<Language>();
        public Language CurrentLanguage { get; set; }
        public ICommand ShowFileDialog { get; private set; }
        public ICommand SaveSettings { get; private set; }

        public SettingsViewModel()
        {
            ShowFileDialog = new RelayCommand(BrowseFile);
            SaveSettings = new RelayCommand<object>(Save);
        }

        private async void Save(object obj)
        {
            object[] elemets = obj as object[];
            if (elemets != null)
            {
                //TODO passCheck
                string newPass = (elemets[0] as PasswordBox)?.Password;
                string currentPass = (elemets[1] as PasswordBox)?.Password;
                if (currentPass == null || string.IsNullOrEmpty(currentPass)) return;
                var server = SimpleIoc.Default.GetInstance<TechSupportServer>().host;
                if (!string.IsNullOrEmpty(newPass) && newPass == currentPass && server.CheckMyPassWD(currentPass))
                {
                    await server.ChangeMyPassWDAsync(newPass);
                }
                if (!string.IsNullOrEmpty(ProfilePicture))
                {
                    await server.ChangeMyPictureAsync(await AzureBlobUploader.UploadImageAsync(ProfilePicture));
                }
                ProfilePicture = "";
            }
        }

        public string ProfilePicture
        {
            get { return _profilePicture; }
            private set { Set(ref _profilePicture, value); }
        }

        private void BrowseFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ProfilePicture = dlg.FileName;
            }
        }
    }
}
