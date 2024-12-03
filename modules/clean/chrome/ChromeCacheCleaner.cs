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
            try
            {
                string cachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                             "Google", "Chrome", "User Data", "Default", "Cache");
                return cachePath;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // 扫描 Chrome 缓存文件
        public static (string Summary, List<string> CacheFiles) ScanChromeCache()
        {
            try
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

                // 转换为可读大小
                string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
                string summary = $"{fileCount}项[{strTotalSize}]";

                return (summary, cacheFiles);
            }
            catch (Exception)
            {
                // 如果发生任何未预期的错误，返回空结果
                return ("0项[0B]", new List<string>());
            }
        }

        // 清理 Chrome 缓存文件
        public static void CleanChromeCache(List<string> cacheFiles)
        {
            Utils.deleteFileBatch(cacheFiles);
        }
    }
}
