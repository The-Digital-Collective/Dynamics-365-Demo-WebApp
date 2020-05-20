using System;
using Dynamics_365_WebApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Dynamics_365_WebApp.BLL;

namespace UnitTests.ContactList
{
    [TestClass]
    public class TestGetContactList
    {
        [TestMethod]
        public void List_Created_From_Complete_Set_Of_Records()
        {
            EntityCollection contactRecords = new EntityCollection();
            KeyAttributeCollection altKey = new KeyAttributeCollection();

            Entity contact1 = new Entity("contact", altKey);
            contact1["firstname"] = "Bob";
            contact1["lastname"] = "Smith";
            contact1["jobtitle"] = "CEO";
            contact1["emailaddress1"] = "bob@bobthebuilder.com";
            contact1["telephone1"] = "01234 567890";
            contact1["contactid"] = "1234567-8909876-5432100";
            contact1["new_chosenreference"] = "0001";

            contactRecords.Entities.Add(contact1);

            Entity contact2 = new Entity("contact", altKey);
            contact2["firstname"] = "Mark";
            contact2["lastname"] = "Armstrong";
            contact2["jobtitle"] = "DBA";
            contact2["emailaddress1"] = "mark@newzealanddba.com";
            contact2["telephone1"] = "64 9 1234 567890";
            contact2["contactid"] = "1234567-8909876-5432101";
            contact2["new_chosenreference"] = "0002";

            contactRecords.Entities.Add(contact2);

            var contactList = new GetDynamicsContacts().GetContactList(contactRecords);

            Assert.AreEqual(contactList.Count, 2);
            Assert.AreEqual(contactList[0].FirstName, "Bob");
            Assert.AreEqual(contactList[1].LastName, "Armstrong");
        }

        [TestMethod]
        public void Missing_Key_Value_Pairs_Returned_As_Null_Other_Key_Value_Pairs_Return_A_Value()
        {
            EntityCollection contactRecords = new EntityCollection();
            KeyAttributeCollection altKey = new KeyAttributeCollection();

            Entity contact1 = new Entity("contact", altKey);
            contact1["lastname"] = "Smith";
            contact1["jobtitle"] = "CEO";
            contact1["emailaddress1"] = "bob@bobthebuilder.com";
            contact1["telephone1"] = "01234 567890";
            contact1["new_chosenreference"] = "0001";

            contactRecords.Entities.Add(contact1);

            var contactList = new GetDynamicsContacts().GetContactList(contactRecords);

            Assert.AreEqual(contactList.Count, 1);
            Assert.AreEqual(contactList[0].FirstName, null);
            Assert.AreEqual(contactList[0].ContactId, null);
            Assert.AreEqual(contactList[0].JobTitle, "CEO");
            Assert.AreEqual(contactList[0].ChosenReference, "0001");
        }

        [TestMethod]
        public void Empty_Recordset_Returns_One_Null_Record()
        {
            EntityCollection contactRecords = new EntityCollection();
            KeyAttributeCollection altKey = new KeyAttributeCollection();

            Entity contact1 = new Entity("contact", altKey);
   
            contactRecords.Entities.Add(contact1);

            var contactList = new GetDynamicsContacts().GetContactList(contactRecords);

            Assert.AreEqual(contactList.Count, 1);
            Assert.AreEqual(contactList[0].FirstName, null);
            Assert.AreEqual(contactList[0].ContactId, null);
        }
    }
}



