/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SchProject"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SchProject.Resources.Layout.StyleResources;
using System.ServiceModel;

namespace SchProject.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RootMenuViewModel>();
            SimpleIoc.Default.Register<BugreportViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();

            //wcf
            //SimpleIoc.Default.Register<Chatservice.ChatClient>();
        }

        public DashboardViewModel Dashboard
        {
            get { return ServiceLocator.Current.GetInstance<DashboardViewModel>(); }
        }
        public SettingsViewModel Settings
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }
        public BugreportViewModel BugReport
        {
            get { return ServiceLocator.Current.GetInstance<BugreportViewModel>(); }
        }

        public RootMenuViewModel Menu
        {
            get { return ServiceLocator.Current.GetInstance<RootMenuViewModel>(); }
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public ChatViewModel Chat
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChatViewModel>();
            }
        }

      

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}