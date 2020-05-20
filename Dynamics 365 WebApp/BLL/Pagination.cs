using Dynamics_365_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Dynamics_365_WebApp.BLL
{
    public class Pagination
    {
        public (List<Contact>, int?, bool, bool) CreatePageList(List<Contact> contactList, int? currentPageNumber)
        {
            var pageSize = int.Parse(ConfigurationManager.AppSettings["PageZize"]);
            var PageNumber = (currentPageNumber == null) ? 0 : currentPageNumber;
            var totalPages = (int)Math.Ceiling(contactList.Count / (double)pageSize);
            var sortedContactList = contactList.OrderBy(x => x.LastName).ToList();
            var hasPreviouPage = (PageNumber > 0) ? true : false;
            var hasNextPage = ((PageNumber + 1) < totalPages) ? true : false;

            var paginatedContactList = (PageNumber <= totalPages) ? sortedContactList.Skip((int)PageNumber * pageSize).Take(pageSize).ToList() : null;

            PageNumber++;

            return (paginatedContactList, PageNumber, hasPreviouPage, hasNextPage);
        }
    }
}