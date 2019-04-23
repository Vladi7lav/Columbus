using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
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
            
            //Задание 3.1 (метод QueryExpressionExample)
            //ConnectProxy con = new ConnectProxy(serviceUri, credentials);
            //EntityCollection contacts = QueryExpressionExample(con.OrganizationServiceProxy);
            //con.Dispose();
            //foreach (var c in contacts.Entities)
            //{
            //    Console.WriteLine(c.Attributes["fullname"].ToString());
            //}

            //Задание 3.3 (Использование FetchXML запроса)
            //string fetch = @"<fetch mapping='logical'> 
            //                        <entity name='lead'> 
            //                            <attribute name='fullname'/> 
            //                            <filter type='and'> 
            //                            <condition attribute='jobtitle' operator='eq' value='Менеджер по закупкам'/> 
            //                            </filter> 
            //                        </entity> 
            //                 </fetch>";
            //ConnectProxy con = new ConnectProxy(serviceUri, credentials);
            //EntityCollection contactlist = (EntityCollection)con.OrganizationServiceProxy.RetrieveMultiple(new FetchExpression(fetch));
            //con.Dispose();
            //foreach (var contact in contactlist.Entities)
            //{
            //    Console.WriteLine(contact.Attributes["fullname"].ToString());
            //}

            //Задание 3.4 (Использование фильтрованных представлений)

                       

            Console.Read();           
        }

        public static EntityCollection QueryExpressionExample(OrganizationServiceProxy con)
        {
            try
            {                                
                ColumnSet cset = new ColumnSet();
                cset.AddColumns("fullname", "parentcustomerid");
                                
                ConditionExpression condExp = new ConditionExpression();                
                condExp.AttributeName = "jobtitle";
                condExp.Operator = ConditionOperator.Equal;
                condExp.Values.Add("Purchasing Assistant");

                FilterExpression filterJobMan = new FilterExpression();
                filterJobMan.AddCondition(condExp);

                QueryExpression qContact = new QueryExpression("contact");
                qContact.ColumnSet = cset;
                qContact.Criteria.AddFilter(filterJobMan);

                EntityCollection contacts = con.RetrieveMultiple(qContact);
                return contacts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
    }
}
