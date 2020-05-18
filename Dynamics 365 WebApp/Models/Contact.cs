using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dynamics_365_WebApp.Models
{
    // Contact model used to hold data extracted from the Dynamics 365 contact entity.
    public class Contact
    {
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string BusinessPhone { get; set; }
        public string ChosenReference { get; set; }

        public Contact()
        {

        }
        public Contact(string firstName, string lastName, string jobTitle, string email, string businessPhone, string contactId, string chosenreference)
        {
            FirstName = firstName;
            LastName = lastName;
            JobTitle = jobTitle;
            Email = email;
            BusinessPhone = businessPhone;
            ContactId = contactId;
            ChosenReference = chosenreference;
        }
    }
}