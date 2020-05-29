using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Dynamics_365_WebApp.DAL
{
    /// <summary>
    /// Creates the endpoint connection string from valuse contained in Web.config appSettings
    /// and implements an authenticated connection to the Dynamics endpoint
    /// </summary>
    public class CreateDynamicsConnection
    {
        public (IOrganizationService, IOrganizationService) ConnectToDynamics()
        {
            IOrganizationService service;

            try 
            {
                // Get the encoded password from appSettings and decode it
                var encodedBytes = Convert.FromBase64String(ConfigurationManager.AppSettings["Token"].ToString());

                // Create the Dynamics 365 Connection string using web.config settings 
                CrmServiceClient CrmConnection = new CrmServiceClient(ConfigurationManager.AppSettings["AuthType"].ToString()           +
                                                                    ConfigurationManager.AppSettings["UserName"].ToString()             +
                                                                    "password=" + ASCIIEncoding.ASCII.GetString(encodedBytes) + "; "    +
                                                                    ConfigurationManager.AppSettings["Url"].ToString());

                // Implements an authenticated connection to the Dynamics endpoint
                service = (IOrganizationService)CrmConnection.OrganizationWebProxyClient != null ?
                          (IOrganizationService)CrmConnection.OrganizationWebProxyClient :
                          (IOrganizationService)CrmConnection.OrganizationServiceProxy;

                return (CrmConnection, service);
            }
            catch 
            {
                // If connectiuon fails then return null values
                return (null, null);           
            }

        }

    }
}