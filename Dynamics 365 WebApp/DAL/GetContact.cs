using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Linq;

namespace Dynamics_365_WebApp.DAL
{
    public class GetContact
    {
        /// <summary>
        /// Uses the connection string to authenticate a Linq query
        /// to Dynamics and the save the result in a list of type Contact (see Model).
        /// Note: the qery blows up if any of the attributes are null so for the demo
        /// I've targeted data that doesn't have null values. This is an area that needs more
        /// effort to make the query resilient. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> List of contact objects </returns>
        public Contact GetContactForId(IOrganizationService crmConnection, string id)
        {
            // The entity type comes from the Dynamics SDK which is installed as a NuGet package
            // and is used to create the specific Dynamics entity model; in this example the 'contact' entity.
            Entity contact = new Entity("contact");
            Contact updateContact = null;

            try
            {
                OrganizationServiceContext serviceContext = new OrganizationServiceContext(crmConnection);
                var query = from attribute in serviceContext.CreateQuery("contact")
                            where (string)contact["contactid"] == id
                            select new
                            {
                                contact = new
                                {
                                    firstName = attribute["firstname"].ToString(),
                                    lastName = attribute["lastname"].ToString(),
                                    jobTitle = attribute["jobtitle"].ToString(),
                                    email = attribute["emailaddress1"].ToString(),
                                    businessPhone = attribute["telephone1"].ToString(),
                                    contactId = attribute["contactid"].ToString(),
                                    chosenReference = attribute["new_chosenreference"].ToString()
                                }

                            };

                foreach (var record in query)
                {
                    updateContact = new Contact(record.contact.firstName,
                                                record.contact.lastName,
                                                record.contact.jobTitle,
                                                record.contact.email,
                                                record.contact.businessPhone,
                                                 record.contact.contactId,
                                                record.contact.chosenReference);
                }
            }
            catch 
            {
                // If there is an exception then return a blank list
                updateContact = new Contact(" ", " ", " ", " ", " ", " ", " ");
            }

            return (updateContact);
        }
    }
}