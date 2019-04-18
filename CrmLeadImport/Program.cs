using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using System.Data;
using System.IO;
using System.Globalization;

namespace LeadImport
{
    class Program
    {
        

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("Lead.csv");
            //var test = File.OpenRead("Lead.csv");
            DataTable leads = sr;
            //Excel leads = File.ReadAllLines("C:\Users\TSIV\source\repos\CrmLeadImport\CrmLeadImport\Lead.csv");
            IOrganizationService p = GetOrganization();
            foreach (DataRow rows in leads.Rows)
            {
                Entity lead = new Entity("lead");
                lead.Attributes["subject"] = rows["Topic"];
                lead.Attributes["firstname"] = rows["First Name"];
                lead.Attributes["lastname"] = rows["Last Name"];
                lead.Attributes["companyname"] = rows["Company Name"];
                lead.Attributes["numberofemployees"] = (int)rows["Number of Employees"];
                lead.Attributes["revenue"] = decimal.Parse(rows["Annual Revenue"].ToString(), NumberStyles.Currency);
                Guid g2 = p.Create(lead);
                Console.WriteLine("Создан интерес для {0} {1}", rows["First Name"], rows["Last Name"]);
            }

        }

        private static IOrganizationService GetOrganization()
        {
            Uri organizationUri = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc"); 
            var cred = new ClientCredentials
            {
                Windows = { ClientCredential = new NetworkCredential("Administrator", "Pass@word99") }
            };
            OrganizationServiceProxy _serviceproxy = new
            OrganizationServiceProxy(organizationUri, null, cred,
            null);
            IOrganizationService _service = (IOrganizationService)_serviceproxy;
            return _service;
        }

    }
}
