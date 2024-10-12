using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class RegistryCleaner
    {
        // 扫描无用的注册表项
        public static (string Summary, List<string> InvalidRegistryKeys) ScanInvalidRegistryKeys()
        {
            List<string> invalidKeys = new List<string>();
            int invalidKeyCount = 0;

            // 扫描与程序卸载有关的注册表项（通常位于 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall）
            string uninstallRegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey uninstallKey = Registry.LocalMachine.OpenSubKey(uninstallRegistryPath))
            {
                if (uninstallKey != null)
                {
                    foreach (string subKeyName in uninstallKey.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = uninstallKey.OpenSubKey(subKeyName))
                        {
                            // 检查 DisplayName 和 InstallLocation
                            if (subKey != null)
                            {
                                string displayName = subKey.GetValue("DisplayName") as string;
                                string installLocation = subKey.GetValue("InstallLocation") as string;

                                if (string.IsNullOrEmpty(displayName) || !System.IO.Directory.Exists(installLocation))
                                {
                                    invalidKeys.Add($"{uninstallRegistryPath}\\{subKeyName}");
                                    invalidKeyCount++;
                                }
                            }
                        }
                    }
                }
            }

            string summary = $"{invalidKeyCount}项";
            return (summary, invalidKeys);
        }

        // 清理无用的注册表项
        public static void CleanInvalidRegistryKeys(List<string> invalidRegistryKeys)
        {
            foreach (string keyPath in invalidRegistryKeys)
            {
                try
                {
                    Registry.LocalMachine.DeleteSubKeyTree(keyPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法删除注册表项 {keyPath}: {ex.Message}");
                }
            }
        }
    }
}
