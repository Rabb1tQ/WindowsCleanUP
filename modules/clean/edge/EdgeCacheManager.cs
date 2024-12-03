using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.edge
{
    internal class EdgeCacheManager
    {
        // 获取 Edge 缓存路径
        private static string GetEdgeCachePath()
        {
            try
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Microsoft", "Edge", "User Data", "Default", "Cache");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // 扫描 Edge 缓存
        public static (string Summary, List<string> CacheFiles) ScanEdgeCache()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> cacheFiles = new List<string>();
            string cachePath = GetEdgeCachePath();

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
                    Console.WriteLine($"扫描缓存时出错: {ex.Message}");
                    return ("0项[0B]", new List<string>());
                }
            }

            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";
            return (summary, cacheFiles);
        }

        // 清理 Edge 缓存
        public static void CleanEdgeCache(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}