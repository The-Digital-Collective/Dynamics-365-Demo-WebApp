using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dynamics_365_WebApp.Interfaces;

namespace Dynamics_365_WebApp.BLL
{
    public class FeatureSwitch : IFeatureSwitch
    {
        public bool CheckPaginationFeatureAllowed()
        {
            bool paginationSwitch = false;

            using (var db = new FeatureSwitchesEntities())
            {
                var query = from b in db.FeatureSwitches select b;

                foreach (var item in query)
                {
                    paginationSwitch = item.Pagination;
                }
            }

            return paginationSwitch;
        }
    }
}