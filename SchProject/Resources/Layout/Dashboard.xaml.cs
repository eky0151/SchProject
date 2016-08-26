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

namespace SchProject.Resources.Layout
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public class Name
    {
        public string FullName { get; set; }
    }
    public partial class Dashboard : UserControl
    {
        public List<Name> Nevek { get; set; }
        public Dashboard()
        {
            InitializeComponent();
            Nevek=new List<Name>() {new Name() {FullName = "Peter Parker"}, new Name() { FullName = "asdasd asdasdv" } , new Name() { FullName = "Petvrvter ertvertv" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" }, new Name() { FullName = "ervtevrt sdvsdve" } };
            DataContext = this;
        }
    }
}
