using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchProject.Resources.Layout.Aqua
{
    /// <summary>
    /// Interaction logic for RootMenu.xaml
    /// </summary>
    public partial class RootMenu : UserControl
    {
        public UserControl temp { get; set; }
        public RootMenu()
        {
            InitializeComponent();
            temp=new Dashboard();
            DataContext = this;
        }
    }
}
