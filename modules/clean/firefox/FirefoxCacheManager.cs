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
            try
            {
                // 默认获取第一个用户配置文件
                string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
                var directories = Directory.GetDirectories(profilesPath);
                return directories.Length > 0 ? directories[0] : null; // 返回第一个配置文件路径
            }
            catch (Exception)
            {
                return null; // 处理任何可能的异常
            }
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
                try
                {
                    string cachePath = Path.Combine(profilePath, "cache2", "entries");
                    if (Directory.Exists(cachePath))
                    {
                        var files = Directory.GetFiles(cachePath, "*.*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            try
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                totalSize += fileInfo.Length;
                                cacheFiles.Add(file);
                                fileCount++;
                            }
                            catch (Exception)
                            {
                                // 忽略单个文件的访问错误
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return ("0项[0B]", new List<string>());
                }
            }

            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";
            return (summary, cacheFiles);
        }

        // 清理 Firefox 缓存
        public static void CleanFirefoxCache(List<string> files)
        {
           Utils.deleteFileBatch(files);
        }




    }
}
