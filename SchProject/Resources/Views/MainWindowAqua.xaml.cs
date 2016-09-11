using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using GalaSoft.MvvmLight.Ioc;
using SchProject.TechSupportSecure;

namespace SchProject.Resources.Layout
{
    /// <summary>
    /// Interaction logic for MainWindowAqua.xaml
    /// </summary>
    public partial class MainWindowAqua : Window
    {
       
        public MainWindowAqua()
        {
            InitializeComponent(); 
        }

        private void MainWindowAqua_OnClosing(object sender, CancelEventArgs e)
        {
             SimpleIoc.Default.GetInstance<TechSupportServer>().host?.ChangeMyStatus(Status.Away);
        }
    }
}
