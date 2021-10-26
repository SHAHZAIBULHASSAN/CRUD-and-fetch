using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;

namespace Contection_with_Microsoft_dynamics_365
{
    class Program
    {
        private static CrmServiceClient svc;
        private static Entity updatedContact ;

        static void Main(string[] args)
        {
            #region connection

            

            string authType = "OAuth";
            string userName = "shahzaib@SHAHZAIBSAFDAR1.onmicrosoft.com";
            string password = "safdar786ALI!";
            string     url = "https://org666f01ac.crm.dynamics.com";
            string appId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            string reDirectURI = "app://58145B91-0C36-4500-8554-080854F2AC97";
            string loginPrompt = "Auto";
            string ConnectionString = string.Format("AuthType = {0};Username = {1};Password = {2}; Url = {3}; AppId={4}; RedirectUri={5};LoginPrompt={6}",authType,userName, password, url, appId, reDirectURI, loginPrompt);



           svc = new CrmServiceClient(ConnectionString);

            if (svc.IsReady)
            {
                Console.WriteLine("Connection Successful!...");
                Console.WriteLine("Please enter key Options");
                Console.WriteLine("C....Create...1");
                Console.WriteLine("R...Retrive....2");
                Console.WriteLine("R...Retrive Multiple....3");
                Console.WriteLine("U...Update........4");
                Console.WriteLine("D.....Delete.....5");
                Console.WriteLine("F.....Fetch.....6");
                string a = Console.ReadLine();
                int aa = Convert.ToInt32(a);
                switch (aa)
                {
                    case 1:
                        C();
                        break;
                    case 2:
                        RS();
                        break;
                    case 3:
                        RM();
                        break;
                    case 4:
                        U();
                        break;
                    case 5:
                        D();
                        break;
                    case 6:
                        F();
                        break;

                    default:
                   
                        break;
                }
               



                #region sir code
                /// retrieve all record
                //var myemail = "test@test.com";
                //var fetchXml = @"<?xml version='1.0'?>
                //<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0'>
                // <entity name='contact'>
                // <attribute name='fullname'/>
                //     <attribute name='telephone1'/><attribute name='contactid'/><order descending='false' attribute='fullname'/>
                //     <filter type='and'>
                //           <condition attribute='emailaddress1' value='test@test.com' operator='eq'/>
                //           </filter></entity></fetch>";
                //fetchXml = String.Format(fetchXml, myemail);
                //EntityCollection contacts = svc.RetrieveMultiple(new FetchExpression(fetchXml));
                //Console.WriteLine("Total record: " + contacts.Entities.Count);
                //foreach (var con in contacts.Entities)
                //{
                //    Console.WriteLine("Id" + con.Id);
                //    if (con.Contains("fullname") && con["fullname"] != null)
                //    {

                //        Console.WriteLine("con Name: " + con["fullname"]);
                //    }
                //}

                //Entity retrievedContact = svc.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
                //Console.WriteLine("Record retrieved {0}", retrievedContact.Id.ToString());
                #endregion


            }
            else
            {
                Console.WriteLine("Failed to Established Connection!!!");
            }

            #region PREVIOUS CODE

            

            //IOrganizationService _service = null;

            //try
            //{
            //    ClientCredentials clientCredentials = new ClientCredentials();
            //    clientCredentials.UserName.UserName = "shahzaib@SHAHZAIBSAFDAR1.onmicrosoft.com";
            //    clientCredentials.UserName.Password = "safdar786ALI!";

            //    // Copy and Paste Organization Service Endpoint Address URL
            //    _service = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://org666f01ac.api.crm.dynamics.com/XRMServices/2011/Organization.svc"),
            //     null, clientCredentials, null);
            //    if (_service != null)
            //    {
            //        Guid userid = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
            //        if (userid != Guid.Empty)
            //        {
            //            Console.WriteLine("Connection Successful!...");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Failed to Established Connection!!!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception caught - " + ex.Message);
            //}
            #endregion
            Console.ReadKey();
#endregion

        }

        private static void F()
        {
            var myemail = "ranashahzaibulhassan@gmail.com";
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                    <entity name='contact'>
                   <attribute name='fullname' />
                     <attribute name='telephone1' />
                         <attribute name='contactid' />
                        <order attribute='fullname' descending='false' />
                              <filter type='and'>
                         <condition attribute='emailaddress1' operator='eq' value='{0}' />
                          </filter>
                                </entity>
                          </fetch>";
            fetchXml = String.Format(fetchXml, myemail);
            EntityCollection contacts = svc.RetrieveMultiple(new FetchExpression(fetchXml));
            Console.WriteLine("Total record: " + contacts.Entities.Count);
            foreach (var contact in contacts.Entities)
            {
                Console.WriteLine("Id" + contact.Id);
                if (contact.Contains("fullname") && contact["fullname"] != null)
                {
                    Console.WriteLine("Contact Name: " + contact["fullname"]);
                }
            }

            Console.ReadLine();
        }

        private static void D()
        {
           Entity contact = new Entity("contact");
            // Delete a record using Id
            Console.WriteLine("Enter Contact Id:");
            // Update record using Id, retrieve all attributes
            string stringGuid = Console.ReadLine();
            Guid contactId = Guid.Parse(stringGuid);
            
          
             svc.Delete(contact.LogicalName, contactId);
            Console.WriteLine("Deleted");
            Console.ReadLine();

        }

        private static void U()
        {
            //   Entity updatedContact = new Entity("contact");
            //Entity updatedContact = new Entity("contact");
            Console.WriteLine("Enter Contact Id:");

            string stringGuid = Console.ReadLine();
            Guid contactId = Guid.Parse(stringGuid);


            var cols = new ColumnSet(
          new String[] { "firstname", "lastname", "jobtitle", "emailaddress1" });
             updatedContact = svc.Retrieve("contact", contactId, cols);
            Console.WriteLine($"{updatedContact.Attributes["firstname"]}" + "\t" + $"{updatedContact.Attributes["lastname"]}" + "\t" + $"{updatedContact.Attributes["jobtitle"]}" + "\n" +
                $"{updatedContact.Attributes["emailaddress1"]}");
        
            Console.Write("please enter the value for updation");
            Console.ReadLine();

            Console.WriteLine("Enter value of firstname:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Enter value of lastname:");
            string lastname = Console.ReadLine();
            Console.WriteLine("Enter value of Jobtitle:");
            string Jobtitle = Console.ReadLine();
            Console.WriteLine("Enter emailaddress:");
             string email = Console.ReadLine();
            updatedContact["firstname"] = firstname;
            updatedContact["lastname"] = lastname;
            updatedContact["jobtitle"] = Jobtitle;
            updatedContact["emailaddress1"] = email;
            svc.Update(updatedContact);
            Console.WriteLine("Updated contact");
        }

        private static void RM()
        {
            QueryExpression qe = new QueryExpression("contact");
            qe.ColumnSet = new ColumnSet("firstname", "lastname", "jobtitle", "emailaddress1", "parentcustomerid");
            EntityCollection ec = svc.RetrieveMultiple(qe);
            for (int i = 0; i < ec.Entities.Count; i++)
            {
                if (ec.Entities[i].Attributes.ContainsKey("firstname"))
                {   
                    Console.WriteLine($"{ec.Entities[i].Attributes["firstname"]}" + "\t"+ $"{ec.Entities[i].Attributes["lastname"]}"+ "\t");
                    Console.WriteLine($"{ ec.Entities[i].Attributes["jobtitle"]} " + "\t"+ $"{ ec.Entities[i].Attributes["emailaddress1"]}");
                    Console.WriteLine($"{ec.Entities[i].GetAttributeValue<EntityReference>("parentcustomerid").Name.ToString()}");
                }
            }
        }

        private static void RS()
        {

            Console.WriteLine("Enter Contact Id:");

            string stringGuid = Console.ReadLine();
            Guid contactId = Guid.Parse(stringGuid);
            var cols = new ColumnSet(
          new String[] { "firstname", "lastname", "jobtitle", "emailaddress1" });
            updatedContact = svc.Retrieve("contact", contactId, cols);
            Console.WriteLine($"{updatedContact.Attributes["firstname"]}" + "\t" + $"{updatedContact.Attributes["lastname"]}" + "\t" + $"{updatedContact.Attributes["jobtitle"]}" + "\n" +
                $"{updatedContact.Attributes["emailaddress1"]}");

           
        }

        private static void C()
        {
            /// create new record

            Entity contact = new Entity("contact");
            Console.WriteLine("Enter username:");
           string userName = Console.ReadLine();
            Console.WriteLine("Enter lastname:");
           string lastname = Console.ReadLine();
            Console.WriteLine("Enter jobtitle:");
            string jobtitle = Console.ReadLine();
           Console.WriteLine("Enter emailaddress1:");
            string emailaddress1 = Console.ReadLine();
           contact["firstname"] = userName;
            contact["lastname"] = lastname;
            contact["jobtitle"] = jobtitle;
            contact["emailaddress1"] = emailaddress1;
            Guid contactId = svc.Create(contact);
            Console.WriteLine("New contact id: {0} .", contactId.ToString());
        }
    }
}
