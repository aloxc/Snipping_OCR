namespace Snipping_OCR
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            notifyIcon = new NotifyIcon(components);
            iconMenu = new ContextMenuStrip(components);
            开始截图ToolStripMenuItem = new ToolStripMenuItem();
            识别剪贴板ToolStripMenuItem = new ToolStripMenuItem();
            退出软件ToolStripMenuItem = new ToolStripMenuItem();
            iconMenu.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = iconMenu;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "截图 OCR";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            // 
            // iconMenu
            // 
            iconMenu.ImageScalingSize = new Size(24, 24);
            iconMenu.Items.AddRange(new ToolStripItem[] { 开始截图ToolStripMenuItem, 识别剪贴板ToolStripMenuItem, 退出软件ToolStripMenuItem });
            iconMenu.Name = "iconMenu";
            iconMenu.Size = new Size(171, 124);

            // 
            // 开始截图ToolStripMenuItem
            // 
            开始截图ToolStripMenuItem.Name = "开始截图ToolStripMenuItem";
            开始截图ToolStripMenuItem.Size = new Size(170, 30);
            开始截图ToolStripMenuItem.Text = "开始截图";
            开始截图ToolStripMenuItem.Click += 开始截图ToolStripMenuItem_Click;
            // 
            // 识别剪贴板ToolStripMenuItem
            // 
            识别剪贴板ToolStripMenuItem.Name = "识别剪贴板ToolStripMenuItem";
            识别剪贴板ToolStripMenuItem.Size = new Size(170, 30);
            识别剪贴板ToolStripMenuItem.Text = "识别剪贴板";
            识别剪贴板ToolStripMenuItem.Click += 识别剪贴板ToolStripMenuItem_Click;
            // 
            // 退出软件ToolStripMenuItem
            // 
            退出软件ToolStripMenuItem.Name = "退出软件ToolStripMenuItem";
            退出软件ToolStripMenuItem.Size = new Size(170, 30);
            退出软件ToolStripMenuItem.Text = "退出软件";
            退出软件ToolStripMenuItem.Click += 退出软件ToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(180, 0);
            Margin = new Padding(5, 4, 5, 4);
            Name = "Main";
            ShowInTaskbar = false;
            Text = "Snipping OCR";
            TopMost = true;
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            SizeChanged += Main_SizeChanged;
            iconMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon;
        private ContextMenuStrip iconMenu;
        private ToolStripMenuItem 识别剪贴板ToolStripMenuItem;
        private ToolStripMenuItem 退出软件ToolStripMenuItem;
        private ToolStripMenuItem 开始截图ToolStripMenuItem;
    }
}