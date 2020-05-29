using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Dynamics_365_WebApp.DAL
{
    public class GetDynamicsContact
    {
        // Gets a single contact Entity using the contactid key (passed as id) from Dynamics 365. The returned attributes
        // are checked for the existance of a key value pair. If a pair doesn't exist then set the Contact model attribute to null.
        public Contact GetContact(IOrganizationService crmConnection, string id)
        {

            Contact contact = null; 

            try
            {
                var querycontact = new QueryExpression()
                {
                    EntityName = "contact",
                    ColumnSet = new ColumnSet(allColumns: true),
                    Criteria = new FilterExpression()
                };

                querycontact.Criteria.AddCondition("contactid", ConditionOperator.Equal, id);

                var dynamicsContactRecord = crmConnection.RetrieveMultiple(querycontact);

                foreach (var record in dynamicsContactRecord.Entities)
                {

                    contact = new Contact (record.Attributes.ContainsKey("firstname") ? record.Attributes["firstname"].ToString() : null,
                                           record.Attributes.ContainsKey("lastname") ? record.Attributes["lastname"].ToString() : null,
                                           record.Attributes.ContainsKey("jobtitle") ? record.Attributes["jobtitle"].ToString() : null,
                                           record.Attributes.ContainsKey("emailaddress1") ? record.Attributes["emailaddress1"].ToString() : null,
                                           record.Attributes.ContainsKey("telephone1") ? record.Attributes["telephone1"].ToString() : null,
                                           record.Attributes.ContainsKey("contactid") ? record.Attributes["contactid"].ToString() : null,
                                           record.Attributes.ContainsKey("new_chosenreference") ? record.Attributes["new_chosenreference"].ToString() : null);
                }
            }
            catch (Exception ex)
            {
                // If there is an exception then return a blank list
                contact = new Contact();  
            }

            return contact;
        }

    }
}