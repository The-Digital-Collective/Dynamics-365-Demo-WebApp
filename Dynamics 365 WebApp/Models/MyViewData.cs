﻿using Microsoft.Xrm.Sdk;

namespace Dynamics_365_WebApp.Models
{
    // Attributes used in storing Dynamics 365 connection status plus page scrolling, 
    // radio button selection and page count data.
    public static class MyViewData
    {
        public static bool CrmConnection;
        public static string RadioButtonSelected;
        public static string Search;
        public static int? PageNumber;
        public static bool? HasPreviousPage;
        public static bool? HasNextPage;
        public static string Message;
        public static bool? PaginationFeature;
        public static System.Threading.Tasks.Task<bool> SearchBoxFeatureSwitch;
           
        public static void SetData(IOrganizationService service, string option, string search, int? pageNumber, bool? hasPreviousPage, bool? hasNextPage, string message, bool? paginationFeature, System.Threading.Tasks.Task<bool> searchBoxFeatureSwitch)
        {
            CrmConnection = (service != null) ? true : false;
            RadioButtonSelected = (option == null) ? "Last Name" : option;
            Search = search;
            PageNumber = pageNumber;
            if (hasPreviousPage != null) HasPreviousPage = hasPreviousPage;
            if (hasNextPage != null) HasNextPage = hasNextPage;
            if (message != null) Message = message;
            PaginationFeature = paginationFeature;
            SearchBoxFeatureSwitch = searchBoxFeatureSwitch;
        }
    }
}