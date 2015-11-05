using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FirstServiceLibrary
{
    [ServiceContract]
    public interface IFirstWCF
    {
        [OperationContract]
        string MyFirstMethod(string inputText);
    }
}
