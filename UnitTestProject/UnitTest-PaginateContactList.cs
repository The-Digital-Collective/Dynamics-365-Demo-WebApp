using System.Collections.Generic;
using Dynamics_365_WebApp.BLL;
using Dynamics_365_WebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Paginate
{
    [TestClass]
    public class TestPaginateContactList
    {

        [TestMethod]
        public void RecordCount_EqualsPageSize_CurrentPage_EqualsZero_Returns_False_NextPage_False_PreviousPage_IncrementedPageNumber_ListCount_EqualTo_PageSize()
        {

            var contactList = new List<Contact>();
            var sortedContactList = new List<Contact>();
            int? pageNumber = 0;
            int pageSize = 12;
            int recordCount = 12;
            int? nextPageNumber;
            bool hasNextPage;
            bool hasPreviousPage;


            for (var i = 0; i < recordCount; i++)
            {
                contactList.Add(new Contact());
            }


            (sortedContactList, nextPageNumber, hasPreviousPage, hasNextPage) = new PaginateContactList().CreatePaginatedList(contactList, pageNumber, pageSize);

            Assert.IsFalse(hasNextPage);
            Assert.IsFalse(hasPreviousPage);
            Assert.AreEqual(pageNumber + 1, nextPageNumber);
            Assert.AreEqual(sortedContactList.Count, pageSize);
        }

        [TestMethod]
        public void LastPage_In_RecordCount_Returns_NextPage_False_PreviousPage_True()
        {

            var contactList = new List<Contact>();
            int? pageNumber = 2;
            int pageSize = 12;
            int recordCount = 36;
            bool hasNextPage;
            bool hasPreviousPage;


            for (var i = 0; i < recordCount; i++)
            {
                contactList.Add(new Contact());
            }


            (_, _, hasPreviousPage, hasNextPage) = new PaginateContactList().CreatePaginatedList(contactList, pageNumber, pageSize);

            Assert.IsFalse(hasNextPage);
            Assert.IsTrue(hasPreviousPage);
        }

        [TestMethod]
        public void FirstPage_In_RecordCount_Returns_NextPage_True_PreviousPage_False()
        {

            var contactList = new List<Contact>();
            int? pageNumber = 0;
            int pageSize = 12;
            int recordCount = 36;
            bool hasNextPage;
            bool hasPreviousPage;


            for (var i = 0; i < recordCount; i++)
            {
                contactList.Add(new Contact());
            }


            (_, _, hasPreviousPage, hasNextPage) = new PaginateContactList().CreatePaginatedList(contactList, pageNumber, pageSize);

            Assert.IsTrue(hasNextPage);
            Assert.IsFalse(hasPreviousPage);
        }

        [TestMethod]
        public void Records_Greater_Than_PageSize_Returns_Count_EqualTo_PageSize()
        {
            var contactList = new List<Contact>();
            var sortedContactList = new List<Contact>();
            int? pageNumber = 5;
            int pageSize = 12;
            int recordCount = 100;



            for (var i = 0; i < recordCount; i++)
            {
                contactList.Add(new Contact());
            }


            (sortedContactList, _, _, _) = new PaginateContactList().CreatePaginatedList(contactList, pageNumber, pageSize);

            Assert.AreEqual(sortedContactList.Count, pageSize);
        }
    }
}
