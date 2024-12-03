using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    public static class RecentFileForeverLabel
    {
        public static (string Summary, List<string>) ScanRecentDocuments()
        {
            string recentDocsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            string[] lnkFiles = Directory.GetFiles(recentDocsFolderPath, "*.lnk");
            int fileCount = 0;
            long totalSize = 0;
            List<string> shortcutFiles = new List<string>(); // 存储快捷方式文件路径

            foreach (var lnkFile in lnkFiles)
            {
                string targetPath = GetShortcutTargetFile(lnkFile);

                FileInfo fileInfo = new FileInfo(lnkFile);
                totalSize += fileInfo.Length;
                fileCount++;
                shortcutFiles.Add(lnkFile); // 添加快捷方式文件到列表
            }

            string strTotalSize = FormatFileSize(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, shortcutFiles); // 返回统计信息和快捷方式文件列表
        }

        private static string GetShortcutTargetFile(string shortcutPath)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            return shortcut.TargetPath;
        }

        private static string FormatFileSize(long fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double formattedSize = fileSize;
            int order = 0;
            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize /= 1024;
            }

            return string.Format("{0:0.##} {1}", formattedSize, sizes[order]);
        }

        public static void ClearRecentDocuments(List<string> shortcutFiles)
        {
            // 清除“最近的文档”快捷方式
            Utils.deleteFileBatch(shortcutFiles);

            // 清除注册表中“最近的文档”的条目
            ClearRecentDocsRegistry();
        }

        private static void ClearRecentDocsRegistry()
        {
            using (RegistryKey recentDocsKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs", true))
            {
                if (recentDocsKey != null)
                {
                    try
                    {
                        // 清除“最近的文档”子键中的所有值
                        recentDocsKey.DeleteSubKeyTree("*");
                        recentDocsKey.DeleteSubKeyTree("MRUListEx");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"无法清除注册表项. 错误: {ex.Message}");
                    }
                }
            }
        }
    }
}
