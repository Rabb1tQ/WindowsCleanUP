using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class DownloadFolderManager
    {
        
        public static (string Summary, List<string> FileList) ScanDownloadFolder()
        {
            int fileCount = 0;
            long totalSize = 0;
            List<string> allFiles = new List<string>();

            // 获取下载文件夹的路径
            string downloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            downloadFolderPath = Utils.NormalizePath(downloadFolderPath);

            if (!Directory.Exists(downloadFolderPath))
            {
                return ("0项[0B]", new List<string>());
            }

            try
            {
                // 使用栈来手动遍历目录，避免递归导致的栈溢出
                var directories = new Stack<string>();
                directories.Push(downloadFolderPath);

                while (directories.Count > 0)
                {
                    string currentDir = directories.Pop();

                    try
                    {
                        // 获取当前目录中的所有文件
                        foreach (string file in Directory.GetFiles(currentDir))
                        {
                            try
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                totalSize += fileInfo.Length;
                                fileCount++;
                                allFiles.Add(file);
                            }
                            catch (Exception)
                            {
                                // 如果无法获取文件信息，仍然添加文件路径以便后续清理
                                allFiles.Add(file);
                                fileCount++;
                            }
                        }

                        // 将所有子目录添加到栈中
                        foreach (string dir in Directory.GetDirectories(currentDir))
                        {
                            directories.Push(dir);
                        }
                    }
                    catch (Exception)
                    {
                        // 如果无法访问某个目录，继续处理其他目录
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                // 如果发生任何错误，返回已收集的文件
            }

            // 转换为可读大小
            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, allFiles);
        }

        // 清理下载文件夹中的文件
        public static void CleanDownloadFolder(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }
    }
}
