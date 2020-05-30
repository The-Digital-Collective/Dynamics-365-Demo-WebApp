using Microsoft.Xrm.Sdk.Query;
using System.Configuration;


namespace Dynamics_365_WebApp.DAL
{
    public class CreateContactQuery
    {
        private string _searchOption;
        private string _searchValue;
        private string _entityName;

        public CreateContactQuery()
        {

        }

        public CreateContactQuery(string searchOption, string searchvalue, string entityName)
        {
            _searchOption = searchOption;
            _searchValue = searchvalue;
            _entityName = entityName;
        }
        public QueryExpression BuildContactQueryExpression()
        {
            var querycontact = new QueryExpression()
            {
                EntityName = _entityName,
                ColumnSet = new ColumnSet(allColumns: true),
                Criteria = new FilterExpression()
            };

            // Select search criteria based on the radio button selection and create the query.
            switch (_searchOption)
            {
                case "First Name":
                    querycontact.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, _searchValue == null ? "" : _searchValue);
                    break;

                case "Last Name":
                    querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, _searchValue == null ? "" : _searchValue);
                    break;

                default:
                    querycontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, _searchValue == null ? "" : _searchValue);
                    break;
            }

            return querycontact;
        }
    }
}