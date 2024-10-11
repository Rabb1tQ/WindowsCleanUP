using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsCleanUP.modules.systemgarbage;
using WindowsCleanUP.utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsCleanUP
{
    public partial class Form1 : CrownForm
    {
        private float X;//当前窗体的宽度
        private float Y;//当前窗体的高度

        public Form1()
        {
            InitializeComponent();
            scanParrotFlatProgressBar.Value = 0;
            InitializeCheckBoxTags();
        }

        private void InitializeCheckBoxTags()
        {
            tempHopeSwitch.Text = "";
            tempHopeSwitch.Tag =new Action(() => TempFile.scanFile(tempHopeSwitch));

        }

        private void bigLabel1_Click(object sender, EventArgs e)
        {

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

        private void scanForeverButton_Click(object sender, EventArgs e)
        {
            int length = Utils.getSwitchLength(sender);
            if (length != 0) {
                scanParrotFlatProgressBar.MaxValue = length;
            }
            var tempFileInfo=TempFile.scanFile(tempHopeSwitch);
            int tempFileLength = tempFileInfo.FileCount;
            string totalSize= tempFileInfo.TotalSize;
            string tempFileAvaInfo= tempFileLength + "项" + "[" + totalSize + "]";
            //crownLabel1.Text = tempFileAvaInfo;
          
            Console.WriteLine(totalSize);
        }

        private void clearForeverButton_Click(object sender, EventArgs e)
        {
            scanParrotFlatProgressBar.Value += 1;
           // tempFileLabel.Text = "ssssssssss";
        }
    }
}
