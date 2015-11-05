using FirstPhoneApp.Model.Base;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstPhoneApp.Model
{
    [DataContract]
    public class LoginDataContext : PropertyBase
    {
        private string userid;
        [DataMember]
        public string UserId
        {
            get { return userid; }
            set 
            { 
                userid = value;
                this.OnPropertyChanged("UserId");
            }
        }

        private string password;
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; this.OnPropertyChanged("Password"); }
        }
        public bool Status { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    this.Status = this.UserId == "Abhishek" && this.Password == "winphone";

                    if (this.Status)
                    {
                        var rootframe = App.Current.RootVisual as PhoneApplicationFrame;
                        rootframe.Navigate(new Uri(string.Format("/FirstPhoneApp;component/MainPage.xaml?name={0}", this.UserId), UriKind.Relative));
                    }
                });
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    this.UserId = this.Password = string.Empty;
                });
            }
        }
    }
}
