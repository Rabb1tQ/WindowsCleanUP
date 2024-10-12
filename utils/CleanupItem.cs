using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;

namespace WindowsCleanUP.utils
{
    public class CleanupItem
    {
        public HopeSwitch HopeSwitch { get; set; }
        public ForeverLabel ForeverLabel { get; set; }
        public Func<(string Summary, List<string> FileList)> ScanAction { get; set; }
        public Action<List<string>> CleanAction { get; set; }
        public List<string> ScanResult { get; set; }  // 保存文件列表
        public string Summary { get; set; }  // 保存统计信息

        public CleanupItem(HopeSwitch hopeSwitch, ForeverLabel foreverLabel,
                           Func<(string, List<string>)> scanAction,
                           Action<List<string>> cleanAction)
        {
            HopeSwitch = hopeSwitch;
            ForeverLabel = foreverLabel;
            ScanAction = scanAction;
            CleanAction = cleanAction;
        }
    }


}
