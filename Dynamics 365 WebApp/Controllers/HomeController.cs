using Dynamics_365_WebApp.DAL;
using System.Web.Mvc;
using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics_365_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchOption, string searchValue, int? currentPageNumber)
        {
            // Connects to Dynamics 365 and gets a list of contacts using the search data and option values
            // from the search function on the index page. A sorted paginated list is returned, along with 
            // boolian states for next and previous pages. These states are calculated based on number of 
            // pages and the current position in the paginated list. 

            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var (paginatedContactList, nextPageNumber, hasPreviousPage, hasNextPage) = new GetContactList().GetListOfContacts(crmConnection, searchValue, searchOption, currentPageNumber);

            // Save values for use on the index page.
            MyViewData.SetData(service, searchOption, searchValue, nextPageNumber, hasPreviousPage, hasNextPage, null);
            
            return View(paginatedContactList);
        }

        public ActionResult Create(string option, string search, int? currentPageNumber)
        {
            // Verifies the connection to Dynamics 365 and sets values for use on the index page.
            var (_, service) = new DynamicsConnection().ConnectToDynamics();

            // Save values for use on the index page.
            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Contact newContact, string search, string option, int? currentPageNumber)
        {
            // Verifies the connection and adds the newContact data to the Dynamics 365 contact entity. 
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new CreateContact().AddContactToDynamics(newContact, service);

            // Save values for use on the index page.
            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);

            // If the contact entity record creation was successful then set the page number to the current page
            // and, if successful, redirect to the Index method. Otherwise display a record creation failed message
            // on the current view
            if (success)
            {
                var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
                return RedirectToAction("Index", new { option = option, search = search, currentPageNumber = pageNumber });
            }
            else
            {
                MyViewData.SetData(service, option, search, currentPageNumber, null, null, "New record creation failed.");
                
                return View();
            }
        }

         public ActionResult DeleteContact(string id, string search, string option, int? currentPageNumber)
        {
            // Verifies the connection and if the connection is okay then delete the record from the contact entity
            // using the contact entity unique identifier (contactid) passed as id. Then redirect to the index method.
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            Entity contact = new Entity("contact");

            if (service != null)
            {
                service.Delete(contact.LogicalName, Guid.Parse(id));
            }
            var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
            return RedirectToAction("Index", new { option = option, search = search, currentPageNumber = pageNumber });
        }

        public ActionResult EditContact(string id, string search, string option, int? currentPageNumber)
        {
            // Verifies the connection and gets the record identified by 'id' from the contact entity.
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var updateContact = new NewGetContact().GetContact(crmConnection, id);

            // Save values for use on the index page.
            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);

            return View(updateContact);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(Contact updatedContact, string search, string option, int? currentPageNumber)
        {
            // Verifies the connection and calls the update function to update the record in
            // the Dynamics 365 contact entity. Then, if successful, redirect to the Index method, otherwise
            // display an update failed message in the edit view. 
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new UpdateContact().UpdateContactData(service, updatedContact);

            // Save values for use on the index page.
            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);
            
            if (success)
            {
                var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
                return RedirectToAction("Index", new {option = option, search = search, currentPageNumber = pageNumber });
            }
            else
            {
                MyViewData.SetData(service, option, search, currentPageNumber, null, null, "Record update failed.");

                return View();
            }
        }
    }
}