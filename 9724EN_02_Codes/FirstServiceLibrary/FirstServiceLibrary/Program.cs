using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FirstServiceLibrary
{
    public class Program
    {
        public static void Main()
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            Uri serviceUri = new Uri("http://localhost:8000");
            ServiceHost host = new ServiceHost(typeof(IFirstWCF), serviceUri);
            host.AddServiceEndpoint(typeof(IFirstWCF), binding, "OperationService");


            host.Open();

            Console.WriteLine("Service is hosted to the Server");
            Console.ReadLine();

            host.Close();
        }
    }
}
