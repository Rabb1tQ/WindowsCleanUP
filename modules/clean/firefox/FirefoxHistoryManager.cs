using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxHistoryManager
    {
        // 获取 Firefox 历史记录数据库路径
        private static string GetFirefoxProfilePath()
        {
            // 默认获取第一个用户配置文件
            string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
            var directories = Directory.GetDirectories(profilesPath);
            return directories.Length > 0 ? directories[0] : null; // 返回第一个配置文件路径
        }

        // 扫描 Firefox 历史记录
        public static (string Summary, List<string> HistoryRecords) ScanFirefoxHistory()
        {
            List<string> historyRecords = new List<string>();
            int recordCount = 0;

            string profilePath = GetFirefoxProfilePath();
            if (profilePath != null)
            {
                string historyDb = Path.Combine(profilePath, "places.sqlite");
                if (File.Exists(historyDb))
                {
                    using (var connection = new SqliteConnection($"Data Source={historyDb};"))
                    {
                        connection.Open();
                        using (var command = new SqliteCommand("SELECT url FROM moz_places;", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    historyRecords.Add(reader.GetString(0));
                                    recordCount++;
                                }
                            }
                        }
                    }
                }
            }

            string summary = $"{recordCount} 条历史记录";
            return (summary, historyRecords);
        }

        // 清理 Firefox 历史记录
        public static void CleanFirefoxHistory(List<string> files)
        {
            string profilePath = GetFirefoxProfilePath();
            if (profilePath != null)
            {
                string historyDb = Path.Combine(profilePath, "places.sqlite");
                if (File.Exists(historyDb))
                {
                    try
                    {
                        File.Delete(historyDb); // 删除历史记录数据库文件
                        Console.WriteLine("Firefox 历史记录已清理。");
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
}
