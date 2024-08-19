using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Utils
{
    public class EmailUtils
    {
        public void SendMail(string subject, string body)
        {            
            try
            {
                AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
                var notifyEmail = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.NotifyEmail);
                var smtpClient = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SmtpClient);
                var networkCredentialUser = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.NetworkCredentialUser);
                var networkCredentialPassword = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.NetworkCredentialPassword);
                var appServer = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.AppServer);

                subject = string.Format("{0} - {1}", subject, appServer);

                SmtpClient SmtpServer = new SmtpClient(smtpClient);
                SmtpServer.Credentials = new NetworkCredential(networkCredentialUser, networkCredentialPassword);
                SmtpServer.Port = 25;

                using (var message = new MailMessage("alerts@amsi-corp.com", notifyEmail) { Subject = subject, Body = body })
                {
                    message.IsBodyHtml = false;
                    SmtpServer.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to send email; {0}", subject);
               // Console.WriteLine(ex.ToString());
            }
        }
    }
}
