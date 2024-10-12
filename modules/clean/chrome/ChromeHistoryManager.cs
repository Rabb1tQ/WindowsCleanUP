using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;


namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromeHistoryManager
    {
        // 获取 Chrome 历史记录数据库路径
        private static string GetChromeHistoryPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Google", "Chrome", "User Data", "Default", "History");
        }

        // 扫描 Chrome 历史记录
        public static (string Summary, List<string> HistoryEntries) ScanChromeHistory()
        {
            int entryCount = 0;
            List<string> historyEntries = new List<string>();
            string historyPath = GetChromeHistoryPath();

            if (File.Exists(historyPath))
            {
                try
                {
                    using (var connection = new SqliteConnection($"Data Source={historyPath};Version=3;"))
                    {
                        connection.Open();

                        string query = "SELECT url, title, visit_count FROM urls ORDER BY last_visit_time DESC";
                        using (var command = new SqliteCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string url = reader.GetString(0);
                                string title = reader.GetString(1);
                                int visitCount = reader.GetInt32(2);
                                historyEntries.Add($"URL: {url}, Title: {title}, Visit Count: {visitCount}");
                                entryCount++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"扫描历史记录时出错: {ex.Message}");
                }
            }

            string summary = $"{entryCount}项";
            return (summary, historyEntries);
        }

        // 清理 Chrome 历史记录
        public static void CleanChromeHistory(List<string> files)
        {
            string historyPath = GetChromeHistoryPath();
            if (File.Exists(historyPath))
            {
                try
                {
                    File.Delete(historyPath); // 删除历史记录数据库
                    Console.WriteLine("历史记录已清理。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法清理历史记录: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("找不到历史记录文件。");
            }
        }
    }
}
