using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromeUpdateCacheManager
    {
        // 获取 Chrome 更新缓存路径
        private static string GetChromeUpdateCachePath()
        {
            try
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google", "Update");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // 扫描 Chrome 更新缓存
        public static (string Summary, List<string> CacheFiles) ScanChromeUpdateCache()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cacheFiles = new List<string>();
            string cachePath = GetChromeUpdateCachePath();

            if (Directory.Exists(cachePath))
            {
                try
                {
                    var files = Directory.GetFiles(cachePath, "*.*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        totalSize += fileInfo.Length;
                        cacheFiles.Add(file);
                        fileCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"扫描更新缓存时出错: {ex.Message}");
                    return ("0项[0B]", new List<string>());
                }
            }

            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";
            return (summary, cacheFiles);
        }

        // 清理 Chrome 更新缓存
        public static void CleanChromeUpdateCache(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}