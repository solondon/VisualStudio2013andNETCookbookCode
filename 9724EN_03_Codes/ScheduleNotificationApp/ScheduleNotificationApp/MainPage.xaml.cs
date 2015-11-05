using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScheduleNotificationApp.Resources;
using Microsoft.Phone.Scheduler;

namespace ScheduleNotificationApp
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

        private void btnAddNotification_Click(object sender, EventArgs e)
        {
            if (this.rbAlarm.IsChecked.Value)
            {
                this.AddAlarm();
            }
            else if(this.rbReminder.IsChecked.Value)
            {
                this.AddReminder();
            }
        }

        private void AddReminder()
        {
            Reminder reminder = new Reminder(txtName.Text);
            reminder.Content = this.txtContent.Text;
            reminder.Title = this.txtTitle.Text;
            
            DateTime bdate = this.dtBegindate.Value.Value;
            DateTime beginDateTime = bdate + this.dtBegintime.Value.Value.TimeOfDay;
            reminder.BeginTime = beginDateTime;

            DateTime edate = this.dtExpdate.Value.Value;
            DateTime exDateTime = edate + this.dtExptime.Value.Value.TimeOfDay;
            reminder.ExpirationTime = exDateTime;

            reminder.RecurrenceType = RecurrenceInterval.Daily ;
            reminder.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);

            ScheduledActionService.Add(reminder);
        }

        private void AddAlarm()
        {
            Alarm alarm = new Alarm(txtName.Text);
            alarm.Content = this.txtContent.Text;

            DateTime bdate = this.dtBegindate.Value.Value;
            DateTime beginDateTime = bdate + this.dtBegintime.Value.Value.TimeOfDay;
            alarm.BeginTime = beginDateTime;

            DateTime edate = this.dtExpdate.Value.Value;
            DateTime exDateTime = edate + this.dtExptime.Value.Value.TimeOfDay;
            alarm.ExpirationTime = exDateTime;

            alarm.RecurrenceType = RecurrenceInterval.Daily;

            ScheduledActionService.Add(alarm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Current.Terminate();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            switch (radio.Content.ToString())
            {
                case "Alarm":
                    this.txtTitle.IsEnabled = false;
                    break;
                case "Reminder":
                    this.txtTitle.IsEnabled = true;
                    break;
            }
        }
    }
}