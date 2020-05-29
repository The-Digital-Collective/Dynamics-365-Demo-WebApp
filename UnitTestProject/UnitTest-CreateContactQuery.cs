using System;
using Dynamics_365_WebApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.ContactQuery
{
    [TestClass]
    public class TestGetContactList
    {
        [TestMethod]
        public void Null_InputValues_Returns_DefaultQuery()
        {
            var queryContact = new CreateContactQuery().BuildContactQueryExpression(null, null);

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void FirstName_InputValue_Returns_FirstNameQuery()
        {
            var queryContact = new CreateContactQuery().BuildContactQueryExpression("First Name", null);

            Assert.AreEqual("firstname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void LastName_And_Search_InputValues_Return_Query_With_SearchCriteria_And_FirstName()
        {
            var queryContact = new CreateContactQuery().BuildContactQueryExpression("Last Name", "Kent");

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("Kent", queryContact.Criteria.Conditions[0].Values[0]);
        }
    }
}
