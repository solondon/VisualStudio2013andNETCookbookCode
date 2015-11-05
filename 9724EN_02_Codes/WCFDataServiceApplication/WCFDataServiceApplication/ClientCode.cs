using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFDataServiceApplication
{
    public class ClientCode
    {
        public void CallService()
        {
            //Uri serviceRootUri = new Uri(
            //                "https://localhost:8000/WCFDataServiceApplication/");
            //WCFDataServiceClient client =
            //    new WCFDataServiceClient(serviceRootUri);
            //client.SendingRequest += (o, requestEventArgs) =>
            //{
            //    var creds = username + ":" + password;
            //    var encodedCreds =
            //             Convert.ToBase64String(Encoding.ASCII.GetBytes(creds));
            //    requestEventArgs.RequestHeaders.Add(
            //             "Authorization", "Basic " + encodedCreds);
            //};
            //var res = client.Customers.FirstOrDefault();
        }
    }
}