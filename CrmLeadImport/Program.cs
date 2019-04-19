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
using Microsoft.Xrm.Sdk.Query;
using CrmLeadImport;
using CrmLeadImport.leadsExcel;

namespace LeadImport
{
    class Program
    {


        static void Main(string[] args)
        {
            var pathTest = @"C:\Users\TSIV\Documents\Visual Studio 2017\Projects\sample_Dynamic_CRM\CrmLeadImport\leadsExcel\Leads.csv";

            if (File.Exists(pathTest))
            {

                var credentials = new ClientCredentials
                {
                    Windows = { ClientCredential = new NetworkCredential("Administrator", "Pass@word99") }
                };

                Uri serviceUri = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc");
                
                FileStream path = new FileStream(pathTest, FileMode.Open, FileAccess.Read);
                List<Export> test = Export.ReadFile(path);

                CrmServiceWrapper a = new CrmServiceWrapper(serviceUri, credentials);
                List<Guid> rGuids = a.ImportLeads(test);
                foreach (Guid Rguid in rGuids)
                {
                    Console.WriteLine(Rguid);
                }
                Console.Read();
            }
            else
            { Console.WriteLine("Error path"); }
        }
    }
}

