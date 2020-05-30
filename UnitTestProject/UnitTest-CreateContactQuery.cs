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
            var entityName = "test";
            var queryContact = new CreateContactQuery(null, null, entityName).
                BuildContactQueryExpression();

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void FirstName_InputValue_Returns_FirstNameQuery()
        {
            var entityName = "test";
            var queryContact = new CreateContactQuery("First Name", null, entityName).
                BuildContactQueryExpression();

            Assert.AreEqual("firstname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void LastName_And_Search_InputValues_Return_Query_With_SearchCriteria_And_FirstName()
        {
            var entityName = "test";
            var queryContact = new CreateContactQuery("Last Name", "Kent", entityName).
                BuildContactQueryExpression();

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("Kent", queryContact.Criteria.Conditions[0].Values[0]);
        }
    }
}
