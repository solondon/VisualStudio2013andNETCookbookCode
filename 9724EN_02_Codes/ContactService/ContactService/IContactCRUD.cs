using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ContactService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IContactCRUD" in both code and config file together.
    [ServiceContract]
    public interface IContactCRUD
    {
        [WebGet(UriTemplate = "contact/{roll}")]
        [OperationContract]
        Contact GetContact(string roll);

        [WebInvoke(Method = "POST", UriTemplate = "contacts")]
        [OperationContract]
        bool SaveContact(Contact currentContact);

        [WebInvoke(Method = "DELETE", UriTemplate = "contact/{roll}")]
        [OperationContract]
        bool RemoveContact(string roll);

        [WebInvoke(Method = "PUT", UriTemplate = "contact")]
        [OperationContract]
        bool UpdateContacts(Contact contact);

        [WebGet(UriTemplate = "contacts")]
        [OperationContract]
        List<Contact> GetAllContacts();
    }

    [DataContract)]
    public class Contact
    {
        [DataMember]
        public int Roll { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Age { get; set; }
    } 
}
