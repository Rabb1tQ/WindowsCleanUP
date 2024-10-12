using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class MemoryDumpManager
    {
        // 扫描内存转储文件
        public static (string Summary, List<string> FileList) ScanMemoryDumps()
        {
            int fileCount = 0;
            long totalSize = 0;
            // 内存转储文件夹路径
            string dumpFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Minidump");
            var files = Directory.Exists(dumpFolderPath)
                        ? Directory.GetFiles(dumpFolderPath, "*.dmp", SearchOption.TopDirectoryOnly).ToList()
                        : new List<string>();

            // 检查大容量的系统内存转储文件 MEMORY.DMP
            string fullMemoryDumpFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "MEMORY.DMP");
            if (File.Exists(fullMemoryDumpFile))
            {
                files.Add(fullMemoryDumpFile);
            }

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

        // 清理内存转储文件
        public static void CleanMemoryDumps(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
