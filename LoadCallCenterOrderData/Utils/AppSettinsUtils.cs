using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadCallCenterOrderData.Data;

namespace LoadCallCenterOrderData.Utils
{
    public class AppSettinsUtils
    {
        public string GetAppSetting(AppSetting appSetting)
        {
            string appSettingValue = string.Empty;

            AMSI_AppsEntities entities = new AMSI_AppsEntities();

            var setting = entities.tSystemParameterValues.Where(r => r.GroupId == "CallCenterApp" && r.ParameterName == appSetting.ToString()).FirstOrDefault();

            if(setting != null)
            {
                appSettingValue = setting.ParameterValue;
            }

            return appSettingValue;
        }

        public enum AppSetting
        {
            NotifyEmail,
            SmtpClient,
            NetworkCredentialUser,
            NetworkCredentialPassword,
            AppServer,
            SaveFileLocation,
            SFTPHost,
            SFTPUserName,
            SFTPUserPassword,
            FileEncryptionKey,
            FileHoldNumberOfDays,            
            IsOrderProcessed,
            DeleteFtpFile
        }
    }
}
