using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaddleOCRSharp;

namespace Snipping_OCR
{
    public partial class ScreenBody : Form
    {
        /*
        public ScreenBody()
        {
            InitializeComponent();
        }
        private Graphics MainPainter;   //主画面
        private Pen pen;                //画笔
        private bool isDowned;          //判断鼠标是否按下 
        private bool RectReady;         //矩形是否绘制完成 
        private Image baseImage;        //基本图形(原来的画面) 
        private Rectangle Rect;         //就是要保存的矩形 
        private Point downPoint;        //鼠标按下的点 
        int tmpx;
        int tmpy;

        //加载初始化
        private void ScreenBody_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MainPainter = this.CreateGraphics();
            pen = new Pen(Brushes.Blue);
            isDowned = false;
            baseImage = this.BackgroundImage;
            Rect = new Rectangle();
            RectReady = false;
        }

        //双击保存
        private void ScreenBody_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Rect.Contains(e.X, e.Y))
            {
                Image memory = new Bitmap(Rect.Width, Rect.Height);
                Graphics g = Graphics.FromImage(memory);
                g.CopyFromScreen(Rect.X + 1, Rect.Y + 1, 0, 0, Rect.Size);
                //Clipboard.SetImage(memory);
                string filePath = null;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png|" +
                "JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg |GIF files (*.gif)|*.gif |BMP files (*.b" +
                "mp)|*.bmp|Tiff files (*.tif;*.tiff)|*.tif;*.tiff|Png files (*.png)| *.png |All f" +
                "iles (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName.ToString();
                    memory.Save(filePath, ImageFormat.Jpeg);
                }
                this.Close();
            }
        }

        //左击开始截图或移动，右击撤销
        private void ScreenBody_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDowned = true;

                if (RectReady == false)
                {
                    Rect.X = e.X;
                    Rect.Y = e.Y;
                    downPoint = new Point(e.X, e.Y);
                }
                if (RectReady == true)
                {
                    tmpx = e.X;
                    tmpy = e.Y;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
                return;
            }
        }
        //左键放开，截图方框完成
        private void ScreenBody_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDowned = false;
                RectReady = true;

                Image memory = new Bitmap(Rect.Width, Rect.Height);
                Graphics g = Graphics.FromImage(memory);
                g.CopyFromScreen(Rect.X + 1, Rect.Y + 1, 0, 0, Rect.Size);
                //Clipboard.SetImage(memory);
                string filePath = null;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png|" +
                "JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg |GIF files (*.gif)|*.gif |BMP files (*.b" +
                "mp)|*.bmp|Tiff files (*.tif;*.tiff)|*.tif;*.tiff|Png files (*.png)| *.png |All f" +
                "iles (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName.ToString();
                    memory.Save(filePath, ImageFormat.Jpeg);
                }
                this.Close();
            }
        }
        //鼠标移动，画框或者拖动
        private void ScreenBody_MouseMove(object sender, MouseEventArgs e)
        {
            if (RectReady == false)
            {
                if (isDowned == true)
                {
                    Image New = DrawScreen((Image)baseImage.Clone(), e.X, e.Y);
                    MainPainter.DrawImage(New, 0, 0);
                    New.Dispose();
                }
            }
            if (RectReady == true)
            {
                if (Rect.Contains(e.X, e.Y))
                {
                    this.Cursor = Cursors.Hand;
                    if (isDowned == true)
                    {
                        //和上一次的位置比较获取偏移量 
                        Rect.X = Rect.X + e.X - tmpx;
                        Rect.Y = Rect.Y + e.Y - tmpy;
                        //记录如今的位置 
                        tmpx = e.X;
                        tmpy = e.Y;
                        MoveRect((Image)baseImage.Clone(), Rect);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
        }
        //画屏幕
        private Image DrawScreen(Image back, int Mouse_x, int Mouse_y)
        {
            Graphics Painter = Graphics.FromImage(back);
            DrawRect(Painter, Mouse_x, Mouse_y);
            return back;
        }
        //画矩形
        private void DrawRect(Graphics Painter, int Mouse_x, int Mouse_y)
        {
            int width = 0;
            int heigth = 0;
            try
            {
                if (Mouse_y < Rect.Y)
                {
                    Rect.Y = Mouse_y;
                    heigth = downPoint.Y - Mouse_y;
                }
                else
                {
                    heigth = Mouse_y - downPoint.Y;
                }
                if (Mouse_x < Rect.X)
                {
                    Rect.X = Mouse_x;
                    width = downPoint.X - Mouse_x;
                }
                else
                {
                    width = Mouse_x - downPoint.X;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("cuo");
            }
            finally
            {
                Rect.Size = new Size(width, heigth);
                Painter.DrawRectangle(pen, Rect);
            }
        }

        //移动矩形
        private void MoveRect(Image image, Rectangle Rect)
        {
            Graphics Painter = Graphics.FromImage(image);
            Painter.DrawRectangle(pen, Rect.X, Rect.Y, Rect.Width, Rect.Height);
            MainPainter.DrawImage(image, 0, 0);
            image.Dispose();
        }*/

         int x = 0;
         int y = 0;
         int nowX = 0;
         int nowY = 0;
         bool isMouseClick = false;
         Graphics g;
         int width = 0;
         int height = 0;
        // static Graphics gi;  
         Bitmap bmpAll;
        
         bool isOneDown = true;
         Bitmap ocrImage;
        public ScreenBody()
        {
            InitializeComponent();
        }
        private void ScreenBody_Load(object sender, EventArgs e)
        {
            Size size = Screen.PrimaryScreen.Bounds.Size;
            bmpAll = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmpAll);
            g.CopyFromScreen(0, 0, 0, 0, size);

            //注意以下顺序。  
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = false;
            g = this.CreateGraphics();
            this.Opacity = 0.1;
        }
        //双击保存
        private void ScreenBody_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        //左击开始截图或移动，右击撤销
        private void ScreenBody_MouseDown(object sender, MouseEventArgs e)
        {
            if (isOneDown)
            {
                x = MousePosition.X;
                y = MousePosition.Y;
                isMouseClick = true;
                isOneDown = false;
            }
        }

        //左键放开，截图方框完成
        private void ScreenBody_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseClick)
            {
                nowX = MousePosition.X + 1;
                nowY = MousePosition.Y + 1;
                Rectangle destRect = new Rectangle(x, y, nowX - x, nowY - y);
                ocrImage = ((Bitmap)bmpAll).Clone(destRect, bmpAll.PixelFormat);
                ocr();
            }
        }
        private void ocr()
        {
            var ocrResult = new OCRResult();
            using PaddleOCREngine engine = new PaddleOCREngine(null, new OCRParameter());
            ocrResult = engine.DetectText(ocrImage);
            var txt = "";
            if (ocrResult.TextBlocks.Count > 0)
            {
                foreach (var item in ocrResult.TextBlocks)
                {
                    txt += item.Text + "\r\n";
                }
            }
            Clipboard.SetText(txt);

  
            ocrImage.Dispose();
            bmpAll.Dispose();
            g.Dispose();
            isMouseClick = false;
            this.Close();
        }


        //鼠标移动，画框或者拖动
        private void ScreenBody_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseClick)
            {
                width = Math.Abs(MousePosition.X - x);
                height = Math.Abs(MousePosition.Y - y);
                g = CreateGraphics();
                g.Clear(BackColor);
                g.FillRectangle(Brushes.Navy, x < MousePosition.X ? x : MousePosition.X, y < MousePosition.Y ? y : MousePosition.Y, width + 1, height + 1);
            }
        }
    }
}
