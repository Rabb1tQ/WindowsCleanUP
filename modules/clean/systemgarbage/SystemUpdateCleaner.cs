using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class SystemUpdateCleaner
    {
        // 扫描系统中无用的更新文件
        public static (string Summary, List<string> FileList) ScanSystemUpdateFiles()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> updateFiles = new List<string>();

            // 常见的更新文件夹路径
            List<string> updateFolders = new List<string>
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "SoftwareDistribution\\Download"), // Windows 更新下载缓存
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "SoftwareDistribution\\DeliveryOptimization"), // Delivery Optimization
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Windows.old") // 旧版本 Windows 文件
        };

            // 遍历更新文件夹
            foreach (string folder in updateFolders)
            {
                if (Directory.Exists(folder))
                {
                    var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).ToList();

                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        totalSize += fileInfo.Length;
                        updateFiles.Add(file);
                        fileCount++;
                    }
                }
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, updateFiles);
        }

        // 清理无用的系统更新文件
        public static void CleanSystemUpdateFiles(List<string> updateFiles)
        {
            Utils.deleteFileBatch(updateFiles);
        }
    }
}
