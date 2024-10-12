using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsCleanUP.modules.clean.edge
{
    internal class EdgeHistoryManager
    {
        // 获取 Edge 历史记录数据库路径
        private static string GetEdgeHistoryPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Microsoft", "Edge", "User Data", "Default", "History");
        }

        // 扫描 Edge 历史记录
        public static (string Summary, List<string> HistoryEntries) ScanEdgeHistory()
        {
            int entryCount = 0;
            List<string> historyEntries = new List<string>();
            string historyPath = GetEdgeHistoryPath();

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

        // 清理 Edge 历史记录
        public static void CleanEdgeHistory(List<string> files)
        {
            string historyPath = GetEdgeHistoryPath();
            if (File.Exists(historyPath))
            {
                try
                {
                    File.Delete(historyPath); // 删除历史记录数据库文件
                    Console.WriteLine("历史记录已清理。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法清理历史记录: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("找不到历史记录数据库文件。");
            }
        }
    }
}
