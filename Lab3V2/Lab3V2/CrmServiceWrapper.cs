using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Lab3V2
{
    class CrmServiceWrapper
    {
        private OrganizationServiceProxy organizationServiceProxy
        {
            get;
            set;
        }
        //private OrganizationServiceContext organizationServiceContext
        //{
        //    get;
        //    set;
        //}

        public CrmServiceWrapper(Uri serviceUri, ClientCredentials credentials)
        {
            this.organizationServiceProxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            //this.organizationServiceContext = new OrganizationServiceContext(this.organizationServiceProxy);

        }

        public EntityCollection QueryExpressionExample()
        {
            try
            {
                Entity lead = new Entity("contact");

                QueryExpression qContact = new QueryExpression("contact");
                ColumnSet cset = new ColumnSet();
                FilterExpression filterJobMan = new FilterExpression();
                ConditionExpression condExp = new ConditionExpression();
                cset.AddColumns("fullname", "parentcustomerid");
                condExp.AttributeName = "jobtitle";
                condExp.Operator = ConditionOperator.Equal;
                condExp.Values.Add("Менеджер по закупкам");
                filterJobMan.AddCondition(condExp);
                qContact.ColumnSet = cset;
                qContact.Criteria.AddFilter(filterJobMan);
                EntityCollection contacts = this.organizationServiceProxy.RetrieveMultiple(qContact);

                return contacts;
            }
            catch
            {
                Console.WriteLine();
                //organizationServiceContext.Dispose();
                organizationServiceProxy.Dispose();
                return null;
            }
        }
    }
}
