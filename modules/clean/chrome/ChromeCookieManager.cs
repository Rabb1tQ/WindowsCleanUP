using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromeCookieManager
    {
        // 获取 Chrome Cookie 数据库路径
        private static string GetChromeCookiePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Google", "Chrome", "User Data", "Default", "Cookies");
        }

        // 扫描 Chrome Cookies
        public static (string Summary, List<string> CookieEntries) ScanChromeCookies()
        {
            int entryCount = 0;
            List<string> cookieEntries = new List<string>();
            string cookiePath = GetChromeCookiePath();

            if (File.Exists(cookiePath))
            {
                try
                {
                    using (var connection = new SqliteConnection($"Data Source={cookiePath};Version=3;"))
                    {
                        connection.Open();

                        string query = "SELECT host_key, name, value, expires_utc FROM cookies";
                        using (var command = new SqliteCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string hostKey = reader.GetString(0);
                                string cookieName = reader.GetString(1);
                                string cookieValue = reader.GetString(2);
                                long expiresUtc = reader.GetInt64(3);

                                cookieEntries.Add($"Domain: {hostKey}, Name: {cookieName}, Value: {cookieValue}, Expires: {DateTimeOffset.FromUnixTimeMilliseconds(expiresUtc).UtcDateTime}");
                                entryCount++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"扫描 Cookies 时出错: {ex.Message}");
                }
            }

            string summary = $"{entryCount}项";
            return (summary, cookieEntries);
        }

        // 清理 Chrome Cookies（清除 Cookies 数据库文件）
        public static void CleanChromeCookies(List<string> files)
        {
            string cookiePath = GetChromeCookiePath();
            if (File.Exists(cookiePath))
            {
                try
                {
                    File.Delete(cookiePath); // 删除 Cookies 数据库文件
                    Console.WriteLine("Cookies 已清理。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法清理 Cookies: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("找不到 Cookies 数据库文件。");
            }
        }
    }
}
