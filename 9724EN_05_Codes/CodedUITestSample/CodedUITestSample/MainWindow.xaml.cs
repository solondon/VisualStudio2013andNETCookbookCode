using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CodedUITestSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void ProgressBarDelegate(DependencyProperty dp, Object value);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReverse_click(object sender, RoutedEventArgs e)
        {
            string reverse = this.ReverseString(this.txtInput.Text);
            
            string isPalindromeString = string.Empty;
            if (this.txtInput.Text.Equals(reverse))
                isPalindromeString += ", and it is a palindrome";
            var msg = string.Format("The reverse is : {0}{1}", reverse, isPalindromeString);

            this.tbReversedText.Text = msg;
            this.btnAgain.Visibility = System.Windows.Visibility.Visible;
        }

        private string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            this.Progress();
            return new string(charArray);
        }
        private void Progress()
        {
            this.pbProgress.Visibility = System.Windows.Visibility.Visible;
            double progress = 0;

            ProgressBarDelegate updatePbDelegate = new ProgressBarDelegate(pbProgress.SetValue);

            do
            {
                progress++;
                Dispatcher.Invoke(updatePbDelegate, DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, progress });
                pbProgress.Value = progress;
                Task.Delay(100);
            }
            while (pbProgress.Value != pbProgress.Maximum);
            this.pbProgress.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnAgain_Click_1(object sender, RoutedEventArgs e)
        {
            this.pbProgress.Visibility = System.Windows.Visibility.Collapsed;
            this.btnAgain.Visibility = System.Windows.Visibility.Collapsed;
            this.txtInput.Clear();
            this.tbReversedText.Text = "";
        }
    }
}
