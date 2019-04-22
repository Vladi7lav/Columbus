using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Lab3V2
{
    class Program
    {
        static void Main(string[] args)
        {

            var credentials = new ClientCredentials
            {
                Windows = { ClientCredential = new NetworkCredential("Administrator", "Pass@word99") }
            };

            Uri serviceUri = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc");
            EntityCollection contacts = new CrmServiceWrapper(serviceUri, credentials).QueryExpressionExample();
            foreach (var c in contacts.Entities)
            {
                Console.WriteLine(c.Attributes["fullname"].ToString());
            }
            Console.Read();           
        }
    }
}
