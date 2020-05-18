using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;


namespace Dynamics_365_WebApp.DAL
{
    public class CreateContact
    {
        // Insert the newContact record into the Dynamics 365 contact entity. 
        // Dynamics entities hold attributes as key value pairs, so for each attribute in 
        // newContact, if there is a value then create the contact Entity key value pair. 
        // If the newContact attribute is null then do nothing - do not set up a key without
        // a value as it will throw an exception. 
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