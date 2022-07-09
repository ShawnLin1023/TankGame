using TankGame.Properties;

namespace TankGame
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            /*this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 250);*/

            windowG = this.CreateGraphics();
            tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tempBmp); //在bmpG這個臨時畫布上繪製的時候，會跟著繪製到tempBmp這張臨時圖片上
            GameFramework.g = bmpG;
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }

        private static void GameMainThread()
        {
            //GameFramework
            GameFramework.Start();
            int sleepTime = 1000 / 60; //設定時間(毫秒)
            while (true)
            {
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update(); //60fps
                windowG.DrawImage(tempBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }

        //在視窗建立完成會呼叫一次Paint，重新繪製完成也會呼叫一次
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //練習使用畫布+筆繪製出線、字串、圖片
            /*Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, new Point(0, 0), new Point(100, 100));
            g.DrawString("WuShark", new Font("Arial", 20), new SolidBrush(Color.Red), new Point(100, 100));

            Image image = Properties.Resources.Boss;

            Bitmap bm = Properties.Resources.Star1;
            bm.MakeTransparent(Color.Black);

            g.DrawImage(bm, 150, 150);
            g.DrawImage(image, 200, 200);*/
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        //事件
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}