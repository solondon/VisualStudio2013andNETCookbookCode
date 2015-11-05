using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LauncherNChooserSample.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace LauncherNChooserSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask pcall = new PhoneCallTask();
            pcall.DisplayName = "Abhishek";
            pcall.PhoneNumber = "9999999999";
            pcall.Show();
        }

        private void btnSMS_Click(object sender, RoutedEventArgs e)
        {
            SmsComposeTask stask = new SmsComposeTask();
            stask.Body = "This is test sms";
            stask.To = "99999999";
            stask.Show();
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask etask = new EmailComposeTask();
            etask.Body = "This is test Email";
            etask.Subject = "This is test subject";
            etask.To = "contact@abhisheksur.com";
            etask.Cc = "abhi2434@gmail.com";
            etask.Bcc = "abhi2434@hotmail.com";
            etask.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchTask stask = new SearchTask();
            stask.SearchQuery = "abhishek";
            stask.Show();
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            ShareStatusTask stask = new ShareStatusTask();
            stask.Status = "This is a status sent from Windows Phone";
            stask.Show();
        }

        private void btnCapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask cTask = new CameraCaptureTask();
            cTask.Completed += (s, evt) =>
            {
                if (evt.Error == null && evt.TaskResult == TaskResult.OK)
                {
                    //BitmapImage bmpImage = new BitmapImage();
                    //bmpImage.SetSource(evt.ChosenPhoto);
                    //image.Source = bmpImage;   
                }
            };
            cTask.Show();
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask ptask = new PhotoChooserTask();
            ptask.ShowCamera = true;
            ptask.Completed += (s, evt) =>
            {
                if (evt.Error == null && evt.TaskResult == TaskResult.OK)
                {
                    //BitmapImage bmpImage = new BitmapImage();
                    //bmpImage.SetSource(evt.ChosenPhoto);
                    //image.Source = bmpImage;   
                }
            };
            ptask.Show();
        }

        private void btnPhone_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumberChooserTask task = new PhoneNumberChooserTask();
            task.Completed += (s, evt) =>
            {
                if (evt.Error == null && evt.TaskResult == TaskResult.OK)
                {
                    MessageBox.Show(evt.PhoneNumber + " phone number selected!");
                }
            };
            task.Show();
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            SaveContactTask ctask = new SaveContactTask();
            ctask.FirstName = "Abhishek";
            ctask.LastName = "Sur";
            ctask.Nickname = "Sunny";
            ctask.JobTitle = "Architect";
            ctask.PersonalEmail = "contact@abhisheksur.com";
            ctask.Completed += (s, evt) =>
            {
                if (evt.TaskResult == TaskResult.OK)
                    MessageBox.Show("Contact saved");
            };
            ctask.Show();
        }
        
        private void btnringtone_Click(object sender, RoutedEventArgs e)
        {
            SaveRingtoneTask ringtonetask = new SaveRingtoneTask();
            ringtonetask.Source = new Uri("appdata:/Assets/Ring-BlackIce.wma");
            ringtonetask.DisplayName = "My custom ringtone";
            ringtonetask.Completed += (s, evt) =>
            {
                if (evt.TaskResult == TaskResult.OK)
                    MessageBox.Show("Ringtone saved");
            };
            ringtonetask.Show();
        }

        private void btnsaveEmail_Click(object sender, RoutedEventArgs e)
        {
            SaveEmailAddressTask eaddr = new SaveEmailAddressTask();
            eaddr.Email = "contact@abhisheksur.com";
            eaddr.Completed += (s, evt) =>
            {
                if (evt.TaskResult == TaskResult.OK)
                    MessageBox.Show("Email address saved");
            };
            eaddr.Show();
        }

        private void btnLink_Click(object sender, RoutedEventArgs e)
        {
            ShareLinkTask sLink = new ShareLinkTask();
            sLink.LinkUri = new Uri("http://www.abhisheksur.com");
            sLink.Message = "See my website";
            sLink.Title = "Abhisheks new Website";
            sLink.Show();
        }
    }
}