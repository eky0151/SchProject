using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SchProject.Resources.Layout;
using SchProject.Resources.Layout.CustomControls;

namespace SchProject.ViewModel
{
    public class RootMenuViewModel : ViewModelBase
    {
        public List<MenuButtonData> MenuButtons { get; private set; }
        public UserControl CurrentView { get; private set; }
        public ICommand Navigation { get; private set; }

        private Dictionary<string, UserControl> Views { get; } = new Dictionary<string, UserControl>();

        public RootMenuViewModel()
        {
            MenuButtons = new List<MenuButtonData>()
            {
                new MenuButtonData(new BitmapImage(new Uri(@"C:\Users\dancs\Documents\GitRepos\SchProject\SchProject\Resources\Layout\Images\error.png")),
                    new BitmapImage(new Uri(@"C:\Users\dancs\Documents\GitRepos\SchProject\SchProject\Resources\Layout\Images\errorSelected.png")), "b", "HIBÁK")
            };

            Navigation = new RelayCommand<object>(param => Navigate(param));
        }

        private void Navigate(object o)
        {
            string d = (string)o;
            if (!Views.ContainsKey(d))
            {
                switch (d)
                {
                    case "Settings":
                        Views[d]=new Settings();break;
                    case "Bugreport":
                        Views[d]=new Bugreport();break;
                    case "Home":
                        Views[d]=new Dashboard();break;
                    case "Admins":
                        Views[d] = new Admins(); break;
                    default:
                        Views[d]=new Dashboard();break;
                }

            }
            CurrentView = Views[d];
            RaisePropertyChanged("CurrentView");
        }
    }
}
