using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SchProject.Annotations;
using SchProject.Resources.Layout;
using SchProject.Resources.Views;

namespace SchProject
{
    public class ContentNavigator:INotifyPropertyChanged
    {
        public UserControl CurrentContent { get; private set; }
        private Dictionary<string, UserControl> _views { get; } = new Dictionary<string, UserControl>();

        public ContentNavigator()
        {
            Init();
        }
        public void NavigateTo(string controlName)
        {
            if (!_views.ContainsKey(controlName))
            {
                switch (controlName)
                {
                        
                    case "Error":
                        _views[controlName] = new Error();
                        break;
                    case "Admins":
                        _views[controlName] = new Admins();
                        break;
                    case "Management":
                        _views[controlName] = new Management();
                        break;
                    case "Report":
                        _views[controlName] = new Demonstration();
                        break;
                    case "Settings":
                        _views[controlName] = new Settings();
                        break;
                    case "Bugreport":
                        _views[controlName] = new Bugreport();
                        break;
                    default:
                        _views[controlName] = new Dashboard();
                        break;
                }
                

            }
            CurrentContent = _views[controlName];
            OnPropertyChanged(nameof(CurrentContent));
        }

        private void Init()
        {
            _views.Clear();
            _views["Home"] = new Dashboard();
            CurrentContent = _views["Home"];
        }
        public void Logout()
        {
            Init();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            temp?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
