using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        protected abstract Image GetImage();

        public void DrawSelf()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(), X, Y);
        }
    }
}
