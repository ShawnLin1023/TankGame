using TankGame.Properties;

namespace TankGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            /*this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 250);*/
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
    }
}