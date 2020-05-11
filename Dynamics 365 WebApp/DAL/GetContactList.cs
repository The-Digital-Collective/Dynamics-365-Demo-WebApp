using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamics_365_WebApp.DAL
{
    public class GetContactList
    {
        /// <summary>
        /// Uses the connection string to authenticate a Linq query
        /// to Dynamics and the save the results in a list of type Contact (see Model).
        /// 
        /// Note: the query throws an exception 'the given key was not present in the dictionary' 
        /// if any of the attributes are empty (such as email address); they may have null values 
        /// but defaulting 'firstname', 'lastname', etc. to "" if the value is null doesn't fix the issue.
        /// Therefore for the demo I've targeted data that doesn't have empty values. This area needs a story 
        /// and more investigation - possibly with MS support. 
        /// </summary>
        /// <param name="crmConnection"></param>
        /// <returns> List of contact objects </returns>
        public List<Contact> GetListOfContacts(IOrganizationService crmConnection)
        {
            // The entity type comes from the Dynamics SDK which is installed as a NuGet package
            // and is used to create the specific Dynamics entity model; in this example the 'contact' entity.
            Entity contact = new Entity("contact");
            List<Contact> ContactList = new List<Contact>();

            try
            {
                // Linq query to select records from the contact entity in Dynamics
                OrganizationServiceContext serviceContext = new OrganizationServiceContext(crmConnection);
                var query = from attribute in serviceContext.CreateQuery("contact")
                            where (string)contact["firstname"] == "Adrian" ||
                                  (string)contact["firstname"] == "Allison" ||
                                  (string)contact["firstname"] == "Andrew" ||
                                  (string)contact["firstname"] == "Alexis" ||
                                  (string)contact["lastname"] == "Belacqua" ||
                                  (string)contact["lastname"] == "Quill"   ||
                                  (string)contact["firstname"] == "Nick"   
                            orderby attribute["new_chosenreference"]
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
                    // Add each query record into the list of contacts and retrun
                    ContactList.Add(new Contact(record.contact.firstName, 
                                                record.contact.lastName, 
                                                record.contact.jobTitle, 
                                                record.contact.email, 
                                                record.contact.businessPhone, 
                                                record.contact.contactId,
                                                record.contact.chosenReference));
                }
            }
            catch 
            {
                // If there is an exception then return a blank list
                ContactList.Add(new Contact(" ", " ", " ", " ", " ", " ", " "));
            }

             return (ContactList);
        }

     }
}