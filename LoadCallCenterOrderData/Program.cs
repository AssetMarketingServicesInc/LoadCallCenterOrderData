using LoadCallCenterOrderData.Modal;
using LoadCallCenterOrderData.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LoadCallCenterOrderData
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUtils fileUtils = new FileUtils();
            OrderUtils orderUtils = new OrderUtils();
            DBUtils dbUtils = new DBUtils();
            FtpUtils ftpUtils = new FtpUtils();
            EmailUtils emailUtils = new EmailUtils();         


            var inputArg = string.Empty;
            
            if (args.Count() > 0)
            {
                inputArg = args.FirstOrDefault().Trim();                
            }

            if (string.IsNullOrEmpty(inputArg))
            {
                List<string> downloadedFiles = ftpUtils.DownloadFileFTP();

                if (downloadedFiles.Count > 0)
                {
                    
                    List<ImportOrderFile> importOrderItems = fileUtils.ReadInputFile(downloadedFiles);

                    if (importOrderItems.Count > 1)
                    {
                        List<Order> orders = orderUtils.ProcessFile(importOrderItems);

                        dbUtils.InsertData(orders);

                        //using (StreamWriter file = File.CreateText(@"orders.json"))
                        //{
                        //    JsonSerializer serializer = new JsonSerializer();
                        //    //serialize object directly into file stream
                        //    serializer.Serialize(file, orders);
                        //}

                        // email here saying 
                        string subject = string.Format("{0} New Call Center File(s) to Process", downloadedFiles.Count);
                        string body = string.Format("A new call center file {0} has been downloaded and ready to process\n\nNumber of Orders: {1}", downloadedFiles.FirstOrDefault(), orders.Count);

                        emailUtils.SendMail(subject, body);
                    }

                    fileUtils.EncryptFile(downloadedFiles);
                }
                else
                {
                    // no file for today in email

                    string subject = "No Call Center File Was Found Today";
                    string body = "No Call Center File Was Found Today";

                    emailUtils.SendMail(subject, body);
                }

                dbUtils.CleanCCData();
            }
            else if(inputArg.ToLower() == "decrypt")
            {
                var fileName = args[1];

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileUtils.DecryptFile(fileName);
                    string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    emailUtils.SendMail("LoadCallCenterOrderData file has been Decrypted", string.Format("File: {0} has been decrypted by {1}", fileName, userName));
                }
                else
                {
                    Console.WriteLine("No File Name was specified on command line");
                }
            }
        }
    }
}
