using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ContactService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ContactCRUD : IContactCRUD
    {
        private List<Contact> _store;
        internal List<Contact> Store
        {
            get
            {
                this._store = this._store ?? new List<Contact>();
                return this._store;
            }
        }

        #region ICRUDContact Members

        public Contact GetContact(string roll)
        {
            return this.Store.FirstOrDefault(item => item.Roll.ToString().Equals(roll));
        }

        public bool SaveContact(Contact currentContact)
        {
            if (this.Store.Any(item => item.Roll == currentContact.Roll))
                return false;
            this.Store.Add(currentContact);
            return true;
        }

        public bool RemoveContact(string roll)
        {
            var currentContact = this.Store.FirstOrDefault(item => item.Roll.ToString().Equals(roll));

            if (currentContact == null)
                return false;


            this.Store.Remove(currentContact);

            return true;
        }
        public bool UpdateContacts(Contact contact)
        {
            var currentContact = this.Store.FirstOrDefault(e => e.Roll == contact.Roll);
            if (currentContact != null)
            {
                currentContact.Name = contact.Name;
                currentContact.Age = contact.Age;
                currentContact.Address = contact.Address;
                return true;
            }
            return false;
        }
        public List<Contact> GetAllContacts()
        {
            return this.Store;
        }

        #endregion
    }
}
