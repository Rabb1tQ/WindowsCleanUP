using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxCookieManager
    {
        private static readonly string FirefoxProfilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");

        // 扫描Firefox的cookie文件
        public static (string Summary, List<string> CookieFiles) ScanFirefoxCookies()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cookieFiles = new List<string>();

            // 查找Firefox配置文件夹
            if (Directory.Exists(FirefoxProfilePath))
            {
                var profileDirs = Directory.GetDirectories(FirefoxProfilePath);
                foreach (var profileDir in profileDirs)
                {
                    string cookiesFilePath = Path.Combine(profileDir, "cookies.sqlite");
                    if (File.Exists(cookiesFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(cookiesFilePath);
                        cookieFiles.Add(cookiesFilePath);
                        totalSize += fileInfo.Length;
                        fileCount++;
                    }
                }
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, cookieFiles);
        }

        // 清理Firefox的cookie文件
        public static void CleanFirefoxCookies(List<string> cookieFiles)
        {
            Utils.deleteFileBatch(cookieFiles);
        }
    }
}