using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class SystemLogManager
    {
        // 扫描系统日志文件
        public static (string Summary, List<string> FileList) ScanSystemLogs()
        {
            int fileCount = 0;
            long totalSize = 0;
            // 系统日志文件夹路径
            string logFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32\\winevt\\Logs");
            var files = Directory.Exists(logFolderPath)
                        ? Directory.GetFiles(logFolderPath, "*.evtx", SearchOption.TopDirectoryOnly).ToList()
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

        // 清理系统日志文件
        public static void CleanSystemLogs(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
