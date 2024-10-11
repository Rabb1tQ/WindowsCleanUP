using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.systemgarbage
{
    internal class TempFile
    {
        public static (int FileCount, string TotalSize) scanFile(object sender) {
            int fileCount = 0;
            long totalSize = 0;
            string tempFolderPath = Path.GetTempPath();
            // 获取指定目录下的所有文件和文件夹
            string[] files = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                // 获取文件信息
                FileInfo fileInfo = new FileInfo(file);

                // 累计文件大小
                totalSize += fileInfo.Length;

                // 累计文件数量
                fileCount++;
            }
            string strTotalSize;
            if (totalSize == 0)
            {
                strTotalSize = "0B";
            }
            else {
                strTotalSize = Utils.FormatBytesToHumanReadable(totalSize);
            }
            return (fileCount, strTotalSize);
        }
    }
}
