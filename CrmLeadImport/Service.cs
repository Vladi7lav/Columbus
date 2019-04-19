using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CrmLeadImport
{
    class Service
    {
        static Uri organizationUri = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc");
        static string name = "Administrator";
        static string pass = "Pass@word99";

        public static Uri serviceUri { get; internal set; }

        private static IOrganizationService GetOrganization()
        {
            try
            {
                var cred = new ClientCredentials
                {
                    Windows = { ClientCredential = new NetworkCredential(name, pass) }
                };
                OrganizationServiceProxy _serviceproxy = new OrganizationServiceProxy(organizationUri, null, cred, null);
                IOrganizationService _service = (IOrganizationService)_serviceproxy;
                return _service;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
    }
}
