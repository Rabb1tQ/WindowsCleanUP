using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsCleanUP.modules.clean.firefox
{
    internal class FirefoxCookieManager
    {
        // 获取 Firefox Cookie 数据库路径
        private static string GetFirefoxProfilePath()
        {
            // 默认获取第一个用户配置文件
            string profilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles");
            var directories = Directory.GetDirectories(profilesPath);
            return directories.Length > 0 ? directories[0] : null; // 返回第一个配置文件路径
        }

        // 扫描 Firefox Cookies
        public static (string Summary, List<string> Cookies) ScanFirefoxCookies()
        {
            List<string> cookies = new List<string>();
            int cookieCount = 0;

            string profilePath = GetFirefoxProfilePath();
            if (profilePath != null)
            {
                string cookieDb = Path.Combine(profilePath, "cookies.sqlite");
                if (File.Exists(cookieDb))
                {
                    using (var connection = new SqliteConnection($"Data Source={cookieDb};"))
                    {
                        connection.Open();
                        using (var command = new SqliteCommand("SELECT name, value FROM moz_cookies;", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cookies.Add($"{reader.GetString(0)}: {reader.GetString(1)}");
                                    cookieCount++;
                                }
                            }
                        }
                    }
                }
            }

            string summary = $"{cookieCount}项";
            return (summary, cookies);
        }

        // 清理 Firefox Cookies
        public static void CleanFirefoxCookies(List<string> files)
        {
            string profilePath = GetFirefoxProfilePath();
            if (profilePath != null)
            {
                string cookieDb = Path.Combine(profilePath, "cookies.sqlite");
                if (File.Exists(cookieDb))
                {
                    try
                    {
                        File.Delete(cookieDb); // 删除 Cookie 数据库文件
                        Console.WriteLine("Firefox Cookies 已清理。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"无法清理 Cookies: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("找不到 Cookie 数据库文件。");
                }
            }
        }
    }
}