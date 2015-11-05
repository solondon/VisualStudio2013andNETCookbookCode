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
using FirstPhoneApp.Model;

namespace FirstPhoneApp
{
    public partial class Login : PhoneApplicationPage
    {
        LoginDataContext logindataContext = new LoginDataContext();
        bool _isNewPageInstance = false;
        public Login()
        {
            InitializeComponent();
            _isNewPageInstance = true;

            this.BuildLocalizedApplicationBar();
            this.DataContext = this.logindataContext; 
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            var appbar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarLoginButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/next.png", UriKind.Relative));
            appBarLoginButton.Text = AppResources.LoginText;
            appBarLoginButton.Click += appBarLoginButton_Click;
            appbar.Buttons.Add(appBarLoginButton);

            ApplicationBarIconButton appBarClearButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/delete.png", UriKind.Relative));
            appBarClearButton.Text = AppResources.ClearText;
            appBarClearButton.Click += appBarClearButton_Click;
            appbar.Buttons.Add(appBarClearButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AboutText);
            appBarMenuItem.Click += delegate(object sender, EventArgs e)
            {
                MessageBox.Show("This is my first MVVM style Phone App");
            };
            appbar.MenuItems.Add(appBarMenuItem);

            this.ApplicationBar = appbar;
        }

        void appBarClearButton_Click(object sender, EventArgs e)
        {
            this.logindataContext.ClearCommand.Execute(sender); 

            //Need to clear password from here
            this.txtPassword.Password = string.Empty;
        }

        void appBarLoginButton_Click(object sender, EventArgs e)
        {
            //Need to assign password as it is a special case
            this.logindataContext.Password = this.txtPassword.Password;
            this.logindataContext.LoginCommand.Execute(sender);
            //if(this.logindataContext.Status)
            //{
            //    this.NavigationService.Navigate(new Uri(string.Format("/FirstPhoneApp;component/MainPage.xaml?name={0}", this.logindataContext.UserId), UriKind.Relative));
            //}
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.logindataContext.Password = txtPassword.Password;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                // Save the ViewModel variable in the page's State dictionary.
                State["ViewModel"] = logindataContext;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_isNewPageInstance)
            {
                if (this.logindataContext == null)
                {
                    if (State.Count > 0)
                    {
                        this.logindataContext = (LoginDataContext)State["ViewModel"];
                    }
                    else
                    {
                        this.logindataContext = new LoginDataContext();
                    }
                }
                DataContext = this.logindataContext;
            }
            _isNewPageInstance = false;
        }
    }
}