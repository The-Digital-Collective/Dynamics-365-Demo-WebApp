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
        private List<Contact> _contactList;
        private int? _currentPageNumber;
        private int _pageSize;
        
        public PaginateContactList()
        {

        }
        public PaginateContactList(bool paginateFeature, List<Contact> contactList, int? currentPageNumber, int pageSize)
        {
            _paginateFeature = paginateFeature;
            _contactList = contactList;
            _currentPageNumber = currentPageNumber;
            _pageSize = pageSize;
        }

        public (List<Contact>, int?, bool?, bool?) CreatePaginatedList()
        {
            var sortedContactList = _contactList.OrderBy(x => x.LastName).ToList();

            if (_paginateFeature)
            {
                var PageNumber = (_currentPageNumber == null) ? 0 : _currentPageNumber;
                var totalPages = (int)Math.Ceiling(_contactList.Count / (double)_pageSize);

                var hasPreviouPage = (PageNumber > 0) ? true : false;
                var hasNextPage = ((PageNumber + 1) < totalPages) ? true : false;

                var paginatedContactList = (PageNumber <= totalPages) ? sortedContactList.Skip((int)PageNumber * _pageSize).Take(_pageSize).ToList() : null;

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