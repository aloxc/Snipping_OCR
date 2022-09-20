namespace AAAPrintScreen
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//��ס������
            k_hook.Start();//��װ���̹���
        }
        private int alt_Num = 0;
        private DateTime alt_last = DateTime.Now;
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            //�жϰ��µļ� ALT
            if (e.KeyValue == 164 || e.KeyValue == 165)
            {
                if((alt_last - DateTime.Now).TotalMilliseconds < 800)
                {
                    alt_Num++;
                    if (alt_Num >= 3)
                    {
                        alt_Num = 0;
                        SendKeys.Send("Win+Shift+S");
                    }
                }
                else
                {
                    alt_Num = 1;
                    alt_last = DateTime.Now;
                }
            }
        }
    }
}