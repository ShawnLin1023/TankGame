using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// 不可移動的物件
    /// </summary>
    internal class NotMovething : GameObject
    {
        public Image Img { get; set; }

        protected override Image GetImage()
        {
            return Img;
        }

        public NotMovething(int x, int y, Image img)
        {
            this.X = x;
            this.Y = y;
            this.Img = img;
        }
    }
}
