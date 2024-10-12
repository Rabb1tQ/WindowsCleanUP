using ReaLTaiizor.Forms;
using System;
using System.Drawing;
using System.Threading.Tasks;
using WindowsCleanUP.modules.clean.Chrome;
using WindowsCleanUP.modules.clean.edge;
using WindowsCleanUP.modules.clean.firefox;
using WindowsCleanUP.modules.clean.systemgarbage;
using WindowsCleanUP.modules.systemgarbage;
using WindowsCleanUP.utils;

namespace WindowsCleanUP
{
    public partial class Form1 : CrownForm
    {
        private CleanupItem[] cleanupItems;

        public Form1()
        {
            InitializeComponent();
            scanParrotFlatProgressBar.Value = 0;
            InitializeCleanupItems();
        }

        private void InitializeCleanupItems()
        {
            cleanupItems = new CleanupItem[]
            {
                //系统垃圾
                new CleanupItem(
                    tempHopeSwitch, tempFileForeverLabel,
                    () => TempFile.scanFile(),
                    (files) => TempFile.cleanUP(files)),
                new CleanupItem(
                    recentFileHopeSwitch, recentFileForeverLabel,
                    () => RecentFileForeverLabel.ScanRecentDocuments(),
                    (files) => RecentFileForeverLabel.ClearRecentDocuments(files)),
                new CleanupItem(
                    thumbCacheHopeSwitch, thumbCacheForeverLabel,
                    () => ThumbCache.ScanThumbnailCache(),
                    (files) => ThumbCache.CleanThumbnailCache(files)),
                new CleanupItem(
                    errorReportHopeSwitch, errorReportForeverLabel,
                    () => ErrorReportManager.ScanErrorReports(),
                    (files) => ErrorReportManager.CleanErrorReports(files)),
                new CleanupItem(
                    systemLogHopeSwitch, systemLogForeverLabel,
                    () => SystemLogManager.ScanSystemLogs(),
                    (files) => SystemLogManager.CleanSystemLogs(files)),
                new CleanupItem(
                    memoryHopeSwitch, memoryForeverLabel,
                    () => MemoryDumpManager.ScanMemoryDumps(),
                    (files) => ErrorReportManager.CleanErrorReports(files)),
                new CleanupItem(
                    recycleHopeSwitch, recycleForeverLabel,
                    () => RecycleBinManager.ScanRecycleBin(),
                    (files) => RecycleBinManager.CleanRecycleBin(files)),
                new CleanupItem(
                    downloadHopeSwitch, downloadForeverLabel,
                    () => DownloadFolderManager.ScanDownloadFolder(),
                    (files) => DownloadFolderManager.CleanDownloadFolder(files)),
                new CleanupItem(
                    InvalidShortcutHopeSwitch, shortcutForeverLabel,
                    () => InvalidShortcutManager.ScanInvalidShortcuts(),
                    (files) => InvalidShortcutManager.CleanInvalidShortcuts(files)),
                new CleanupItem(
                    systemUpdateHopeSwitch, systemUpdateForeverLabel,
                    () => SystemUpdateCleaner.ScanSystemUpdateFiles(),
                    (files) => SystemUpdateCleaner.CleanSystemUpdateFiles(files)),

                //Chrome
                new CleanupItem(
                    chromeCacheHopeSwitch, chromeCacheForeverLabel,
                    () => ChromeCacheCleaner.ScanChromeCache(),
                    (files) => ChromeCacheCleaner.CleanChromeCache(files)),
                new CleanupItem(
                    ChromeHistoryHopeSwitch, chromeHilstoryForeverLabel,
                    () => ChromeHistoryManager.ScanChromeHistory(),
                    (files) => ChromeHistoryManager.CleanChromeHistory(files)),
                new CleanupItem(
                    chromePasswordHopeSwitch, chromePasswordForeverLabel,
                    () => ChromePasswordManager.ScanChromePasswords(),
                    (files) => ChromePasswordManager.CleanChromePasswords(files)),
                new CleanupItem(
                    chromeCookieHopeSwitch, chromeCookieForeverLabel,
                    () => ChromeCookieManager.ScanChromeCookies(),
                    (files) => ChromeCookieManager.CleanChromeCookies(files)),
                new CleanupItem(
                    chromeUpdateCacheHopeSwitch, chromeUpdateCacheForeverLabel,
                    () => ChromeUpdateCacheManager.ScanChromeUpdateCache(),
                    (files) => ChromeUpdateCacheManager.CleanChromeUpdateCache(files)),
                
                
                
                //Edge
                new CleanupItem(
                    edgeCacheHopeSwitch, edgeCacheForeverLabel,
                    () =>EdgeCacheManager.ScanEdgeCache(),
                    (files) => EdgeCacheManager.CleanEdgeCache(files)),
                new CleanupItem(
                    edgeHistoryHopeSwitch, edgeHistoryforeverLabel,
                    () =>EdgeHistoryManager.ScanEdgeHistory() ,
                    (files) => EdgeHistoryManager.CleanEdgeHistory(files)),
                new CleanupItem(
                    edgePasswordHopeSwitch, edgePasswordForeverLabel,
                    () => EdgePasswordManager.ScanEdgePasswords(),
                    (files) => EdgePasswordManager.CleanEdgePasswords(files)),
                new CleanupItem(
                    edgeCookieHopeSwitch, edgeCookieForeverLabel,
                    () => EdgeCookieManager.ScanEdgeCookies(),
                    (files) => EdgeCookieManager.CleanEdgeCookies(files)),
                new CleanupItem(
                    edgeUpdateCacheHopeSwitch, edgeUpdateCacheForeverLabel,
                    () => EdgeUpdateCacheManager.ScanEdgeUpdateCache(),
                    (files) => EdgeUpdateCacheManager.CleanEdgeUpdateCache(files)),

                 //FireFox
                new CleanupItem(
                    firefoxCacheHopeSwitch, firefoxCacheForeverLabel,
                    () =>FirefoxCacheManager.ScanFirefoxCache(),
                    (files) => FirefoxCacheManager.CleanFirefoxCache(files)),
                new CleanupItem(
                    firefoxCookieHopeSwitch, firefoxCookieForeverLabel,
                    () =>FirefoxCookieManager.ScanFirefoxCookies() ,
                    (files) =>FirefoxCookieManager.CleanFirefoxCookies(files)),
                new CleanupItem(
                    firefoxHistoryHopeSwitch, firefoxHistoryForeverLabel,
                    () => FirefoxHistoryManager.ScanFirefoxHistory(),
                    (files) => FirefoxHistoryManager.CleanFirefoxHistory(files)),

            };
        }


        private void systemSelectAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, true);
        }

        private void systemRemoveAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, false);
        }

        private void browserSelectAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, true);
        }

        private void browserRemoveAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, false);
        }

        private void selectAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, true);
        }

        private void removeAllForeverButton_Click(object sender, EventArgs e)
        {
            Utils.operateAllButton_Click(sender, false);
        }

        private async void scanForeverButton_Click(object sender, EventArgs e)
        {
            int length = Utils.getSwitchLength(sender);
            if (length != 0)
            {
                scanParrotFlatProgressBar.MaxValue = length;
            }

            foreach (var item in cleanupItems)
            {
                if (item.HopeSwitch.Checked == true)
                {
                    var (result, fileList) = await Task.Run(item.ScanAction);
                    item.ScanResult = fileList;  // 保存文件列表
                    item.Summary = result;  // 保存统计信息
                    item.ForeverLabel.ForeColor = Color.Tomato;
                    item.ForeverLabel.Text = result;  // 显示统计信息
                }
            }

        }

        private void clearForeverButton_Click(object sender, EventArgs e)
        {
            foreach (var item in cleanupItems)
            {
                if (item.HopeSwitch.Checked == true)
                {
                    item.CleanAction(item.ScanResult);  // 传递文件列表
                    item.ForeverLabel.Text = "Cleaned";
                    item.ForeverLabel.ForeColor = Color.MediumSeaGreen;
                    scanParrotFlatProgressBar.Value += 1;
                }
            }

            // tempFileLabel.Text = "ssssssssss";
        }
    }
}
