using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class InvalidShortcutManager
    {
        // 扫描系统中无效的快捷方式
        public static (string Summary, List<string> InvalidShortcuts) ScanInvalidShortcuts()
        {
            int invalidShortcutCount = 0;
            long totalSize = 0;
            List<string> invalidShortcuts = new List<string>();

            // 常见的快捷方式文件夹路径
            List<string> shortcutFolders = new List<string>
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)), // 用户桌面
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)), // 公共桌面
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)), // 用户开始菜单
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu)), // 公共开始菜单
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar") // 任务栏
        };

            // 遍历快捷方式文件夹
            foreach (string folder in shortcutFolders)
            {
                if (Directory.Exists(folder))
                {
                    try
                    {
                        var shortcutFiles = Directory.GetFiles(folder, "*.lnk", SearchOption.AllDirectories).ToList();

                        foreach (string shortcut in shortcutFiles)
                        {
                            // 检查快捷方式是否有效
                            if (!IsShortcutValid(shortcut))
                            {
                                FileInfo fileInfo = new FileInfo(shortcut);
                                totalSize += fileInfo.Length;
                                invalidShortcuts.Add(shortcut);
                                invalidShortcutCount++;
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"访问被拒绝: {folder}. 错误信息: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"处理文件夹 {folder} 时出错: {ex.Message}");
                    }
                }
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{invalidShortcutCount}项[{strTotalSize}]";

            return (summary, invalidShortcuts);
        }

        // 清理无效的快捷方式
        public static void CleanInvalidShortcuts(List<string> invalidShortcuts)
        {
            Utils.deleteFileBatch(invalidShortcuts);
        }

        // 检查快捷方式是否有效
        private static bool IsShortcutValid(string shortcutPath)
        {
            try
            {
                // 创建 WScript.Shell 对象
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                // 检查目标文件是否存在
                return File.Exists(shortcut.TargetPath) || Directory.Exists(shortcut.TargetPath);
            }
            catch
            {
                // 如果无法解析快捷方式，视为无效
                return false;
            }
        }
    }
}
