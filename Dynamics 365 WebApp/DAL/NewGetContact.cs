using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Dynamics_365_WebApp.DAL
{
    public class NewGetContact
    {

        public Contact GetContact(IOrganizationService crmConnection, string id)
        {

            Contact updateContact = null;
            try
            {
                var serviceContext = new OrganizationServiceContext(crmConnection);

                var querycontact = new QueryExpression()
                {
                    EntityName = "contact",
                    ColumnSet = new ColumnSet(allColumns: true),
                    Criteria = new FilterExpression()
                };

                querycontact.Criteria.AddCondition("contactid", ConditionOperator.Equal, id);

                var contactRecords = crmConnection.RetrieveMultiple(querycontact);

                foreach (var a in contactRecords.Entities)
                {

                    updateContact = new Contact (a.Attributes.ContainsKey("firstname") ? a.Attributes["firstname"].ToString() : null,
                                                a.Attributes.ContainsKey("lastname") ? a.Attributes["lastname"].ToString() : null,
                                                a.Attributes.ContainsKey("jobtitle") ? a.Attributes["jobtitle"].ToString() : null,
                                                a.Attributes.ContainsKey("emailaddress1") ? a.Attributes["emailaddress1"].ToString() : null,
                                                a.Attributes.ContainsKey("telephone1") ? a.Attributes["telephone1"].ToString() : null,
                                                a.Attributes.ContainsKey("contactid") ? a.Attributes["contactid"].ToString() : null,
                                                a.Attributes.ContainsKey("new_chosenreference") ? a.Attributes["new_chosenreference"].ToString() : null);
                }
            }
            catch (Exception ex)
            {
                // If there is an exception then return a blank list
                updateContact = new Contact (null, null, null, null, null, null, null);
            }

            return (updateContact);
        }

    }
}