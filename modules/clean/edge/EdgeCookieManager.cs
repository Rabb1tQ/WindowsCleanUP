using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
using System.IO;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.edge
{
    internal class EdgeCookieManager
    {
        // 获取 Edge Cookie 数据库路径
        private static string GetEdgeCookiePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Microsoft", "Edge", "User Data", "Default", "Network", "Cookies");
        }

        // 扫描 Edge Cookies
        public static (string Summary, List<string> CookieEntries) ScanEdgeCookies()
        {
            int entryCount = 0;
            List<string> cookieEntries = new List<string>();
            string cookiePath = GetEdgeCookiePath();

            if (File.Exists(cookiePath))
            {
                try
                {
                    using (var connection = new SqliteConnection(String.Format("Version=3,uri=file://{0}", cookiePath)))
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

                                //cookieEntries.Add($"Domain: {hostKey}");
                                entryCount++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"扫描 Cookies 时出错: {ex.Message}");
                    return ("0项", new List<string>());
                }
            }

            string summary = $"{entryCount}项";
            return (summary, cookieEntries);
        }

        // 清理 Edge Cookies（清除 Cookies 数据库文件）
        public static void CleanEdgeCookies(List<string> files)
        {
            string cookiePath = GetEdgeCookiePath();
            Utils.deleteFile(cookiePath);
        }
    }
}
