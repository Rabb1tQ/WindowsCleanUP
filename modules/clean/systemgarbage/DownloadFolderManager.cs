using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class DownloadFolderManager
    {
        // 扫描下载文件夹中的文件
        public static (string Summary, List<string> FileList) ScanDownloadFolder()
        {
            int fileCount = 0;
            long totalSize = 0;
            // 获取下载文件夹的路径
            string downloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var files = Directory.Exists(downloadFolderPath)
                        ? Directory.GetFiles(downloadFolderPath, "*.*", SearchOption.AllDirectories).ToList()
                        : new List<string>();

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

        // 清理下载文件夹中的文件
        public static void CleanDownloadFolder(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
