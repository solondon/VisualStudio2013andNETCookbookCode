using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CalendarSample.Resources;
using Microsoft.Phone.UserData;
using Microsoft.Phone.Tasks;

namespace CalendarSample
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

        private void SearchAppointment_Click(object sender, EventArgs e)
        {
            this.RefreshAppointments();
        }

        private void RefreshAppointments()
        {
            Appointments appointment = new Appointments();
            appointment.SearchCompleted += appointment_SearchCompleted;
            DateTime starttime = DateTime.Now;
            DateTime endtime = starttime.AddDays(10);

            int max = 50;

            appointment.SearchAsync(starttime, endtime, max, null);
        }

        void appointment_SearchCompleted(object sender, AppointmentsSearchEventArgs e)
        {
            try
            {
                lstAppointments.DataContext = e.Results;
            }
            catch 
            {
                MessageBox.Show("No appointments found !!");
            }
        }

        private void AddAppointment_Click(object sender, EventArgs e)
        {
            var saveAppontmentTask = new SaveAppointmentTask();

            saveAppontmentTask.StartTime = DateTime.Now.AddHours(1);
            saveAppontmentTask.EndTime = DateTime.Now.AddHours(2);
            saveAppontmentTask.Subject = "Sample Appointment Demo entry";
            saveAppontmentTask.Location = "www.packtpub.com";
            saveAppontmentTask.Details = "Sample save appointment entry for Windows Phone";
            saveAppontmentTask.IsAllDayEvent = false;
            saveAppontmentTask.Reminder = Reminder.FifteenMinutes;
            saveAppontmentTask.AppointmentStatus = AppointmentStatus.Busy;

            saveAppontmentTask.Show();
        }

    }
}