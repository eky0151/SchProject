using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SchProject.Resources.Layout.CustomControls;
using SchProject.TechSupportSecure1;

namespace SchProject
{
    public enum MenuButtonType
    {
        Error, Manager, Report
    }
    internal class MenuButtonFactory
    {

        private static MenuButtonData CreateButton(MenuButtonType s)
        {
            switch (s)
            {
                case MenuButtonType.Error:
                    return new MenuButtonData(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/error.png")), new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/errorSelected.png")), "Error", "HIBÁK");
                case MenuButtonType.Manager:
                    return new MenuButtonData(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/management.png")), new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/managementSelected.png")), "Management", "Kezelés");
                default: throw new ArgumentException();
            }
        }
        public static List<MenuButtonData> Create(Role role)
        {
            switch (role)
            {
                case Role.Admin:
                    return new List<MenuButtonData>() { CreateButton(MenuButtonType.Manager), CreateButton(MenuButtonType.Error) };
                case Role.Boss:
                    return new List<MenuButtonData>() { CreateButton(MenuButtonType.Manager), };
                case Role.HelpDesk:
                    return new List<MenuButtonData>() { CreateButton(MenuButtonType.Error), CreateButton(MenuButtonType.Manager) };
                case Role.Technician:
                    return new List<MenuButtonData>() { CreateButton(MenuButtonType.Manager) };
                default: throw new ArgumentException();
            }
        }
    }
}
