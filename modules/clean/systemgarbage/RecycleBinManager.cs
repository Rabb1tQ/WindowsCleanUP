using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class RecycleBinManager
    {
        // 扫描回收站文件
        public static (string Summary, List<string> FilePaths) ScanRecycleBin()
        {
            List<string> filePaths = new List<string>();
            long totalSize = 0;

            // 遍历所有逻辑驱动器
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                string recycleBinPath = Path.Combine(drive.Name, "$Recycle.Bin");

                if (Directory.Exists(recycleBinPath))
                {
                    // 遍历回收站中的所有用户文件夹
                    foreach (string userFolder in Directory.GetDirectories(recycleBinPath))
                    {
                        // 遍历用户文件夹中的所有文件
                        foreach (string file in Directory.GetFiles(userFolder, "*", SearchOption.AllDirectories))
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            totalSize += fileInfo.Length;
                            filePaths.Add(file);
                        }
                    }
                }
            }

            // 格式化总大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{filePaths.Count}项 [{strTotalSize}]";

            return (summary, filePaths);
        }

        // 清空回收站
        public static void CleanRecycleBin(List<string> files)
        {
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it)
                    Console.WriteLine($"Error deleting file: {file}. Error: {ex.Message}");
                }
            }
        }
    }
}