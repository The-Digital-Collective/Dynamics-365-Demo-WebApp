using System;
using Dynamics_365_WebApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class QueryExpression_ReturnsSearchCriteria
    {
        [TestMethod]
        public void Null_InputValues_Returns_DefaultQuery()
        {
            var queryContact = new ContactEntityQuery().GetContactQueryExpression(null, null);

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void FirstName_InputValue_Returns_FirstNameQuery()
        {
            var queryContact = new ContactEntityQuery().GetContactQueryExpression("First Name", null);

            Assert.AreEqual("firstname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("", queryContact.Criteria.Conditions[0].Values[0]);
        }

        [TestMethod]
        public void LastName_And_Search_InputValues_Return_Query_With_SearchCriteria_And_FirstName()
        {
            var queryContact = new ContactEntityQuery().GetContactQueryExpression("Last Name", "Kent");

            Assert.AreEqual("lastname", queryContact.Criteria.Conditions[0].AttributeName);
            Assert.AreEqual("Kent", queryContact.Criteria.Conditions[0].Values[0]);
        }
    }
}
