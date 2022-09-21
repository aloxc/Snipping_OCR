using AAAPrintScreen.Component;
using PaddleOCRSharp;
using System.Diagnostics;

namespace AAAPrintScreen
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��д������Ϣ
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
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
            Hotkey.Regist(base.Handle, HotkeyModifiers.MOD_CONTROL_ALT, Keys.A, new Hotkey.HotKeyCallBackHanlder(StartCapture));
        }



        /// <summary>
        /// ����ϵͳ��ͼ����
        /// </summary>
        private void StartCapture()
        {
            // ����
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            Process snippingToolProcess = new Process()
            {
                StartInfo = new ProcessStartInfo("C:\\Windows\\system32\\SnippingTool.exe", "/clip"),
                EnableRaisingEvents = true,
            };
            snippingToolProcess.Exited += SnippingToolProcess_Exited;
            snippingToolProcess.Start();
        }

        /// <summary>
        /// ��ͼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnippingToolProcess_Exited(object? sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                ClipboardOCR();
            }));
        }

        /// <summary>
        /// �Ӽ��а��ȡͼƬ��ʶ��
        /// </summary>
        private void ClipboardOCR()
        {

            WindowsAPI.ShowWindow(this.Handle, 9);
            var img = Clipboard.GetImage();
            if (img == null) return;
            sqPhoto.Image = img;
            timeOCR_Start();

        }

        /// <summary>
        /// �Ϸ�ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sqPhoto_DragDrop(object sender, DragEventArgs e)
        {
            string[] allow = new string[] { "jpg", "png", "gif", "peg", "bmp" };
            string file = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string ext = file.ToLower().Substring(file.Length - 3);
            if (allow.Contains(ext))
            {
                sqPhoto.Image = Image.FromFile(file);
                timeOCR_Start();
            }
        }

        /// <summary>
        /// �����Ϸ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sqPhoto_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// ִ��OCRʶ��ͼƬ
        /// </summary>
        /// <param name="imgfile"></param>
        private void showFileOcr(Image imgfile)
        {

            //ʶ��������
            var ocrResult = new OCRResult();
            using PaddleOCREngine engine = new PaddleOCREngine(null, new OCRParameter());
            ocrResult = engine.DetectText(imgfile);
            if (ocrResult.TextBlocks.Count>0)
            {
                textOCR.Text = "";
                foreach (var item in ocrResult.TextBlocks)
                {
                    textOCR.Text += item.Text+"\r\n";
                }
            }
            textOCR.Cursor = Cursors.IBeam;
        }

        /// <summary>
        /// ����ʶ��
        /// </summary>
        private void timeOCR_Start() {
            textOCR.Cursor = Cursors.WaitCursor;
            timerOCR.Enabled = true;
        }
        private void timerOCR_Tick(object sender, EventArgs e)
        {
            timerOCR.Enabled = false;
            showFileOcr(sqPhoto.Image);
        }


        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowsAPI.ShowWindow(this.Handle, 9);
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

        private void ��ʾToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsAPI.ShowWindow(this.Handle, 9);
        }
    }
}