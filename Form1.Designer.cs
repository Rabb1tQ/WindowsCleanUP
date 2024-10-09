namespace WindowsCleanUP
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.parrotForm1 = new ReaLTaiizor.Forms.ParrotForm();
            this.tabPage1 = new ReaLTaiizor.Controls.TabPage();
            this.CleanTab = new System.Windows.Forms.TabPage();
            this.SystemControlTab = new System.Windows.Forms.TabPage();
            this.parrotFlatProgressBar1 = new ReaLTaiizor.Controls.ParrotFlatProgressBar();
            this.hopeSwitch1 = new ReaLTaiizor.Controls.HopeSwitch();
            this.thunderLabel1 = new ReaLTaiizor.Controls.ThunderLabel();
            this.thunderLabel2 = new ReaLTaiizor.Controls.ThunderLabel();
            this.parrotForm1.WorkingArea.SuspendLayout();
            this.parrotForm1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.CleanTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // parrotForm1
            // 
            this.parrotForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.ExitApplication = true;
            this.parrotForm1.FormStyle = ReaLTaiizor.Forms.ParrotForm.Style.MacOS;
            this.parrotForm1.Location = new System.Drawing.Point(0, 0);
            this.parrotForm1.MacOSForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.parrotForm1.MacOSLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.parrotForm1.MacOSRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.parrotForm1.MacOSSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            this.parrotForm1.MaterialBackColor = System.Drawing.Color.DodgerBlue;
            this.parrotForm1.MaterialForeColor = System.Drawing.Color.White;
            this.parrotForm1.Name = "parrotForm1";
            this.parrotForm1.ShowMaximize = true;
            this.parrotForm1.ShowMinimize = true;
            this.parrotForm1.Size = new System.Drawing.Size(1135, 684);
            this.parrotForm1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotForm1.TabIndex = 0;
            this.parrotForm1.TitleText = "Windows清理工具";
            this.parrotForm1.UbuntuForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(210)))));
            this.parrotForm1.UbuntuLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(85)))), ((int)(((byte)(80)))));
            this.parrotForm1.UbuntuRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            // 
            // parrotForm1.WorkingArea
            // 
            this.parrotForm1.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.WorkingArea.Controls.Add(this.tabPage1);
            this.parrotForm1.WorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.WorkingArea.Location = new System.Drawing.Point(0, 39);
            this.parrotForm1.WorkingArea.Name = "WorkingArea";
            this.parrotForm1.WorkingArea.Size = new System.Drawing.Size(1135, 645);
            this.parrotForm1.WorkingArea.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.ActiveForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage1.ActiveLineTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(169)))), ((int)(((byte)(222)))));
            this.tabPage1.ActiveTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            this.tabPage1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPage1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.tabPage1.CompositingType = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            this.tabPage1.ControlBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(64)))));
            this.tabPage1.Controls.Add(this.CleanTab);
            this.tabPage1.Controls.Add(this.SystemControlTab);
            this.tabPage1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabPage1.Font = new System.Drawing.Font("宋体", 15F);
            this.tabPage1.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(50)))), ((int)(((byte)(63)))));
            this.tabPage1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.tabPage1.ItemSize = new System.Drawing.Size(44, 135);
            this.tabPage1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.tabPage1.LineTabColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(64)))));
            this.tabPage1.Location = new System.Drawing.Point(0, 1);
            this.tabPage1.Multiline = true;
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.NormalForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(162)))), ((int)(((byte)(167)))));
            this.tabPage1.PageColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(63)))), ((int)(((byte)(74)))));
            this.tabPage1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.tabPage1.SelectedIndex = 0;
            this.tabPage1.Size = new System.Drawing.Size(1135, 644);
            this.tabPage1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPage1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tabPage1.StringType = System.Drawing.StringAlignment.Near;
            this.tabPage1.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(64)))));
            this.tabPage1.TabIndex = 0;
            this.tabPage1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // CleanTab
            // 
            this.CleanTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(63)))), ((int)(((byte)(74)))));
            this.CleanTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CleanTab.Controls.Add(this.thunderLabel2);
            this.CleanTab.Controls.Add(this.thunderLabel1);
            this.CleanTab.Controls.Add(this.hopeSwitch1);
            this.CleanTab.Controls.Add(this.parrotFlatProgressBar1);
            this.CleanTab.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CleanTab.Location = new System.Drawing.Point(139, 4);
            this.CleanTab.Name = "CleanTab";
            this.CleanTab.Padding = new System.Windows.Forms.Padding(3);
            this.CleanTab.Size = new System.Drawing.Size(992, 636);
            this.CleanTab.TabIndex = 0;
            this.CleanTab.Text = "清理";
            // 
            // SystemControlTab
            // 
            this.SystemControlTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(63)))), ((int)(((byte)(74)))));
            this.SystemControlTab.Location = new System.Drawing.Point(139, 4);
            this.SystemControlTab.Name = "SystemControlTab";
            this.SystemControlTab.Padding = new System.Windows.Forms.Padding(3);
            this.SystemControlTab.Size = new System.Drawing.Size(992, 636);
            this.SystemControlTab.TabIndex = 1;
            this.SystemControlTab.Text = "系统控制";
            // 
            // parrotFlatProgressBar1
            // 
            this.parrotFlatProgressBar1.BarStyle = ReaLTaiizor.Controls.ParrotFlatProgressBar.Style.Material;
            this.parrotFlatProgressBar1.BorderColor = System.Drawing.Color.Black;
            this.parrotFlatProgressBar1.Colors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("parrotFlatProgressBar1.Colors")));
            this.parrotFlatProgressBar1.CompleteBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(250)))));
            this.parrotFlatProgressBar1.CompleteColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(119)))), ((int)(((byte)(215)))));
            this.parrotFlatProgressBar1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.parrotFlatProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.parrotFlatProgressBar1.IncompletedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.parrotFlatProgressBar1.InocmpletedColor = System.Drawing.Color.White;
            this.parrotFlatProgressBar1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.parrotFlatProgressBar1.Location = new System.Drawing.Point(3, 611);
            this.parrotFlatProgressBar1.MaxValue = 100;
            this.parrotFlatProgressBar1.Name = "parrotFlatProgressBar1";
            this.parrotFlatProgressBar1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotFlatProgressBar1.Positions = ((System.Collections.Generic.List<float>)(resources.GetObject("parrotFlatProgressBar1.Positions")));
            this.parrotFlatProgressBar1.ShowBorder = true;
            this.parrotFlatProgressBar1.Size = new System.Drawing.Size(986, 22);
            this.parrotFlatProgressBar1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.parrotFlatProgressBar1.TabIndex = 8;
            this.parrotFlatProgressBar1.Text = "parrotFlatProgressBar1";
            this.parrotFlatProgressBar1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.parrotFlatProgressBar1.Value = 50;
            // 
            // hopeSwitch1
            // 
            this.hopeSwitch1.AutoSize = true;
            this.hopeSwitch1.BaseColor = System.Drawing.Color.White;
            this.hopeSwitch1.BaseOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.hopeSwitch1.BaseOnColor = System.Drawing.Color.MediumSeaGreen;
            this.hopeSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hopeSwitch1.Location = new System.Drawing.Point(109, 24);
            this.hopeSwitch1.Name = "hopeSwitch1";
            this.hopeSwitch1.Size = new System.Drawing.Size(40, 20);
            this.hopeSwitch1.TabIndex = 10;
            this.hopeSwitch1.Text = "hopeSwitch1";
            this.hopeSwitch1.UseVisualStyleBackColor = true;
            // 
            // thunderLabel1
            // 
            this.thunderLabel1.BackColor = System.Drawing.Color.Transparent;
            this.thunderLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.thunderLabel1.Location = new System.Drawing.Point(7, 28);
            this.thunderLabel1.Name = "thunderLabel1";
            this.thunderLabel1.Size = new System.Drawing.Size(96, 16);
            this.thunderLabel1.TabIndex = 11;
            this.thunderLabel1.Text = "thunderLabel1";
            // 
            // thunderLabel2
            // 
            this.thunderLabel2.BackColor = System.Drawing.Color.Transparent;
            this.thunderLabel2.ForeColor = System.Drawing.Color.IndianRed;
            this.thunderLabel2.Location = new System.Drawing.Point(156, 28);
            this.thunderLabel2.Name = "thunderLabel2";
            this.thunderLabel2.Size = new System.Drawing.Size(96, 16);
            this.thunderLabel2.TabIndex = 12;
            this.thunderLabel2.Text = "thunderLabel2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 684);
            this.Controls.Add(this.parrotForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.parrotForm1.WorkingArea.ResumeLayout(false);
            this.parrotForm1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.CleanTab.ResumeLayout(false);
            this.CleanTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.ParrotForm parrotForm1;
        private ReaLTaiizor.Controls.TabPage tabPage1;
        private System.Windows.Forms.TabPage CleanTab;
        private System.Windows.Forms.TabPage SystemControlTab;
        private ReaLTaiizor.Controls.ParrotFlatProgressBar parrotFlatProgressBar1;
        private ReaLTaiizor.Controls.HopeSwitch hopeSwitch1;
        private ReaLTaiizor.Controls.ThunderLabel thunderLabel1;
        private ReaLTaiizor.Controls.ThunderLabel thunderLabel2;
    }
}

