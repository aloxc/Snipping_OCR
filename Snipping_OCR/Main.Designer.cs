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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.识别剪贴板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconMenu.SuspendLayout();
            this.SuspendLayout();
          
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.iconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "截图 OCR";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.开始截图ToolStripMenuItem,
            this.识别剪贴板ToolStripMenuItem,
            this.退出软件ToolStripMenuItem});
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(137, 92);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示ToolStripMenuItem.Text = "显示界面";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 开始截图ToolStripMenuItem
            // 
            this.开始截图ToolStripMenuItem.Name = "开始截图ToolStripMenuItem";
            this.开始截图ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.开始截图ToolStripMenuItem.Text = "开始截图";
            this.开始截图ToolStripMenuItem.Click += new System.EventHandler(this.开始截图ToolStripMenuItem_Click);
            // 
            // 识别剪贴板ToolStripMenuItem
            // 
            this.识别剪贴板ToolStripMenuItem.Name = "识别剪贴板ToolStripMenuItem";
            this.识别剪贴板ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.识别剪贴板ToolStripMenuItem.Text = "识别剪贴板";
            this.识别剪贴板ToolStripMenuItem.Click += new System.EventHandler(this.识别剪贴板ToolStripMenuItem_Click);
            // 
            // 退出软件ToolStripMenuItem
            // 
            this.退出软件ToolStripMenuItem.Name = "退出软件ToolStripMenuItem";
            this.退出软件ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出软件ToolStripMenuItem.Text = "退出软件";
            this.退出软件ToolStripMenuItem.Click += new System.EventHandler(this.退出软件ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.Text = "Snipping OCR";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.iconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NotifyIcon notifyIcon;
        private ContextMenuStrip iconMenu;
        private ToolStripMenuItem 显示ToolStripMenuItem;
        private ToolStripMenuItem 识别剪贴板ToolStripMenuItem;
        private ToolStripMenuItem 退出软件ToolStripMenuItem;
        private ToolStripMenuItem 开始截图ToolStripMenuItem;
    }
}