using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromeCacheCleaner
    {
        // 获取 Chrome 缓存目录
        private static string GetChromeCachePath()
        {
            string cachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                             "Google", "Chrome", "User Data", "Default", "Cache");
            return cachePath;
        }

        // 扫描 Chrome 缓存文件
        public static (string Summary, List<string> CacheFiles) ScanChromeCache()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cacheFiles = new List<string>();
            string cachePath = GetChromeCachePath();

            if (Directory.Exists(cachePath))
            {
                var files = Directory.GetFiles(cachePath, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    totalSize += fileInfo.Length;
                    cacheFiles.Add(file);
                    fileCount++;
                }
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, cacheFiles);
        }

        // 清理 Chrome 缓存文件
        public static void CleanChromeCache(List<string> cacheFiles)
        {
            Utils.deleteFileBatch(cacheFiles);
        }
    }
}
