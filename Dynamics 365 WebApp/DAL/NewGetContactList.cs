using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamics_365_WebApp.DAL
{
    public class NewGetContactList
    {
        // Gets a list of contacts from the contact Entity using the search value and option data. The returned data 
        // are checked for the existance of a key value pair. If a pair doesn't exist then set the Contact model attribute to null.
        // The search option is set based in the radio buttion selection on the index form.
        // The returned data is filtered into a paginated list.

        public (List<Contact>, int?, bool, bool) GetListOfContacts(IOrganizationService crmConnection, string searchValue, string searchOption, int? currentPageNumber)
        {
            
            var contactList = new List<Contact>();
            var paginatedContactList = new List<Contact>();
            var pageSize = 12;
            var PageNumber = (currentPageNumber == null) ? 0 : currentPageNumber;
            var hasPreviouPage = false;
            var hasNextPage = false;

            try
            {
                var serviceContext = new OrganizationServiceContext(crmConnection);

                var querycontact = new QueryExpression()
                {
                    EntityName = "contact",
                    ColumnSet = new ColumnSet(allColumns: true),
                    Criteria = new FilterExpression()
                };
                
                // Select search criteria based on the radio button selection.
                switch (searchOption)
                {
                    case "First name": querycontact.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                        break;

                    case "Last Name": querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                        break;

                    default: querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                        break;
                }

                // Use the SDK RetrieveMultiple method to extract a listed of records from Dynamics 365.
                var contactRecords = crmConnection.RetrieveMultiple(querycontact);
                
                // Build the list of contact (model) records
                foreach (var a in contactRecords.Entities)
                {
 
                    contactList.Add(new Contact(a.Attributes.ContainsKey("firstname")? a.Attributes["firstname"].ToString() : null,
                                                a.Attributes.ContainsKey("lastname") ? a.Attributes["lastname"].ToString() : null,
                                                a.Attributes.ContainsKey("jobtitle") ? a.Attributes["jobtitle"].ToString() : null,
                                                a.Attributes.ContainsKey("emailaddress1") ? a.Attributes["emailaddress1"].ToString() : null,
                                                a.Attributes.ContainsKey("telephone1") ? a.Attributes["telephone1"].ToString() : null,
                                                a.Attributes.ContainsKey("contactid") ? a.Attributes["contactid"].ToString() : null,
                                                a.Attributes.ContainsKey("new_chosenreference") ? a.Attributes["new_chosenreference"].ToString() : null));
                }

                // Determine if there are previous and next page options in the list and generate a paginated list
                // skipping over pages already viewed - calculated based on page number. 
                var totalPages = (int)Math.Ceiling(contactList.Count / (double)pageSize);
                var sortedContactList = contactList.OrderBy(x => x.LastName).ToList();
                hasPreviouPage = (PageNumber > 0) ? true : false;
                hasNextPage = ((PageNumber + 1) < totalPages) ? true : false;
                                   
                paginatedContactList = (PageNumber <= totalPages) ? sortedContactList.Skip((int)PageNumber * pageSize).Take(pageSize).ToList() : null;

                PageNumber++;
            
            }
            catch (Exception ex)
            {
                // If there is an exception then return an empty list
                paginatedContactList = null;
            }

            return (paginatedContactList, PageNumber, hasPreviouPage, hasNextPage);
        }

    }
}