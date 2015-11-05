using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FirstPhoneApp.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using FirstPhoneApp.Model;

namespace FirstPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        MainDataContext dataContext = new MainDataContext();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = dataContext;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string name = NavigationContext.QueryString["name"];

            this.txtApplicationDescription.Text = string.Format("Hello {0}, Welcome to your new app", name);
            base.OnNavigatedTo(e);
        }

        
       
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}