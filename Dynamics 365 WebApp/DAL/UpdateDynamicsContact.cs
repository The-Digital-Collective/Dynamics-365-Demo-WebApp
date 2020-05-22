using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;


namespace Dynamics_365_WebApp.DAL
{
    public class UpdateDynamicsContact
    {
        // Update the Dynamics 365 contact entity with the data in updateContact. Dynamics entities 
        // hold attributes as key value pairs, so for each attribute in updateContact, if there is a 
        // value then create the contact Entity key value pair. If the updateContact attribute is null 
        // then do nothing - do not set up a key without a value as it will throw an exception. 
        public bool UpdateContactData(IOrganizationService service, Contact updatedContact)
        {
            try
            {
                Entity contact = new Entity("contact");

                contact = service.Retrieve(contact.LogicalName, Guid.Parse(updatedContact.ContactId), new ColumnSet(true));

                if (updatedContact.FirstName != null)       contact["firstname"] = updatedContact.FirstName.ToString();
                if (updatedContact.LastName != null)        contact["lastname"] = updatedContact.LastName.ToString();
                if (updatedContact.JobTitle != null)        contact["jobtitle"] = updatedContact.JobTitle.ToString();
                if(updatedContact.BusinessPhone != null)    contact["telephone1"] = updatedContact.BusinessPhone.ToString();
                if (updatedContact.Email != null)           contact["emailaddress1"] = updatedContact.Email.ToString();
                if (updatedContact.ChosenReference != null) contact["new_chosenreference"] = updatedContact.ChosenReference.ToString();
                
                service.Update(contact);
                return (true);
            }
            catch (Exception ex)
            {
                return (false);
            }

        }
        // a.Attributes.ContainsKey("firstname")? a.Attributes["firstname"].ToString() : null

    }
}