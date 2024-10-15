using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WindowsCleanUP.utils;


namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxHistoryManager
    {
        private static readonly string FirefoxProfilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
        // 在应用程序启动时调用此方法，例如在 Main 方法中或者在应用程序启动的早期阶段
        // 扫描 Firefox 的历史记录
        // 扫描Firefox历史记录
        public static (string Summary, List<string> HistoryList) ScanFirefoxHistory()
        {
            int historyCount = 0;
            List<string> historyUrls = new List<string>();

            // 获取Firefox历史记录数据库的路径
            string profilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "AppData", "Roaming", "Mozilla", "Firefox", "Profiles");

            // 假设只有一个配置文件，实际情况可能需要处理多个配置文件
            string[] profileFolders = Directory.GetDirectories(profilePath);
            string historyDbPath = Path.Combine(profileFolders[0], "places.sqlite");
            historyDbPath= PatchWALDatabase(historyDbPath);
            string summary = "0项";
            try
            {

                // 使用Microsoft.Data.Sqlite创建连接
                using (var connection = new SqliteConnection($"Data Source={historyDbPath};Version=3;"))
                {
                    connection.Open();
                    SqliteCommand drop = new SqliteCommand("DROP TABLE IF EXISTS moz_previews_tombstones;", connection);
                    drop.ExecuteNonQuery();

                    // 执行查询历史记录的SQL语句
                    string sql = "SELECT url FROM moz_places WHERE visit_count > 0 ORDER BY last_visit_date DESC";
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                historyUrls.Add(reader.GetString(0));
                                historyCount++;
                            }
                        }
                    }
                }

                summary = $"{historyCount}项";

            }
            catch {

                Console.WriteLine("未找到Firefox历史记录");
            }


            return (summary, historyUrls);
        }

        public static string PatchWALDatabase(string tempDBFile)
        {
            // I couldn't figure out a safe way to open WAL enabled sqlite DBs (https://github.com/metacore/csharp-sqlite/issues/112)
            // So we'll "patch" temporary DB files we're reading to disable WAL journaling
            // Patch idea from here (https://stackoverflow.com/a/5476850)
            // WARNING - Don't use this patch on live/production sqlite DB files, always create temp duplicates first then patch the copy
            var offsets = new List<int> { 0x12, 0x13 };

            foreach (var n in offsets)

                using (var fs = new FileStream(tempDBFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    fs.Position = n;
                    fs.WriteByte(Convert.ToByte(0x1));
                }
            return tempDBFile;
        }



        // 清理 Firefox 的历史记录
        public static void CleanFirefoxHistory(List<string> files)
        {
            if (Directory.Exists(FirefoxProfilePath))
            {
                var profileDirs = Directory.GetDirectories(FirefoxProfilePath);
                foreach (var profileDir in profileDirs)
                {
                    string placesFilePath = Path.Combine(profileDir, "places.sqlite");
                    if (File.Exists(placesFilePath))
                    {
                        try
                        {
                            // 清理历史记录
                            using (var connection = new SqliteConnection($"Data Source={placesFilePath};"))
                            {
                                connection.Open();
                                string query = "DELETE FROM moz_places";
                                using (var command = new SqliteCommand(query, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }

                            Console.WriteLine("历史记录已清理。");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"清理历史记录时出错: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("未找到 Firefox 配置文件夹。");
            }
        }
    }
}