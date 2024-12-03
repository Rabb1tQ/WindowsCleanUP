
using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WindowsCleanUP.utils;


namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromeHistoryManager
    {
        // 获取 Chrome 历史记录数据库路径
        private static string GetChromeHistoryPath()
        {
            try
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Google", "Chrome", "User Data", "Default", "History");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // 扫描 Chrome 历史记录
        public static (string Summary, List<string> HistoryEntries) ScanChromeHistory()
        {
            int entryCount = 0;
            List<string> historyEntries = new List<string>();
<<<<<<< HEAD

=======
            
>>>>>>> c3562df48416b0ebe8134276931f43acf1e6f74b
            string historyPath = Utils.CreateTmpFile(GetChromeHistoryPath());

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
                                try
                                {
                                    string url = reader.GetString(0);
                                    string title = reader.GetString(1);
                                    int visitCount = reader.GetInt32(2);
                                    historyEntries.Add($"URL: {url}, Title: {title}, Visit Count: {visitCount}");
                                    entryCount++;
                                }
                                catch (Exception)
                                {
                                    // 忽略单条记录读取错误
                                    continue;
                                }
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

        // 清理 Chrome 历史记录
        public static void CleanChromeHistory(List<string> files)
        {
            string historyPath = GetChromeHistoryPath();
            Utils.deleteFile(historyPath);
        }
    }
}
