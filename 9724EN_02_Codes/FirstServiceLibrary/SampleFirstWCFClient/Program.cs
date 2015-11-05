using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SampleFirstWCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to call Server");
            string enteredString = Console.ReadLine();
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IFirstWCF> factory = new ChannelFactory<IFirstWCF>(binding,
                                                                        new EndpointAddress("http://localhost:8000/OperationService"));

            IFirstWCF proxy = factory.CreateChannel();
            var data = new Data { Message = enteredString };
            var methodFromServer = proxy.MySecondMethod(data);

            Console.WriteLine(methodFromServer.Message);

            Console.ReadLine();
        }

        [ServiceContract]
        public interface IFirstWCF
        {
            [OperationContract]
            string MyFirstMethod(string inputText);
            [OperationContract]
            Data MySecondMethod(Data inputText);
        }
        [DataContract]
        public class Data
        {
            [DataMember]
            public string Message { get; set; }
        }
    }
}
