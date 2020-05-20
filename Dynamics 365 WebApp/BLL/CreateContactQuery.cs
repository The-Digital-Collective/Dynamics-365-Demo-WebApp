using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dynamics_365_WebApp.DAL
{
    public class CreateContactQuery
    {
        public QueryExpression BuildContactQueryExpression(string searchOption, string searchValue)
        {
            var querycontact = new QueryExpression()
            {
                EntityName = "contact",
                ColumnSet = new ColumnSet(allColumns: true),
                Criteria = new FilterExpression()
            };

            // Select search criteria based on the radio button selection and create the query.
            switch (searchOption)
            {
                case "First Name":
                    querycontact.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                    break;

                case "Last Name":
                    querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                    break;

                default:
                    querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, searchValue == null ? "" : searchValue);
                    break;
            }

            return querycontact;
        }
    }
}