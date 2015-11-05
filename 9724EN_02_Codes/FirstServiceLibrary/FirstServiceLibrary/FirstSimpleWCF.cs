using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FirstServiceLibrary
{
    public class FirstSimpleWCF : IFirstWCF
    {
        public string MyFirstMethod(string inputText)
        {
            Console.WriteLine("Message Received : {0}", inputText);
            Console.WriteLine(OperationContext.Current.RequestContext.RequestMessage.ToString());
            return string.Format("Message from server {0}", inputText);
        }
    }
}
