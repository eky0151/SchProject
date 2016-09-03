using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Microsoft.Maps.MapControl.WPF;

namespace SchProject.ViewModel
{
    public class AdminTemporaryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BitmapImage ProfilePicture { get; set; }
        public bool Available { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AdditionalData { get; set; }
        public Location LastLocation { get; set; }

        public AdminTemporaryModel(int id, string name, BitmapImage profilePicture, bool avaible, string email, string phoneNumber, string additionalData, Location lastLocation)
        {
            ID = id;
            Name = name;
            ProfilePicture = profilePicture;
            Available = avaible;
            Email = email;
            PhoneNumber = phoneNumber;
            AdditionalData = additionalData;
            LastLocation = lastLocation;
        }

    }
    public class AdminsViewModel : ViewModelBase
    {
        public ObservableCollection<AdminTemporaryModel> Admins { get; set; } = new ObservableCollection<AdminTemporaryModel>()
        {
            new AdminTemporaryModel(1,"FSfadfas dfgsdfgs",new BitmapImage(new Uri(@"C:\Users\dancs\Documents\GitRepos\SchProject\SchProject\Resources\Layout\Images\demoPic.jpg")),true,"momfsdfm@dsfd.gz","0484848484","Ugyféltől jön",new Location(47.539519, 19.074154) ),
            new AdminTemporaryModel(1,"ASafsd sdfsdaf",new BitmapImage(new Uri(@"C:\Users\dancs\Documents\GitRepos\SchProject\SchProject\Resources\Layout\Images\demoPic.jpg")),true,"fasdfasd@dsfd.gz","334563456","U56345gyféltől jön",new Location(47.589519, 19.074154) ),
            new AdminTemporaryModel(1,"INMK jndsuiajnvs",new BitmapImage(new Uri(@"C:\Users\dancs\Documents\GitRepos\SchProject\SchProject\Resources\Layout\Images\demoPic.jpg")),false,"gasdg@dsfd.gz","345634","Ugyféltől jön",new Location(47.519519, 19.074154) )
        };

        private AdminTemporaryModel _selectedTechnician;

        public AdminTemporaryModel SelectedTechnician
        {
            get { return _selectedTechnician; }
            set { Set(ref _selectedTechnician, value); }
        }

        public AdminsViewModel()
        {
            SelectedTechnician = Admins.First();
        }

    }
}
