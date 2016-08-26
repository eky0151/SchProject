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
    public class MenuTabItem : TabItem
    {
        public static readonly DependencyProperty DefaultPictureProperty = DependencyProperty.Register("DefaultPicture", typeof(ImageSource), typeof(MenuTabItem));
        public static readonly DependencyProperty SelectedPictureProperty = DependencyProperty.Register("SelectedtPicture", typeof(ImageSource), typeof(MenuTabItem));
        public static readonly DependencyProperty PictureSourceProperty = DependencyProperty.Register("PictureSource", typeof(ImageSource), typeof(MenuTabItem));
        public MenuTabItem()
        {

        }
        public ImageSource PictureSource
        {
            get { return (ImageSource)GetValue(PictureSourceProperty); }
            set { SetValue(PictureSourceProperty, value); }
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
    }
}
