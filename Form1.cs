using ReaLTaiizor.Controls;
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
    }
}
