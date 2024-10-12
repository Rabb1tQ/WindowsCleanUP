using System.Collections.Generic;
using System.IO;
using System.Linq;
using WindowsCleanUP.utils;

namespace WindowsCleanUP.modules.systemgarbage
{
    internal class TempFile
    {
        public static (string Summary, List<string> FileList) scanFile()
        {
            int fileCount = 0;
            long totalSize = 0;
            string tempFolderPath = Path.GetTempPath();
            var files = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                totalSize += fileInfo.Length;
                fileCount++;
            }

            string strTotalSize = totalSize == 0 ? "0B" : Utils.FormatBytesToHumanReadable(totalSize);
            string summary = $"{fileCount}项[{strTotalSize}]";

            return (summary, files);
        }

        public static void cleanUP(List<string> files)
        {
            Utils.deleteFileBatch(files);
        }

    }
}
