using CrmLeadImport.leadsExcel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CrmLeadImport
{
    public partial class CrmServiceWrapper
    {
        private OrganizationServiceProxy organizationServiceProxy
        {
            get;
            set;
        }

        public CrmServiceWrapper(Uri serviceUri, ClientCredentials credentials)
        {
            this.organizationServiceProxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
        }

        public List<Guid> ImportLeads(List<Export> test)
        {
            try
            {
                Entity lead = new Entity("lead");
                List<Guid> rGuid = new List<Guid>();
                foreach (var r in test)
                {
                    lead.Attributes["subject"] = r.Subject;
                    lead.Attributes["firstname"] = r.FirstName;
                    lead.Attributes["lastname"] = r.LastName;
                    lead.Attributes["companyname"] = r.CompanyName;
                    lead.Attributes["numberofemployees"] = Convert.ToInt32(r.NumberOfEmployees);
                    lead.Attributes["revenue"] = Convert.ToDecimal(r.Revenue);
                    rGuid.Add(this.organizationServiceProxy.Create(lead));
                }
                return rGuid;
            }
            catch
            {
                Console.WriteLine();
                organizationServiceProxy.Dispose();
                return null;
            }
        }
    }
} 

