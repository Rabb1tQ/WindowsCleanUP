using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.clean.systemgarbage
{
    internal class RecycleBinManager
    {
        // 调用Windows API 获取回收站信息
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        // 结构体，用于获取回收站信息
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SHQUERYRBINFO
        {
            public uint cbSize;
            public long i64Size;   // 回收站中文件总大小（以字节为单位）
            public long i64NumItems; // 回收站中项目的数量
        }

        [Flags]
        public enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,   // 不确认删除
            SHERB_NOPROGRESSUI = 0x00000002,     // 不显示进度UI
            SHERB_NOSOUND = 0x00000004           // 不播放声音
        }

        // 扫描回收站文件
        public static (string Summary, List<string> FileCount) ScanRecycleBin()
        {
            SHQUERYRBINFO queryInfo = new SHQUERYRBINFO();
            queryInfo.cbSize = (uint)Marshal.SizeOf(typeof(SHQUERYRBINFO));

            // 获取回收站信息
            int result = SHQueryRecycleBin(null, ref queryInfo);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }

            // 格式化总大小
            string strTotalSize = queryInfo.i64Size == 0 ? "0B" : Utils.FormatBytesToHumanReadable(queryInfo.i64Size);
            string summary = $"{queryInfo.i64NumItems}项[{strTotalSize}]";

            return (summary, new List<string>());
        }

        // 清空回收站
        public static void CleanRecycleBin(List<string> files)
        {
            int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }
        }
    }
}
