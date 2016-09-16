using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchProject.Resources.Layout.CustomControls
{
    public class AtachedProperties:TextBox
    {
        public static readonly DependencyProperty ValidUserProperty = DependencyProperty.RegisterAttached("ValidUser",typeof(bool), typeof(AtachedProperties),new UIPropertyMetadata(false));

        public static void SetValidUser(DependencyObject element, bool value)
        {
            element.SetValue(ValidUserProperty, value);
        }
        public static bool GetValidUser(DependencyObject element)
        {
            return (bool)element.GetValue(ValidUserProperty);
        }
    }
}
