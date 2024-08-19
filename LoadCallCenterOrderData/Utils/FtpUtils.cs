using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace LoadCallCenterOrderData.Utils
{
    public class FtpUtils
    {
        public List<string> DownloadFileFTP()
        {
            AppSettinsUtils appSettinsUtils = new AppSettinsUtils();
            List<string> downloadedFiles = new List<string>();
            EmailUtils emailUtils = new EmailUtils();

            try
            {
                string saveFilePath = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SaveFileLocation);
                bool deleteFtpFile = Convert.ToBoolean(appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.DeleteFtpFile));

                string host = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SFTPHost);

                int port = 22;
                
                string username = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SFTPUserName);
                string password = appSettinsUtils.GetAppSetting(AppSettinsUtils.AppSetting.SFTPUserPassword);
                List<SftpFile> ftpFiles = new List<SftpFile>();

                using (var sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    ftpFiles = sftp.ListDirectory("/Coure").ToList();
                    foreach (var ftpFile in ftpFiles)
                    {
                        if (!ftpFile.IsDirectory)
                        {
                            string inputfilepath = string.Format("{0}{1}", saveFilePath, ftpFile.Name);
                            string remoteFileName = string.Format("/Coure/{0}", ftpFile.Name);
                            using (var file = File.OpenWrite(inputfilepath))
                            {
                                downloadedFiles.Add(inputfilepath);
                                sftp.DownloadFile(remoteFileName, file);

                                if (deleteFtpFile)
                                {
                                    sftp.DeleteFile(remoteFileName);
                                }

                                Console.WriteLine(inputfilepath);
                            }
                        }
                    }

                    sftp.Disconnect();

                    Console.WriteLine("Download Complete");
                }
            }
            catch (Exception ex)
            {
                string subject = "Error in Application LoadCallCenterOrderData.Utils.DownloadFileFTP";
                string body = string.Format("There was an error in the application: {0}", ex.ToString());

                emailUtils.SendMail(subject, body);                
            }

            return downloadedFiles;
        }
    }
}
