using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxCacheManager
    {
        // 获取 Firefox 缓存路径
        private static string GetFirefoxProfilePath()
        {
            // 默认获取第一个用户配置文件
            string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
            var directories = Directory.GetDirectories(profilesPath);
            return directories.Length > 0 ? directories[0] : null; // 返回第一个配置文件路径
        }

        // 扫描 Firefox 缓存
        public static (string Summary, List<string> CacheFiles) ScanFirefoxCache()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cacheFiles = new List<string>();

            string profilePath = GetFirefoxProfilePath();
            if (profilePath != null)
            {
                string cachePath = Path.Combine(profilePath, "cache2", "entries");
                if (Directory.Exists(cachePath))
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
            }

            string strTotalSize = totalSize == 0 ? "0B" : FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount} 个缓存文件 [{strTotalSize}]";
            return (summary, cacheFiles);
        }

        // 清理 Firefox 缓存
        public static void CleanFirefoxCache(List<string> files)
        {
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file); // 删除文件
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法删除缓存文件 {file}: {ex.Message}");
                }
            }
            Console.WriteLine("Firefox 缓存已清理。");
        }

        // 格式化字节为可读形式
        private static string FormatBytesToHumanReadable(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            int i;
            for (i = 0; i < suffixes.Length && bytes >= 1024; i++)
            {
                bytes /= 1024;
            }
            return $"{bytes} {suffixes[i]}";
        }



    }
}
