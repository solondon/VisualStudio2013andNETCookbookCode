using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFCommunicationLibrary;

namespace ServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(FileTransferLibrary)))
            {
                host.Open();
                Console.WriteLine("Service Started!");

                foreach (Uri address in host.BaseAddresses)
                    Console.WriteLine("Listening on " + address);
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();

                host.Close();
            }
        }
    }
}
