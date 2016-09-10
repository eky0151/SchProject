using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using SchProject.Resources.Layout;
using SchProject.Resources.Layout.CustomControls;
using SchProject.TechSupportSecure;

namespace SchProject.ViewModel
{
    public class RootMenuViewModel : ViewModelBase
    {
        public UserData User { get; private set; }
        private List<MenuButtonData> _menuButtons;
        public ContentNavigator CurrentView { get; private set; }
        public ICommand Navigation { get; private set; }
        public ICommand Logout { get; private set; }

        public RootMenuViewModel()
        {
            CurrentView = SimpleIoc.Default.GetInstance<ContentNavigator>();
            Navigation = new RelayCommand<string>(Navigate);
            Logout = new RelayCommand(NavLogout);
            User = ServiceLocator.Current.GetInstance<UserData>();
            MenuButtons = MenuButtonFactory.Create(User.Role);
        }

        public List<MenuButtonData> MenuButtons
        {
            get { return _menuButtons; }
            private set { Set(ref _menuButtons, value); }
        }

        private void NavLogout()
        {
            CurrentView.Logout();
            ServiceLocator.Current.GetInstance<NavigatorSingleton>().Navigator.Logout();
        }
        private void Navigate(string controlname)
        {
            CurrentView.NavigateTo(controlname);
        }
    }
}
