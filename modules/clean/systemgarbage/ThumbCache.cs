using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class ThumbCache
    {
        public static (string Summary, List<string> FileList) ScanThumbnailCache()
        {
            int fileCount = 0;
            long totalSize = 0;
            // 获取缩略图缓存的文件夹路径
            string thumbnailCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Windows\\Explorer");
            var files = Directory.GetFiles(thumbnailCachePath, "thumbcache_*.db", SearchOption.TopDirectoryOnly).ToList();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                totalSize += fileInfo.Length;
                fileCount++;
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, files);
        }

        // 清理缩略图缓存
        public static void CleanThumbnailCache(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
