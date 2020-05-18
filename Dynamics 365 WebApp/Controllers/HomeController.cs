using Dynamics_365_WebApp.DAL;
using System.Web.Mvc;
using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics_365_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string option, string search, int? currentPageNumber)
        {
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var (paginatedContactList, nextPageNumber, hasPreviousPage, hasNextPage) = new NewGetContactList().GetListOfContacts(crmConnection, search, option, currentPageNumber);

            MyViewData.SetData(service, option, search, nextPageNumber, hasPreviousPage, hasNextPage, null);
            
            return View(paginatedContactList);
        }

        public ActionResult Create(string option, string search, int? currentPageNumber)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Contact newContact, string search, string option, int? currentPageNumber)
        {
           
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new CreateContact().AddContactToDynamics(newContact, service);

            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);

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
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var updateContact = new NewGetContact().GetContact(crmConnection, id);

            MyViewData.SetData(service, option, search, currentPageNumber, null, null, null);

            return View(updateContact);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(Contact updatedContact, string search, string option, int? currentPageNumber)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new UpdateContact().UpdateContactData(service, updatedContact);
            
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