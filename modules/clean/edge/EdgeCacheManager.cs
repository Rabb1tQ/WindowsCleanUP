using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsCleanUP.modules.clean.edge
{
    internal class EdgeCacheManager
    {
        // 获取 Edge 缓存路径
        private static string GetEdgeCachePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Microsoft", "Edge", "User Data", "Default", "Cache");
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
                }
            }

            string strTotalSize = totalSize == 0 ? "0B" : FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";
            return (summary, cacheFiles);
        }

        // 清理 Edge 缓存
        public static void CleanEdgeCache(List<string> files)
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
            Console.WriteLine("Edge 缓存已清理。");
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
