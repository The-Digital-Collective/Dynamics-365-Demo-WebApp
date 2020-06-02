using System;
using System.Configuration;

namespace Dynamics_365_WebApp.BLL
{
    public class IncompleteFeature
    {
        private System.Threading.Tasks.Task<bool> _ignoreCode;
        public IncompleteFeature()
        {
            _ignoreCode = new AzureFeatureManager(ConfigurationManager.AppSettings["Sprint23-Development-1"].ToString())
                .GetAzureFeatureFlag();
        }

        public void BrokenlMethod()
        {
            if (!_ignoreCode.Result)
            {
                // Do some stuff and then...
                throw new NullReferenceException("The method 'BrokenMethod' threw a wobble" );
            }
                
        }
    }
}