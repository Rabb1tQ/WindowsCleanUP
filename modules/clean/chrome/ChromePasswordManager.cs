﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WindowsCleanUP.modules.clean.Chrome
{
    internal class ChromePasswordManager
    {
        // 获取 Chrome 密码数据库路径
        private static string GetChromePasswordPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                               "Google", "Chrome", "User Data", "Default", "Login Data");
        }

        // 解密 Chrome 密码
        private static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                // Windows 的本地数据解密
                byte[] passwordBytes = Convert.FromBase64String(encryptedPassword);
                // 使用 DPAPI 解密
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(passwordBytes, null, DataProtectionScope.CurrentUser));
            }
            catch
            {
                return string.Empty;
            }
        }

        // 扫描 Chrome 保存的密码
        public static (string Summary, List<string> PasswordEntries) ScanChromePasswords()
        {
            int entryCount = 0;
            List<string> passwordEntries = new List<string>();
            string passwordPath = GetChromePasswordPath();

            if (File.Exists(passwordPath))
            {
                try
                {
                    using (var connection = new SqliteConnection($"Data Source={passwordPath};Version=3;"))
                    {
                        connection.Open();

                        string query = "SELECT origin_url, username_value, password_value FROM logins";
                        using (var command = new SqliteCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string url = reader.GetString(0);
                                string username = reader.GetString(1);
                                string encryptedPassword = reader.GetString(2);
                                string password = DecryptPassword(encryptedPassword);

                                if (!string.IsNullOrEmpty(password))
                                {
                                    passwordEntries.Add($"URL: {url}, Username: {username}, Password: {password}");
                                    entryCount++;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"扫描保存的密码时出错: {ex.Message}");
                }
            }

            string summary = $"{entryCount}项";
            return (summary, passwordEntries);
        }

        // 清理 Chrome 密码（清除 Login Data 文件）
        public static void CleanChromePasswords(List<string> files)
        {
            string passwordPath = GetChromePasswordPath();
            if (File.Exists(passwordPath))
            {
                try
                {
                    File.Delete(passwordPath); // 删除密码数据库文件
                    Console.WriteLine("保存的密码已清理。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法清理保存的密码: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("找不到密码数据库文件。");
            }
        }
    }
}