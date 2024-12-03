using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxCookieManager
    {
        private static string GetFirefoxProfilePath()
        {
            try
            {
                // 默认获取第一个用户配置文件
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
            }
            catch (Exception)
            {
                return null; // 处理任何可能的异常
            }
        }

        // 扫描Firefox的cookie文件
        public static (string Summary, List<string> CookieFiles) ScanFirefoxCookies()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cookieFiles = new List<string>();

            // 查找Firefox配置文件夹
            if (Directory.Exists(GetFirefoxProfilePath()))
            {
                try
                {
                    var profileDirs = Directory.GetDirectories(GetFirefoxProfilePath());
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
                catch (Exception)
                {
                    // 如果无法访问配置文件夹，返回空结果
                    return ("0项", new List<string>());
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