using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dynamics_365_WebApp.BLL
{
    public class GetDynamicsContacts
    {
        public List<Contact> GetContactList(EntityCollection contactRecords)
        {
            var contactList = new List<Contact>();

            foreach (var record in contactRecords.Entities)
            {

                contactList.Add(new Contact(record.Attributes.ContainsKey("firstname") ? record.Attributes["firstname"].ToString() : null,
                                            record.Attributes.ContainsKey("lastname") ? record.Attributes["lastname"].ToString() : null,
                                            record.Attributes.ContainsKey("jobtitle") ? record.Attributes["jobtitle"].ToString() : null,
                                            record.Attributes.ContainsKey("emailaddress1") ? record.Attributes["emailaddress1"].ToString() : null,
                                            record.Attributes.ContainsKey("telephone1") ? record.Attributes["telephone1"].ToString() : null,
                                            record.Attributes.ContainsKey("contactid") ? record.Attributes["contactid"].ToString() : null,
                                            record.Attributes.ContainsKey("new_chosenreference") ? record.Attributes["new_chosenreference"].ToString() : null));
            }

            if (contactList.Count == 0) contactList.Add(null);

            return contactList;
        }
    }
}