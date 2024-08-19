using LoadCallCenterOrderData.Modal;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Utils
{
    public class FileUtils
    {
        public FileUtils()
        {
            CleanupFiles();
        }

        public List<ImportOrderFile> ReadInputFile(List<string> downloadedFiles)
        {
            List<ImportOrderFile> importOrderItems = new List<ImportOrderFile>();
            EmailUtils emailUtils = new EmailUtils();

            try
            {
                foreach (var downloadedFile in downloadedFiles)
                {
                    TextFieldParser parser = new TextFieldParser(downloadedFile);

                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");
                    parser.TrimWhiteSpace = true;

                    string[] lineItem;

                    while (!parser.EndOfData)
                    {
                        lineItem = parser.ReadFields();
                        if (lineItem.Count() > 1)
                        {
                            ImportOrderFile importOrderItem = new ImportOrderFile();
                            importOrderItems.Add(importOrderItem);

                            importOrderItem.orderid = lineItem[0];
                            importOrderItem.userid = lineItem[1];
                            importOrderItem.membership = lineItem[2];
                            importOrderItem.total = lineItem[3];
                            importOrderItem.giftcert_discount = lineItem[4];
                            importOrderItem.giftcert_ids = lineItem[5];
                            importOrderItem.subtotal = lineItem[6];
                            importOrderItem.discount = lineItem[7];
                            importOrderItem.coupon = lineItem[8];
                            importOrderItem.coupon_discount = lineItem[9];
                            importOrderItem.shippingid = lineItem[10];
                            importOrderItem.shipping = lineItem[11];
                            importOrderItem.tracking = lineItem[12];
                            importOrderItem.shipping_cost = lineItem[13];
                            importOrderItem.taxes_applied = lineItem[14];
                            importOrderItem.date = GetEpochTime(lineItem[15]);
                            importOrderItem.order_date = GetDate(lineItem[15]);
                            importOrderItem.status = lineItem[16];
                            importOrderItem.payment_method = lineItem[17];
                            importOrderItem.flag = lineItem[18];
                            importOrderItem.notes = lineItem[19];
                            importOrderItem.details = lineItem[20];
                            importOrderItem.customer_notes = lineItem[21];
                            importOrderItem.customer = lineItem[22];
                            importOrderItem.title = lineItem[23];
                            importOrderItem.firstname = lineItem[24];
                            importOrderItem.lastname = lineItem[25];
                            importOrderItem.company = lineItem[26];
                            importOrderItem.b_title = lineItem[27];
                            importOrderItem.b_firstname = lineItem[28];
                            importOrderItem.b_lastname = lineItem[29];
                            importOrderItem.b_address = lineItem[30];
                            importOrderItem.b_city = lineItem[31];
                            importOrderItem.b_county = lineItem[32];
                            importOrderItem.b_state = lineItem[33];
                            importOrderItem.b_country = lineItem[34];
                            importOrderItem.b_zipcode = lineItem[35];
                            importOrderItem.b_zip4 = lineItem[36];
                            importOrderItem.b_phone = lineItem[37];
                            importOrderItem.b_fax = lineItem[38];
                            importOrderItem.s_title = lineItem[39];
                            importOrderItem.s_firstname = lineItem[40];
                            importOrderItem.s_lastname = lineItem[41];
                            importOrderItem.s_address = lineItem[42];
                            importOrderItem.s_city = lineItem[43];
                            importOrderItem.s_county = lineItem[44];
                            importOrderItem.s_state = lineItem[45];
                            importOrderItem.s_country = lineItem[46];
                            importOrderItem.s_zipcode = lineItem[47];
                            importOrderItem.s_phone = lineItem[48];
                            importOrderItem.s_fax = lineItem[49];
                            importOrderItem.s_zip4 = lineItem[50];
                            importOrderItem.CC_type = lineItem[51];
                            importOrderItem.CC_card_no = lineItem[52];
                            importOrderItem.CC_exp_year = lineItem[53];
                            importOrderItem.CC_exp_month = lineItem[54];
                            importOrderItem.url = lineItem[55];
                            importOrderItem.email = lineItem[56];
                            importOrderItem.language = lineItem[57];
                            importOrderItem.clickid = lineItem[58];
                            importOrderItem.extra = lineItem[59];
                            importOrderItem.membershipid = lineItem[60];
                            importOrderItem.paymentid = lineItem[61];
                            importOrderItem.payment_surcharge = lineItem[62];
                            importOrderItem.tax_number = lineItem[63];
                            importOrderItem.init_total = lineItem[64];
                            importOrderItem.access_key = lineItem[65];
                            importOrderItem.deposit_required = lineItem[66];
                            importOrderItem.deposit_paid = lineItem[67];
                            importOrderItem.go2mcm = lineItem[68];
                            importOrderItem.problem = lineItem[69];
                            importOrderItem.wcm_fraud_flag = lineItem[70];
                            importOrderItem.fraud_score = lineItem[71];
                            importOrderItem.referral_campaign = lineItem[72];
                            importOrderItem.ams_code = lineItem[73];
                            importOrderItem.print_time = lineItem[74];
                            importOrderItem.verification_hold = lineItem[75];
                            importOrderItem.release_time = GetEpochTime(lineItem[76]);
                            importOrderItem.itemid = lineItem[77];
                            importOrderItem.product = lineItem[78];
                            importOrderItem.amount = lineItem[79];
                            importOrderItem.price = lineItem[80];
                        }
                    }

                    parser.Close();
                }
            }
            catch (Exception ex)
            {
                string subject = "Error in Application LoadCallCenterOrderData.Utls.ReadInputFile";
                string body = string.Format("There was an error in the application: {0}", ex.ToString());

                emailUtils.SendMail(subject, body);
            }

            //List<ImportOrderFile> importOrderItems = new List<ImportOrderFile>();
            //EmailUtils emailUtils = new EmailUtils();            
            //try
            //{
            //    foreach (var downloadedFile in downloadedFiles)
            //    {
            //        string fileName = downloadedFile;
            //        var contents = File.ReadAllText(fileName).Split('\n');

            //        var lineItems = from line in contents
            //                        select line.Replace('\r', ' ').Trim().Split(',').ToList();

            //        foreach (var lineItem in lineItems)
            //        {
            //            if (lineItem.Count > 1)
            //            {
            //                ImportOrderFile importOrderItem = new ImportOrderFile();
            //                importOrderItems.Add(importOrderItem);

            //                importOrderItem.orderid = lineItem[0];
            //                importOrderItem.userid = lineItem[1];
            //                importOrderItem.membership = lineItem[2];
            //                importOrderItem.total = lineItem[3];
            //                importOrderItem.giftcert_discount = lineItem[4];
            //                importOrderItem.giftcert_ids = lineItem[5];
            //                importOrderItem.subtotal = lineItem[6];
            //                importOrderItem.discount = lineItem[7];
            //                importOrderItem.coupon = lineItem[8];
            //                importOrderItem.coupon_discount = lineItem[9];
            //                importOrderItem.shippingid = lineItem[10];
            //                importOrderItem.shipping = lineItem[11];
            //                importOrderItem.tracking = lineItem[12];
            //                importOrderItem.shipping_cost = lineItem[13];
            //                importOrderItem.taxes_applied = lineItem[14];
            //                importOrderItem.date = GetEpochTime(lineItem[15]);
            //                importOrderItem.order_date = GetDate(lineItem[15]);
            //                importOrderItem.status = lineItem[16];
            //                importOrderItem.payment_method = lineItem[17];
            //                importOrderItem.flag = lineItem[18];
            //                importOrderItem.notes = lineItem[19];
            //                importOrderItem.details = lineItem[20];
            //                importOrderItem.customer_notes = lineItem[21];
            //                importOrderItem.customer = lineItem[22];
            //                importOrderItem.title = lineItem[23];
            //                importOrderItem.firstname = lineItem[24];
            //                importOrderItem.lastname = lineItem[25];
            //                importOrderItem.company = lineItem[26];
            //                importOrderItem.b_title = lineItem[27];
            //                importOrderItem.b_firstname = lineItem[28];
            //                importOrderItem.b_lastname = lineItem[29];
            //                importOrderItem.b_address = lineItem[30];
            //                importOrderItem.b_city = lineItem[31];
            //                importOrderItem.b_county = lineItem[32];
            //                importOrderItem.b_state = lineItem[33];
            //                importOrderItem.b_country = lineItem[34];
            //                importOrderItem.b_zipcode = lineItem[35];
            //                importOrderItem.b_zip4 = lineItem[36];
            //                importOrderItem.b_phone = lineItem[37];
            //                importOrderItem.b_fax = lineItem[38];
            //                importOrderItem.s_title = lineItem[39];
            //                importOrderItem.s_firstname = lineItem[40];
            //                importOrderItem.s_lastname = lineItem[41];
            //                importOrderItem.s_address = lineItem[42];
            //                importOrderItem.s_city = lineItem[43];
            //                importOrderItem.s_county = lineItem[44];
            //                importOrderItem.s_state = lineItem[45];
            //                importOrderItem.s_country = lineItem[46];
            //                importOrderItem.s_zipcode = lineItem[47];
            //                importOrderItem.s_phone = lineItem[48];
            //                importOrderItem.s_fax = lineItem[49];
            //                importOrderItem.s_zip4 = lineItem[50];
            //                importOrderItem.CC_type = lineItem[51];
            //                importOrderItem.CC_card_no = lineItem[52];
            //                importOrderItem.CC_exp_year = lineItem[53];
            //                importOrderItem.CC_exp_month = lineItem[54];
            //                importOrderItem.url = lineItem[55];
            //                importOrderItem.email = lineItem[56];
            //                importOrderItem.language = lineItem[57];
            //                importOrderItem.clickid = lineItem[58];
            //                importOrderItem.extra = lineItem[59];
            //                importOrderItem.membershipid = lineItem[60];
            //                importOrderItem.paymentid = lineItem[61];
            //                importOrderItem.payment_surcharge = lineItem[62];
            //                importOrderItem.tax_number = lineItem[63];
            //                importOrderItem.init_total = lineItem[64];
            //                importOrderItem.access_key = lineItem[65];
            //                importOrderItem.deposit_required = lineItem[66];
            //                importOrderItem.deposit_paid = lineItem[67];
            //                importOrderItem.go2mcm = lineItem[68];
            //                importOrderItem.problem = lineItem[69];
            //                importOrderItem.wcm_fraud_flag = lineItem[70];
            //                importOrderItem.fraud_score = lineItem[71];
            //                importOrderItem.referral_campaign = lineItem[72];
            //                importOrderItem.ams_code = lineItem[73];
            //                importOrderItem.print_time = lineItem[74];
            //                importOrderItem.verification_hold = lineItem[75];
            //                importOrderItem.release_time = GetEpochTime(lineItem[76]);
            //                importOrderItem.itemid = lineItem[77];
            //                importOrderItem.product = lineItem[78];
            //                importOrderItem.amount = lineItem[79];
            //                importOrderItem.price = lineItem[80];
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string subject = "Error in Application LoadCallCenterOrderData.Utls.ReadInputFile";
            //    string body = string.Format("There was an error in the application: {0}", ex.ToString());

            //    emailUtils.SendMail(subject, body);
            //}

            return importOrderItems;
        }

        private DateTime GetDate(string date)
        {
            DateTime dateTime = DateTime.Now;

            try
            {
                dateTime = DateTime.Parse(date);
            }
            catch (Exception)
            {
                dateTime = DateTime.Now;
            }

            return dateTime;
        }

        private int GetEpochTime(string dateTime)
        {
            try
            {
                int unixTimestamp = (int)DateTime.Parse(dateTime).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                return unixTimestamp;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private void CleanupFiles()
        {
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
            EmailUtils emailUtils = new EmailUtils();

            string saveFileLocation = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SaveFileLocation);
            int archiveDays = Convert.ToInt32(appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.FileHoldNumberOfDays));

            try
            {
                if (Directory.Exists(saveFileLocation))
                {
                    string[] files = Directory.GetFiles(saveFileLocation);

                    foreach (var file in files)
                    {
                        DateTime fileDate = File.GetCreationTime(file);
                        double totalDays = (DateTime.Today - fileDate).TotalDays;
                        if (totalDays >= archiveDays)
                        {
                            Console.WriteLine("Deleteing File: {0}", file);
                            File.Delete(file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                string subject = "Error in Application LoadCallCenterOrderData.Utls.CleanupFiles";
                string body = string.Format("There was an error in the application: {0}", ex.ToString());

                emailUtils.SendMail(subject, body);
            }
        }

        public void EncryptFile(List<string> downloadedFiles)
        {
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
            string fileEncryptionKey = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.FileEncryptionKey);

            foreach (var downloadedFile in downloadedFiles)
            {
                EncryptionFile enc = new EncryptionFile();
                enc.EncryptFile(downloadedFile, fileEncryptionKey);                
            }
        }

        public void DecryptFile(string fileName)
        {
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
            string fileEncryptionKey = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.FileEncryptionKey);
            string saveFileLocation = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SaveFileLocation);
            DecryptionFile dnc = new DecryptionFile();
            string tempFileName = string.Format("{0}temp_{1}", saveFileLocation, fileName);
            fileName = string.Format("{0}{1}", saveFileLocation, fileName);            
            dnc.DecryptFile(tempFileName, fileName, fileEncryptionKey);
            Console.WriteLine("File has been decrypted: {0}", tempFileName);
        }
    }
}
