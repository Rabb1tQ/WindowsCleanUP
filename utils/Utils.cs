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
            if (selectAllButton != null&& selectAllButton.Parent != null)
            {
                processHopeSwitch(selectAllButton.Parent, isChecked);
            }
        }

        public static void processHopeSwitch(Control parentControl, Boolean isChecked) {
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

    }
}
