using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using SchProject.Resources.Layout;
using SchProject.Resources.Layout.CustomControls;
using SchProject.TechSupportSecure1;

namespace SchProject.ViewModel
{
    public class RootMenuViewModel : ViewModelBase
    {
        public string _fullName;
        private Navigator _rootNavigator;
        private List<MenuButtonData> _menuButtons;
        public UserControl CurrentView { get; private set; }
        public ICommand Navigation { get; private set; }
        public ICommand Logout { get; private set; }
        private Dictionary<string, UserControl> Views { get; } = new Dictionary<string, UserControl>();

        public RootMenuViewModel()
        {
            Views["Dashboard"] = new Dashboard();
            CurrentView = Views["Dashboard"];
            Navigation = new RelayCommand<object>(param => Navigate(param));
            Logout = new RelayCommand(NavLogout);
            _rootNavigator = NavigatorFactory.Navigator;
            LoginSet(ServiceLocator.Current.GetInstance<UserData>());
        }
        public List<MenuButtonData> MenuButtons
        {
            get { return _menuButtons; }
            private set { Set(ref _menuButtons, value); }
        }


        public string FullName
        {
            get { return _fullName; }
            set { Set(ref _fullName, value); }
        }
        private void LoginSet(UserData res)
        {
            FullName = res.FullName;
            MenuButtons = MenuButtonFactory.Create(res.Role);
        }

        private void NavLogout()
        {
            Views.Clear();
            _rootNavigator.Logout();
        }
        private void Navigate(object o)
        {
            string d = (string)o;
            if (!Views.ContainsKey(d))
            {
                switch (d)
                {
                    case "Settings":
                        Views[d] = new Settings();
                        break;
                    case "Bugreport":
                        Views[d] = new Bugreport();
                        break;
                    case "Home":
                        Views[d] = new Dashboard();
                        break;
                    case "Admins":
                        Views[d] = new Admins();
                        break;
                    case "Management":
                        Views[d] = new Management();
                        break;
                    case "Error":
                        Views[d] = new Chat();
                        break;
                    default:
                        Views[d] = new Dashboard(); break;
                }

            }
            CurrentView = Views[d];
            RaisePropertyChanged("CurrentView");
        }
    }
}
