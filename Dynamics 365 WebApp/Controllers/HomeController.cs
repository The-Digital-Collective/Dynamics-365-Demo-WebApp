using Dynamics_365_WebApp.DAL;
using System.Web.Mvc;
using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Web.Routing;

namespace Dynamics_365_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string option, string search)
        {
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var contactList = new NewGetContactList().GetListOfContacts(crmConnection, search, option);

            ViewBag.CrmConnection = (service != null) ? true : false;
            ViewBag.RadioButtonSelected = (option == null) ? "Last Name" : option;
            ViewBag.SearchValue = search;

            return View(contactList);
        }

        public ActionResult Create(string option, string search)
        {
            ViewBag.CrmConnection = true;
            ViewBag.RadioButtonSelected = (option == null) ? "Last Name" : option;
            ViewBag.SearchValue = search;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Contact newContact, string searchValue, string selectionValue)
        {
           
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new CreateContact().AddContactToDynamics(newContact, service);

            ViewBag.CrmConnection = (service != null) ? true : false;

            if (success)
            {
                return RedirectToAction("Index", new { option = selectionValue, search = searchValue });
            }
            else
            {
                ViewBag.ErrorMessage = "New record creation failed.";
                return View();
            }
        }

         public ActionResult DeleteContact(string id, string searchValue, string selectionValue)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            Entity contact = new Entity("contact");

            if (service != null)
            {
                service.Delete(contact.LogicalName, Guid.Parse(id));
            }

            return RedirectToAction("Index", new { option = selectionValue, search = searchValue });
        }

        public ActionResult EditContact(string id, string searchValue, string selectionValue)
        {
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var updateContact = new NewGetContact().GetContact(crmConnection, id);

            ViewBag.CrmConnection = (service != null) ? true : false;
            ViewBag.RadioButtonSelected = selectionValue;
            ViewBag.SearchValue = searchValue;
            return View(updateContact);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(Contact updatedContact, string searchValue, string selectionValue)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new UpdateContact().UpdateContactData(service, updatedContact);

            ViewBag.CrmConnection = (service != null) ? true : false;

            if (success)
            {
                return RedirectToAction("Index", new {option = selectionValue, search = searchValue});
            }
            else
            {
                ViewBag.ErrorMessage = "Record update failed.";
                ViewBag.CrmConnection = (service != null) ? true : false;
                ViewBag.RadioButtonSelected = selectionValue;
                ViewBag.SearchValue = searchValue;
                return View();
            }
        }
    }
}