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

            ViewBag.CrmConnection = (service != null) ? true : false;
            ViewBag.RadioButtonSelected = (option == null) ? "Last Name" : option;
            ViewBag.SearchValue = search;
            ViewBag.PageNumber = nextPageNumber;
            ViewBag.HasPreviousPage = hasPreviousPage;
            ViewBag.HasNextPage = hasNextPage;

            return View(paginatedContactList);
        }

        public ActionResult Create(string option, string search, int? currentPageNumber)
        {
            ViewBag.CrmConnection = true;
            ViewBag.RadioButtonSelected = (option == null) ? "Last Name" : option;
            ViewBag.SearchValue = search;
            ViewBag.PageNumber = currentPageNumber;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Contact newContact, string searchValue, string selectionValue, int? currentPageNumber)
        {
           
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new CreateContact().AddContactToDynamics(newContact, service);

            ViewBag.CrmConnection = (service != null) ? true : false;

            if (success)
            {
                var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
                return RedirectToAction("Index", new { option = selectionValue, search = searchValue, currentPageNumber = pageNumber });
            }
            else
            {
                ViewBag.ErrorMessage = "New record creation failed.";
                return View();
            }
        }

         public ActionResult DeleteContact(string id, string searchValue, string selectionValue, int? currentPageNumber)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            Entity contact = new Entity("contact");

            if (service != null)
            {
                service.Delete(contact.LogicalName, Guid.Parse(id));
            }
            var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
            return RedirectToAction("Index", new { option = selectionValue, search = searchValue, currentPageNumber = pageNumber });
        }

        public ActionResult EditContact(string id, string searchValue, string selectionValue, int? currentPageNumber)
        {
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var updateContact = new NewGetContact().GetContact(crmConnection, id);

            ViewBag.CrmConnection = (service != null) ? true : false;
            ViewBag.RadioButtonSelected = selectionValue;
            ViewBag.SearchValue = searchValue;
            ViewBag.PageNumber = currentPageNumber;
            return View(updateContact);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(Contact updatedContact, string searchValue, string selectionValue, int? currentPageNumber)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new UpdateContact().UpdateContactData(service, updatedContact);

            ViewBag.CrmConnection = (service != null) ? true : false;

            if (success)
            {
                var pageNumber = (currentPageNumber > 0) ? currentPageNumber - 1 : null;
                return RedirectToAction("Index", new {option = selectionValue, search = searchValue, currentPageNumber = pageNumber });
            }
            else
            {
                ViewBag.ErrorMessage = "Record update failed.";
                ViewBag.CrmConnection = (service != null) ? true : false;
                ViewBag.RadioButtonSelected = selectionValue;
                ViewBag.SearchValue = searchValue;
                ViewBag.PageNumber = currentPageNumber;
                return View();
            }
        }
    }
}