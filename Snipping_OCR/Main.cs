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
        /// ��д������Ϣ
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.Hide();
            base.WndProc(ref m);
            Hotkey.ProcessHotKey(m);
        }

        /// <summary>
        /// ������ע���ȼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            //ע���ȼ� Ctrl+ALT+A ��ͼ
            try
            {
                Hotkey.Regist(base.Handle, HotkeyModifiers.NONE, Keys.F1, new Hotkey.HotKeyCallBackHanlder(StartCapture));
                
            }
            catch
            {
                notifyIcon.ShowBalloonTip(2, "��Ļ OCR", "�ȼ�ע��ʧ�ܣ����Կ���ʹ��������ʽִ�� OCR��",ToolTipIcon.Info);
            }
            
        }

        /// <summary>
        /// �رգ�ж���ȼ�
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
        /// ����ϵͳ��ͼ����
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
            //�����Ҫ�ָ����룬��Ҫ�ָ����汻ע�͵��Ĵ���
            
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
        /// ��ͼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnippingToolProcess_Exited(object? sender, EventArgs e)
        {
            Debug.WriteLine("�������Ѿ�");
            this.BeginInvoke(new Action(() =>
            {
                ClipboardOCR();
            }));
        }

        private readonly string[] ImgAllow = new string[] { "jpg", "png", "gif", "peg", "bmp" };

        /// <summary>
        /// �Ӽ��а��ȡͼƬ��ʶ��
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

            // ֱ�Ӹ��Ƶ�ͼƬ�ļ�
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
        /// ִ��OCRʶ��ͼƬ
        /// </summary>
        /// <param name="imgfile"></param>
        public void showFileOcr(Image imgfile)
        {
            new Task(() =>
            {
                //ʶ��������
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
        /// ����ʶ��
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

        private void �˳����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ʶ�������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardOCR();
        }

        private void ��ʼ��ͼToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartCapture();
        }
    }
}