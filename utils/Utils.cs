using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WindowsCleanUP.utils
{
    internal class Utils
    {
        public static void operateAllButton_Click(object sender, Boolean isChecked)
        {
            // 获取发送事件的按钮
            ForeverButton selectAllButton = sender as ForeverButton;
            if (selectAllButton != null && selectAllButton.Parent != null)
            {
                processHopeSwitch(selectAllButton.Parent, isChecked);
            }
        }

        public static void processHopeSwitch(Control parentControl, Boolean isChecked)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is HopeSwitch)
                {
                    ((HopeSwitch)control).Checked = isChecked;
                }
                else if (control is ParrotGroupBox || control is ForeverGroupBox)
                {
                    processHopeSwitch(control, isChecked);
                }
            }
        }

        public static int getSwitchLength(object sender)
        {
            ForeverButton selectAllButton = sender as ForeverButton;
            return switchLength(selectAllButton.Parent);

        }

        public static int switchLength(Control parentControl)
        {
            int checkBoxCount = 0;

            foreach (Control control in parentControl.Controls)
            {
                // 如果找到的是 CheckBox，则计数加一
                if (control is HopeSwitch && ((HopeSwitch)control).Checked == true)
                {
                    checkBoxCount++;
                }
                // 如果找到的是 GroupBox 或自定义的 GroupBox，则递归遍历
                else if (control is ForeverGroupBox || control is ParrotGroupBox)
                {
                    checkBoxCount += switchLength(control);
                }
            }

            return checkBoxCount;
        }

        public static string FormatBytesToHumanReadable(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double formattedSize = bytes;
            int order = 0;
            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize /= 1024;
            }

            // Return the formatted file size string
            return $"{formattedSize:0.##}{sizes[order]}";
        }

        public static void deleteFileBatch(List<string> files)
        {
            foreach (string file in files)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception)
                {
                    // 如果删除失败，尝试使用长路径
                    try
                    {
                        string longPath = Utils.NormalizePath(file);
                        if (File.Exists(longPath))
                        {
                            File.Delete(longPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"无法删除文件: {file}. 错误: {ex.Message}");
                        // 如果仍然无法删除，跳过该文件
                        continue;
                    }
                }
            }
        }

        public static void deleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath); // 删除历史记录数据库
                    Console.WriteLine(filePath+"已清理。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法清理文件：{filePath} {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("找不到要删除的文件："+filePath);
            }
        }
        
        public static string CreateTmpFile(string dbfile)
        {
            try
            {
                string tempFile = Path.GetTempFileName();
                File.Copy(dbfile, tempFile, true);
                return tempFile;
            }
            catch
            {
                return null;
            }
        }
        

        // 扫描下载文件夹中的文件
        public static string NormalizePath(string path)
        {
            // 添加长路径前缀 "\\?\" 来支持超过260个字符的路径
            if (!path.StartsWith(@"\\?\"))
            {
                path = @"\\?\" + path;
            }
            return path;
        }
        
    }
}
