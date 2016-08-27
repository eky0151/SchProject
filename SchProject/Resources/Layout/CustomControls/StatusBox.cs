using System.Windows;
using System.Windows.Controls;

namespace SchProject.Resources.Layout.CustomControls
{
    public class StatusBox : Control
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(StatusBox));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(StatusBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
