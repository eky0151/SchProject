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
    public class MenuButtonData
    {
        public ImageSource DefaultPicture { get; set; }
        public ImageSource SelectedPicture { get; set; }
        public string UserControl { get; set; }
        public string Name { get; set; }
        public MenuButtonData(ImageSource defaultPicture, ImageSource selectedPicture, string navigateTo, string name)
        {
            DefaultPicture = defaultPicture;
            SelectedPicture = selectedPicture;
            UserControl = navigateTo;
            Name = name;
        }
    }


    class MenuButton : RadioButton
    {
        public static readonly DependencyProperty DefaultPictureProperty = DependencyProperty.Register("DefaultPicture", typeof(ImageSource), typeof(MenuButton));
        public static readonly DependencyProperty SelectedPictureProperty = DependencyProperty.Register("SelectedtPicture", typeof(ImageSource), typeof(MenuButton));
        public static readonly DependencyProperty PictureSourceProperty = DependencyProperty.Register("PictureSource", typeof(ImageSource), typeof(MenuButton));
        public static readonly DependencyProperty NavigateToProperty = DependencyProperty.Register("NavigateTo", typeof(string), typeof(MenuButton));
        public ImageSource PictureSource
        {
            get { return (ImageSource)GetValue(PictureSourceProperty); }
            set
            {
                SetValue(PictureSourceProperty, value);
            }
        }
        public string NavigateTo
        {
            get
            {
                return (string)GetValue(NavigateToProperty);
            }
            set
            {
                SetValue(NavigateToProperty, value);
            }
        }
        public ImageSource DefaultPicture
        {
            get { return (ImageSource)GetValue(DefaultPictureProperty); }
            set { SetValue(DefaultPictureProperty, value); }
        }
        public ImageSource SelectedtPicture
        {
            get { return (ImageSource)GetValue(SelectedPictureProperty); }
            set { SetValue(SelectedPictureProperty, value); }
        }

        public MenuButton()
        {
            this.Checked += MenuButton_Checked;
            this.Unchecked += MenuButton_Unchecked;
            this.Loaded += MenuButton_Loaded;
        }

        private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (PictureSource == null)
            {
                PictureSource = DefaultPicture;
            }
        }

        private void MenuButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.PictureSource = DefaultPicture;
        }

        private void MenuButton_Checked(object sender, RoutedEventArgs e)
        {
            this.PictureSource = SelectedtPicture;
        }

    }
}
