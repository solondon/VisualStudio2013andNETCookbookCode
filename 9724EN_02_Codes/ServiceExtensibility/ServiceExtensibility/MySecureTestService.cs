using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExtensibility
{
    [ServiceContract]
    public interface ISecureTestService
    {
        [OperationContract]
        string MyDummyOperation(string name, string password);
    }

    public class MySecureTestService : ISecureTestService
    {
        public string MyDummyOperation(string name, string password)
        {
            return "Doing inside secure service";
        }
    }
}
