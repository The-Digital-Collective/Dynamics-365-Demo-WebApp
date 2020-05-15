using Dynamics_365_WebApp.DAL;
using System.Web.Mvc;
using Dynamics_365_WebApp.Models;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics_365_WebApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Establish a connection to Dynamics and get a list of contacts 
        /// </summary>
        /// <returns> List of contacts of type Contact </returns>
        public ActionResult Index(string option, string search)
        {
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var contactList = new NewGetContactList().GetListOfContacts(crmConnection, search, option);

            ViewData["crmConnection"] = (service != null) ? true : false;
            ViewData["radioButtonSelected"] = (option == null) ? "Last Name" : "First Name";

            return View(contactList);
        }

        public ActionResult Create()
        {
            ViewData["crmConnection"] = true;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Contact newContact)
        {
            /// <summary>
            /// Establish a connection to Dynamics and creates a new contact record 
            /// </summary>
            /// <returns> Redirects to the home page or throws up a message if the record creation in Dynamics was not successful </returns>
            
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new CreateContact().AddContactToDynamics(newContact, service);

            ViewData["crmConnection"] = (service != null) ? true : false;

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "New record creation failed, please try again. If the problem persists contact the system administrator.";
                return View();
            }
        }

         public ActionResult DeleteContact(string id)
        {
            // Delete the contact for the given id from Dynamics
            // and redirect to the home page
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            Entity contact = new Entity("contact");

            if (service != null)
            {
                service.Delete(contact.LogicalName, Guid.Parse(id));
            } 

            ViewData["crmConnection"] = (service != null) ? true : false;
            return RedirectToAction("Index");
        }

        public ActionResult EditContact(string id)
        {
            // Get the contact data from Dynamics for the given contact id and pass 
            // to the view.
            var (crmConnection, service) = new DynamicsConnection().ConnectToDynamics();
            var updateContact = new NewGetContact().GetContact(crmConnection, id);

            ViewData["crmConnection"] = (service != null) ? true : false;
            return View(updateContact);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(Contact updatedContact)
        {
            var (_, service) = new DynamicsConnection().ConnectToDynamics();
            var success = new UpdateContact().UpdateContactData(service, updatedContact);

            ViewData["crmConnection"] = (service != null) ? true : false;

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Record update failed, please try again. If the problem persists contact the system administrator.";
                return View();
            }
        }
    }
}