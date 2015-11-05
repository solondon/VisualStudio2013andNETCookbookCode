using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
