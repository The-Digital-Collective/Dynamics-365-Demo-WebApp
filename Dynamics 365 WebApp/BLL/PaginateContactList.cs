using Dynamics_365_WebApp.Models;
using Dynamics_365_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Dynamics_365_WebApp.BLL
{
    public class PaginateContactList
    {
        private bool _paginateFeature;
        public PaginateContactList(bool paginateFeature)
        {
            _paginateFeature = paginateFeature;
        }

        public (List<Contact>, int?, bool?, bool?) CreatePaginatedList(List<Contact> contactList, int? currentPageNumber, int pageSize)
        {
            var sortedContactList = contactList.OrderBy(x => x.LastName).ToList();

            if (_paginateFeature)
            {
                var PageNumber = (currentPageNumber == null) ? 0 : currentPageNumber;
                var totalPages = (int)Math.Ceiling(contactList.Count / (double)pageSize);

                var hasPreviouPage = (PageNumber > 0) ? true : false;
                var hasNextPage = ((PageNumber + 1) < totalPages) ? true : false;

                var paginatedContactList = (PageNumber <= totalPages) ? sortedContactList.Skip((int)PageNumber * pageSize).Take(pageSize).ToList() : null;

                PageNumber++;

                return (paginatedContactList, PageNumber, hasPreviouPage, hasNextPage);
            }
            else
            {
                return (sortedContactList, null, false, false);
            }
        }
    }
}