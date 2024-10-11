using ReaLTaiizor.Controls;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsCleanUP.utils
{
    internal class Utils
    {
        public static void operateAllButton_Click(object sender, Boolean isChecked)
        {
            // 获取发送事件的按钮
            ForeverButton selectAllButton = sender as ForeverButton;
            if (selectAllButton != null && selectAllButton.Parent != null)
            {
                processHopeSwitch(selectAllButton.Parent, isChecked);
            }
        }

        public static void processHopeSwitch(Control parentControl, Boolean isChecked)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is HopeSwitch)
                {
                    ((HopeSwitch)control).Checked = isChecked;
                }
                else if (control is ParrotGroupBox || control is ForeverGroupBox)
                {
                    processHopeSwitch(control, isChecked);
                }
            }
        }

        public static int getSwitchLength(object sender)
        {
            ForeverButton selectAllButton = sender as ForeverButton;
            return switchLength(selectAllButton.Parent);

        }

        public static int switchLength(Control parentControl)
        {
            int checkBoxCount = 0;

            foreach (Control control in parentControl.Controls)
            {
                // 如果找到的是 CheckBox，则计数加一
                if (control is HopeSwitch&& ((HopeSwitch)control).Checked==true)
                {
                    checkBoxCount++;
                }
                // 如果找到的是 GroupBox 或自定义的 GroupBox，则递归遍历
                else if (control is ForeverGroupBox || control is ParrotGroupBox)
                {
                    checkBoxCount += switchLength(control);
                }
            }

            return checkBoxCount;
        }

        public static string FormatBytesToHumanReadable(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double formattedSize = bytes;
            int order = 0;
            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize /= 1024;
            }

            // Return the formatted file size string
            return $"{formattedSize:0.##}{sizes[order]}";
        }
    }
}
