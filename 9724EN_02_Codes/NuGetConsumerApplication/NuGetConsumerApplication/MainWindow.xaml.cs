using NuGetConsumerApplication.NugetPackageOData;
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

namespace NuGetConsumerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Uri clientUri = null;
        public MainWindow()
        {
            InitializeComponent();
            clientUri = new Uri("http://packages.nuget.org/v1/FeedService.svc/");
        }
        IEnumerable<V1FeedPackage> QueryNuGetPackage(string query)
        {
            var client = new FeedContext_x0060_1(this.clientUri);
            var returnedPackages = client.Packages.Where(e => e.Title.ToUpper().Contains(query.ToUpper()));

            return returnedPackages;
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuery.Text))
                return;

            this.lstPackages.DataContext = this.QueryNuGetPackage(txtQuery.Text);
        }
    }
}
