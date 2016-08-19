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
using System.Windows.Shapes;

namespace SchProject.Resources.Layout.Aqua
{
    /// <summary>
    /// Interaction logic for MainWindowAqua.xaml
    /// </summary>
    public partial class MainWindowAqua : Window
    {
        public UserControl IO { get; set; }
        public MainWindowAqua()
        {
            InitializeComponent();
            IO=new Login();
            DataContext = this;
        }
    }
}
