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

                if (newContact.FirstName != null) contact["firstname"] = newContact.FirstName.ToString();
                if (newContact.LastName != null) contact["lastname"] = newContact.LastName.ToString();
                if (newContact.JobTitle != null) contact["jobtitle"] = newContact.JobTitle.ToString();
                if (newContact.BusinessPhone != null) contact["telephone1"] = newContact.BusinessPhone.ToString();
                if (newContact.Email != null) contact["emailaddress1"] = newContact.Email.ToString();
                if (newContact.ChosenReference != null) contact["new_chosenreference"] = newContact.ChosenReference.ToString();
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