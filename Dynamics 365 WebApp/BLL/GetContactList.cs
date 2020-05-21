using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using Dynamics_365_WebApp.BLL;
using System.Configuration;

namespace Dynamics_365_WebApp.DAL
{
    public class GetContactList
    {
        // Gets a list of contacts from the contact Entity using the search value and option data. The returned data 
        // are checked for the existance of a key value pair. If a pair doesn't exist then set the Contact model attribute to null.
        // The search option is set based in the radio buttion selection on the index form.
        // The returned data is filtered into a paginated list.

        public (List<Contact>, int?, bool, bool) GetListOfContacts(IOrganizationService crmConnection, string searchValue, string searchOption, int? currentPageNumber)
        {            
            var queryContact = new CreateContactQuery().BuildContactQueryExpression(searchOption, searchValue);

            var contactRecords = crmConnection.RetrieveMultiple(queryContact);

            var contactList = new GetDynamicsContacts().GetContactList(contactRecords);

            var pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

            return (new PaginateContactList().CreatePaginatedList(contactList, currentPageNumber, pageSize));
        }

    }
}