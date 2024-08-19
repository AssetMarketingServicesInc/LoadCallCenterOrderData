using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadCallCenterOrderData.Utils
{
    public class CstUtils
    {
        public void CstWorkaround()
        {
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
            EmailUtils emailUtils = new EmailUtils();

            bool isCSTWorkaroundActive = Convert.ToBoolean(appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.CSTWorkaroundActive));
            bool isTestEnvironment = Convert.ToBoolean(appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.IsTestEnvironment));
            string cstFolderLocation = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.CSTFolderLocation);
            string cstEmail = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.CSTEmail);

            if (!isTestEnvironment)
            {
                if (isCSTWorkaroundActive)
                {
                    CheckForExistingFiles(cstFolderLocation, cstEmail);
                }
            }
        }

        private void CheckForExistingFiles(string cstFolderLocation, string cstEmail)
        {
            EmailUtils emailUtils = new EmailUtils();
            List<string> existingFiles = new List<string>();
            if (Directory.Exists(cstFolderLocation))
            {
                string[] files = Directory.GetFiles(cstFolderLocation);

                foreach (var file in files)
                {
                    DateTime fileDate = File.GetCreationTime(file);
                    double totalDays = (DateTime.Today - fileDate).TotalDays;
                    if (totalDays >= 1)
                    {
                        existingFiles.Add(file);
                    }
                }
            }

            if(existingFiles.Count > 0)
            {
                string subject = "Existing Call Center Files Exists";
                string body = string.Empty;

                foreach(var existingFile in existingFiles)
                {
                    body = string.Format("{0}\n{1}", body, existingFile);
                }

                body = string.Format("Existing Call Center Files Exists in {0}\nFile Names: {1}", cstFolderLocation, body);

                emailUtils.SendMail(subject, body);
            }
        }
    }
}
