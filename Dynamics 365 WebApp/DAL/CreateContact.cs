using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;


namespace Dynamics_365_WebApp.DAL
{
    public class CreateContact
    {
        /// <summary>
        /// Add new contact data into the Dynamics CRM contact entity
        /// </summary>
        /// <param name="newContact"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool AddContactToDynamics(Contact newContact, IOrganizationService service)
        {
            try
            {
                Entity contact = new Entity("contact");
                contact["firstname"] = newContact.FirstName;
                contact["lastname"] = newContact.LastName;
                contact["jobtitle"] = newContact.JobTitle;
                contact["emailaddress1"] = newContact.Email;
                contact["telephone1"] = newContact.BusinessPhone;
                contact["new_chosenreference"] = newContact.ChosenReference;
                service.Create(contact);
                
                return (true);
            }
            catch
            {
                return (false);
            }

        }

    }
}