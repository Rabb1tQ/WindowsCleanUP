using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

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
            string historyPath = Utils.CreateTmpFile(GetEdgeHistoryPath());

            if (File.Exists(historyPath))
            {
                try
                {
                    using (var connection = new SqliteConnection(String.Format("Version=3,uri=file://{0}", historyPath)))
                    {
                        connection.Open();
                        SqliteCommand drop = new SqliteCommand("DROP TABLE if EXISTS cluster_visit_duplicates;DROP TABLE if EXISTS clusters_and_visits;", connection);
                        drop.ExecuteNonQuery();
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
                    return ("0项", new List<string>());
                }
            }

            string summary = $"{entryCount}项";
            return (summary, historyEntries);
        }

        // 清理 Edge 历史记录
        public static void CleanEdgeHistory(List<string> files)
        {
            string historyPath = GetEdgeHistoryPath();
            Utils.deleteFile(historyPath);

        }
    }
}
