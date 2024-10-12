using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class ErrorReportManager
    {
        // 扫描错误报告文件
        public static (string Summary, List<string> FileList) ScanErrorReports()
        {
            int fileCount = 0;
            long totalSize = 0;
            // 错误报告文件夹路径
            string errorReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\Windows\\WER\\ReportQueue");
            var files = Directory.GetFiles(errorReportPath, "*.*", SearchOption.AllDirectories).ToList();

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

        // 清理错误报告文件
        public static void CleanErrorReports(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
