using PaddleOCRSharp;
using Snipping_OCR;
using System.Diagnostics;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Snipping_OCR
{
    public partial class Main : Form
    {
        private Image snapImage;
        private ScreenBody screenBody;
        public Main()
        {
            InitializeComponent();
            base.Hide();
            this.Visible = false;
        }

        /// <summary>
        /// 覆写窗体消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.Hide();
            base.WndProc(ref m);
            Hotkey.ProcessHotKey(m);
        }

        /// <summary>
        /// 启动，注册热键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            //注册热键 Ctrl+ALT+A 截图
            try
            {
                Hotkey.Regist(base.Handle, HotkeyModifiers.NONE, Keys.F1, new Hotkey.HotKeyCallBackHanlder(StartCapture));
                
            }
            catch
            {
                notifyIcon.ShowBalloonTip(2, "屏幕 OCR", "热键注册失败，您仍可以使用其他方式执行 OCR。",ToolTipIcon.Info);
            }
            
        }

        /// <summary>
        /// 关闭，卸载热键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Hotkey.UnRegist(base.Handle, new Hotkey.HotKeyCallBackHanlder(StartCapture));
            }
            catch
            {

            }
        }



        /// <summary>
        /// 调用系统截图处理
        /// </summary>
        private void StartCapture()
        {
            /*
            Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            
            ScreenBody body = new ScreenBody();
            //body.BackgroundImage = img;
            body.Show();
            */
            //如果需要恢复代码，需要恢复下面被注释掉的代码
            
            var psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "ms-screenclip:"
            };
            Process.Start(psi);

            var snippingToolProcess = Process.GetProcessesByName("ScreenClippingHost")[0];
            snippingToolProcess.EnableRaisingEvents = true;
            snippingToolProcess.Exited += SnippingToolProcess_Exited;
            

        }

        /// <summary>
        /// 截图完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnippingToolProcess_Exited(object? sender, EventArgs e)
        {
            Debug.WriteLine("触发了已经");
            this.BeginInvoke(new Action(() =>
            {
                ClipboardOCR();
            }));
        }

        private readonly string[] ImgAllow = new string[] { "jpg", "png", "gif", "peg", "bmp" };

        /// <summary>
        /// 从剪切板获取图片并识别
        /// </summary>
        private void ClipboardOCR()
        {

            //WindowsAPI.ShowWindow(this.Handle, 9);
            var img = Clipboard.GetImage();

            if (img != null) {
                snapImage = img;
                timeOCR_Start();
                return;
            }

            // 直接复制的图片文件
            var files = Clipboard.GetFileDropList();
            if (files.Count > 0)
            {
                string ext = files[0].ToLower().Substring(files[0].Length - 3);
                if (ImgAllow.Contains(ext))
                {
                    snapImage = Image.FromFile(files[0]);
                    timeOCR_Start();
                }
            }
        }

  

        /// <summary>
        /// 执行OCR识别图片
        /// </summary>
        /// <param name="imgfile"></param>
        public void showFileOcr(Image imgfile)
        {
            new Task(() =>
            {
                //识别结果对象
                var ocrResult = new OCRResult();
                using PaddleOCREngine engine = new PaddleOCREngine(null, new OCRParameter());
                ocrResult = engine.DetectText(imgfile);
                var txt = "";
                if (ocrResult.TextBlocks.Count > 0)
                {
                    List<TextBlock> list = ocrResult.TextBlocks;
                    if(list.Count == 1)
                    {
                        txt = list[0].Text;
                    }
                    else
                    {
                        for (int i = 0; i<list.Count - 1;i++ )
                        {
                            txt += list[i].Text + "\r\n";
                        }
                        txt += list[list.Count -1].Text;
                    }
                }
                this.BeginInvoke(new Action(() =>
                {
                    Clipboard.SetText(txt);
                }));
                
            }).Start();
        }

        /// <summary>
        /// 启动识别
        /// </summary>
        private void timeOCR_Start() {
            showFileOcr(snapImage);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //WindowsAPI.ShowWindow(this.Handle, 9);
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void 退出软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 识别剪贴板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardOCR();
        }

        private void 开始截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartCapture();
        }
    }
}